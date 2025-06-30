using System.Globalization;
using System.Text;
using EasySharp.Utility;

namespace EasySharp.Core.Lexers;

internal partial class Lexer
{
    private abstract class State(LexerFA fa)
    {
        protected readonly LexerFA Fa = fa;

        protected void PushToken(Token newToken)
        {
            Fa.PushToken(newToken);
        }

        public virtual bool Execute() => false;
    }

    private sealed class IdleState(LexerFA fa) : State(fa)
    {
    }

    /// <summary>
    /// 对由字母，数字和下划线构成的字符串的预处理
    /// </summary>
    /// <param name="fa"></param>
    private class LetterState(LexerFA fa) : State(fa)
    {
        public override bool Execute()
        {
            return false;
        }
    }

    private sealed class KeywordState(LexerFA fa) : State(fa)
    {
        public override bool Execute()
        {
            var code = Fa.Lexer.CurrentCode;
            var isSuccess = Token.Keywords.TryGetByFirst(code.Value, out var keyword);
            if (!isSuccess) return false;
            PushToken(code.ToToken(keyword));
            return true;
        }
    }

    private sealed class IdentifierState(LexerFA fa) : State(fa)
    {
        public override bool Execute()
        {
            PushToken(Fa.Lexer.CurrentCode.ToToken(TokenType.Identifier));
            return true;
        }
    }

    private sealed class StringLiteralState(LexerFA fa) : State(fa)
    {
        public override bool Execute()
        {
            PushToken(Fa.Lexer.CurrentCode.ToToken(TokenType.StringLiteral));
            return true;
        }

        // public override bool Execute()
        // {
        //     var nextID = Fa.Lexer.FirstMatchCode(CodeToken.RawString);
        //     if (nextID == -1) return false;
        //     var startCode = Fa.Lexer.CurrentCode;
        //     var endCode = Fa.Lexer.GetCodeByIndex(nextID);
        //     StringBuilder sb = new();
        //     for (int i = Fa.Lexer._index + 1; i < nextID; i++)
        //     {
        //         sb.Append(Fa.Lexer.GetCodeByIndex(i).Value);
        //     }
        //
        //     string value = sb.ToString();
        //
        //     PushToken(new Token(TokenType.String, startCode.Line, startCode.ColumnStart, endCode.ColumnEnd,
        //         $"\"{value}\"", value));
        //     Fa.Lexer.Advance(nextID - Fa.Lexer._index);
        //     return true;
        // }
    }

    private sealed class CharLiteralState(LexerFA fa) : State(fa)
    {
        public override bool Execute()
        {
            PushToken(Fa.Lexer.CurrentCode.ToToken(TokenType.CharLiteral));
            return true;
        }

        // public override bool Execute()
        // {
        //     var nextID = Fa.Lexer.FirstMatchCode(CodeToken.RawChar);
        //     if (nextID == -1) return false;
        //     if (Fa.Lexer._index + 1 == nextID) return false;
        //     var startCode = Fa.Lexer.CurrentCode;
        //     var endCode = Fa.Lexer.GetCodeByIndex(nextID);
        //     StringBuilder sb = new();
        //     for (int i = Fa.Lexer._index + 1; i < nextID; i++)
        //     {
        //         sb.Append(Fa.Lexer.GetCodeByIndex(i).Value);
        //     }
        //
        //     string strValue = sb.ToString();
        //     if (!ProcessCharContent(strValue, out var charValue)) return false;
        //     PushToken(new Token(TokenType.Char, startCode.Line, startCode.ColumnStart, endCode.ColumnEnd,
        //         $"'{strValue}'", charValue));
        //     Fa.Lexer.Advance(nextID - Fa.Lexer._index);
        //     return true;
        // }

        #region private

        #region Handle Char

        private bool ProcessCharContent(string content, out char result)
        {
            // 处理转义序列
            if (content.StartsWith("\\"))
            {
                return ProcessEscapeSequence(content, out result);
            }

            result = content[0];
            return content.Length == 1;
        }

        private bool ProcessEscapeSequence(string sequence, out char result)
        {
            try
            {
                result = sequence switch
                {
                    // 基本转义序列
                    "\\n" => '\n',
                    "\\r" => '\r',
                    "\\t" => '\t',
                    "\\'" => '\'',
                    "\\\"" => '\"',
                    "\\\\" => '\\',
                    "\\0" => '\0',
                    "\\a" => '\a', // 警报
                    "\\b" => '\b', // 退格
                    "\\f" => '\f', // 换页
                    "\\v" => '\v', // 垂直制表符

                    // // Unicode转义序列
                    // _ when sequence.StartsWith("\\u") => HandleUnicodeEscape(sequence),
                    //
                    // // ASCII八进制表示 \ooo
                    // _ when sequence.StartsWith("\\") && char.IsDigit(sequence[1]) =>
                    //     HandleOctalEscape(sequence),
                    _ => throw new ArgumentOutOfRangeException(nameof(sequence), sequence, null)
                };
            }
            catch (Exception)
            {
                result = '\0';
                return false;
            }

            return true;
        }

        #endregion

        #endregion
    }

    private sealed class IntegerLiteralState(LexerFA fa) : State(fa)
    {
        public override bool Execute()
        {
            PushToken(Fa.Lexer.CurrentCode.ToToken(TokenType.IntLiteral, int.Parse(Fa.Lexer.CurrentCode.Value)));
            return true;
        }
    }

    private sealed class FloatLiteralState(LexerFA fa) : State(fa)
    {
        public override bool Execute()
        {
            if (Fa.Lexer.CurrentCode.Value == ".")
            {
                if (Fa.Lexer.ExistNextCode() && Fa.Lexer.SecondCode.CodeToken == CodeToken.Digit)
                {
                    if (float.TryParse("." + Fa.Lexer.SecondCode.Value, out var num))
                    {
                        PushToken(new Token(TokenType.FloatLiteral, Fa.Lexer.CurrentCode.Line,
                            Fa.Lexer.CurrentCode.ColumnStart,
                            Fa.Lexer.SecondCode.ColumnEnd, num.ToString(CultureInfo.InvariantCulture), num));
                        Fa.Lexer.Advance();
                        return true;
                    }

                    return false;
                }
            }

            if (Fa.Lexer.ExistNextCode(2) && Fa.Lexer.SecondCode.Value == "." &&
                Fa.Lexer.ThirdCode.CodeToken == CodeToken.Digit)
            {
                if (float.TryParse(Fa.Lexer.CurrentCode.Value + "." + Fa.Lexer.ThirdCode.Value, out var num))
                {
                    PushToken(new Token(TokenType.FloatLiteral, Fa.Lexer.CurrentCode.Line, Fa.Lexer.CurrentCode.ColumnStart,
                        Fa.Lexer.ThirdCode.ColumnEnd, num.ToString(CultureInfo.InvariantCulture), num));
                    Fa.Lexer.Advance(2);
                    return true;
                }
            }


            return false;
        }
    }

    private sealed class OperatorState(LexerFA fa) : State(fa)
    {
        public override bool Execute()
        {
            var code = Fa.Lexer.CurrentCode;
            if (Fa.Lexer.ExistNextCode(2) && code.Value == "." &&
                Fa.Lexer.SecondCode.CodeToken == CodeToken.Digit) return false;

            ReadOnlySpan<char> span = code.Value.AsSpan();
            int left = 0;
            int right = span.Length - 1;

            while (left < span.Length)
            {
                bool find = false;
                while (right >= left)
                {
                    int length = right - left + 1;
                    ReadOnlySpan<char> slice = span.Slice(left, length);
                    if (Token.Operators.TryGetByFirst(slice.ToString(), out var type))
                    {
                        PushToken(code.ToToken(type));
                        left = right + 1;
                        right = span.Length - 1;
                        find = true;
                        break;
                    }

                    right--;
                }

                if (find == false)
                {
                    return false;
                }
            }

            return true;
        }
    }

    private sealed class SeparatorState(LexerFA fa) : State(fa)
    {
        public override bool Execute()
        {
            var code = Fa.Lexer.CurrentCode;
            if (!Token.Separators.TryGetByFirst(code.Value[0], out var separator)) return false;
            PushToken(code.ToToken(separator));
            return true;
        }
    }

    private sealed class ErrorState(LexerFA fa) : State(fa)
    {
        public override bool Execute()
        {
            PushToken(Fa.Lexer.CurrentCode.ToToken(TokenType.Error));
            return true;
        }
    }

    private class Transition(Func<bool> condition, State to)
    {
        public readonly State To = to;
        public bool Match() => condition();
    }

    private class LexerFA
    {
        private readonly Dictionary<State, List<Transition>> _transitions = new();
        private readonly Queue<Token> _tokenQueue = new();
        private readonly State _firstState;
        public readonly Lexer Lexer;

        public LexerFA(Lexer lexer)
        {
            var initialState = new IdleState(this);
            var keywordState = new KeywordState(this);
            var letterState = new LetterState(this);
            var identifierState = new IdentifierState(this);
            var stringLiteralState = new StringLiteralState(this);
            var charLiteralState = new CharLiteralState(this);
            var integerLiteralState = new IntegerLiteralState(this);
            var floatLiteralState = new FloatLiteralState(this);
            var operatorState = new OperatorState(this);
            var separatorState = new SeparatorState(this);
            var errorState = new ErrorState(this);
            _firstState = initialState;
            Lexer = lexer;

            AddTransition(initialState, letterState, () => Lexer.CurrentCode.CodeToken == CodeToken.Letter);
            AddTransition(initialState, operatorState, () => Lexer.CurrentCode.CodeToken == CodeToken.Operator);
            AddTransition(initialState, floatLiteralState, () => Lexer.CurrentCode.CodeToken == CodeToken.Digit);
            AddTransition(initialState, separatorState, () => Lexer.CurrentCode.CodeToken == CodeToken.Separator);
            AddTransition(initialState, stringLiteralState, () => Lexer.CurrentCode.CodeToken == CodeToken.RawString);
            AddTransition(initialState, charLiteralState, () => Lexer.CurrentCode.CodeToken == CodeToken.RawChar);
            AddTransition(initialState, errorState, () => Lexer.CurrentCode.CodeToken == CodeToken.Error);

            AddTransition(letterState, keywordState, () => true);
            AddTransition(keywordState, identifierState, () => true);
            AddTransition(floatLiteralState, integerLiteralState, () => true);
            AddTransition(operatorState, floatLiteralState, () => true); // todo 暂时没有别的状态转移 所以先设为 true
        }

        public IEnumerable<Token> Tokenize()
        {
            _tokenQueue.Clear();
            Stack<State> prepareStates = new();
            prepareStates.Push(_firstState);
            while (prepareStates.TryPop(out var state))
            {
                // 如果当前状态不匹配 这里存在 prepareStates 的 push 操作
                if (!state.Execute())
                {
                    // 则寻找下一个状态
                    if (_transitions.TryGetValue(state, out var transitions))
                    {
                        foreach (var transition in transitions.Where(transition => transition.Match()))
                        {
                            prepareStates.Push(transition.To);
                            break;
                        }
                    }
                }
                else
                {
                    // 如果存在匹配的状态 就返回他
                    prepareStates.Clear();
                    break;
                }
            }

            if (Lexer.ExistNextCode())
            {
                if (_tokenQueue.Count == 0) _tokenQueue.Enqueue(Lexer.CurrentCode.ToToken(TokenType.Error));
            }

            foreach (var token in _tokenQueue)
            {
                yield return token;
            }
        }

        private void AddTransition(State from, State to, Func<bool> condition)
        {
            if (_transitions.TryGetValue(from, out var transitions))
            {
                transitions.Add(new Transition(condition, to));
            }
            else
            {
                _transitions.Add(from, [new Transition(condition, to)]);
            }
        }

        public void PushToken(Token newToken)
        {
            _tokenQueue.Enqueue(newToken);
        }
    }
}