using System.Globalization;
using System.Text;
using EasySharp.Utility;

namespace EasySharp.Core.Lexers;

internal partial class Lexer
{
    private abstract class State(LexerStateMachine fsm)
    {
        protected readonly LexerStateMachine Fsm = fsm;

        protected void PushToken(Token newToken)
        {
            Fsm.PushToken(newToken);
        }

        public virtual bool Execute() => false;
    }

    private sealed class IdleState(LexerStateMachine fsm) : State(fsm)
    {
    }

    /// <summary>
    /// 对由字母，数字和下划线构成的字符串的预处理
    /// </summary>
    /// <param name="fsm"></param>
    private class NormalState(LexerStateMachine fsm) : State(fsm)
    {
        public override bool Execute()
        {
            return false;
        }
    }

    private sealed class KeywordState(LexerStateMachine fsm) : State(fsm)
    {
        public override bool Execute()
        {
            var code = Fsm.Lexer.CurrentCode;
            var isSuccess = Token.Keywords.TryGetBySecond(code.Value, out var keyword);
            if (!isSuccess) return false;
            PushToken(code.ToToken(keyword));
            return true;
        }
    }

    private sealed class IdentifierState(LexerStateMachine fsm) : State(fsm)
    {
        public override bool Execute()
        {
            PushToken(Fsm.Lexer.CurrentCode.ToToken(TokenType.Identifier));
            return true;
        }
    }

    private sealed class StringLiteralState(LexerStateMachine fsm) : State(fsm)
    {
        public override bool Execute()
        {
            var nextID = Fsm.Lexer.FirstMatchString("\"");
            if (nextID == -1) return false;
            var startCode = Fsm.Lexer.CurrentCode;
            var endCode = Fsm.Lexer.GetCodeByIndex(nextID);
            StringBuilder sb = new();
            for (int i = Fsm.Lexer._index + 1; i < nextID - 1; i++)
            {
                sb.Append(Fsm.Lexer.GetCodeByIndex(i).Value);
            }

            string value = sb.ToString();

            PushToken(new Token(TokenType.String, startCode.Line, startCode.ColumnStart, endCode.ColumnEnd,
                $"\"{value}\"", value));
            Fsm.Lexer.Advance(nextID - Fsm.Lexer._index);
            return true;
        }
    }

    private sealed class CharLiteralState(LexerStateMachine fsm) : State(fsm)
    {
        public override bool Execute()
        {
            var nextID = Fsm.Lexer.FirstMatchString("'");
            if (nextID == -1) return false;
            if (Fsm.Lexer._index + 1 == nextID) return false;
            var startCode = Fsm.Lexer.CurrentCode;
            var endCode = Fsm.Lexer.GetCodeByIndex(nextID);
            StringBuilder sb = new();
            for (int i = Fsm.Lexer._index + 1; i < nextID - 1; i++)
            {
                sb.Append(Fsm.Lexer.GetCodeByIndex(i).Value);
            }

            string strValue = sb.ToString();
            if (!ProcessCharContent(strValue, out var charValue)) return false;
            PushToken(new Token(TokenType.Char, startCode.Line, startCode.ColumnStart, endCode.ColumnEnd,
                $"'{strValue}'", charValue));
            Fsm.Lexer.Advance(nextID - Fsm.Lexer._index);
            return true;
        }

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

        private char HandleUnicodeEscape(string sequence)
        {
            // \u + 4位十六进制
            if (sequence.Length == 6 && sequence.StartsWith("\\u"))
            {
                string hexValue = sequence.Substring(2);
                if (int.TryParse(hexValue, NumberStyles.HexNumber, null, out int charCode))
                {
                    return (char)charCode;
                }
            }
            // \U + 8位十六进制 (用于表示超出BMP的Unicode字符)
            else if (sequence.Length == 10 && sequence.StartsWith("\\U"))
            {
                string hexValue = sequence.Substring(2);
                if (int.TryParse(hexValue, NumberStyles.HexNumber, null, out int charCode))
                {
                    // 注意：C#中char是UTF-16，所以这里可能需要特殊处理超出BMP的字符
                    if (charCode <= 0xFFFF)
                    {
                        return (char)charCode;
                    }
                    else
                    {
                        // 处理超出BMP的字符，可能需要转换为代理对
                        // 在实际编译器中，这里可能需要特殊处理或报错
                        return '\0';
                    }
                }
            }

            return '\0'; // 处理错误情况
        }

        private char HandleOctalEscape(string sequence)
        {
            // 去掉前导反斜杠
            string octalValue = sequence.Substring(1);

            // 处理八进制值
            try
            {
                int charCode = Convert.ToInt32(octalValue, 8);
                if (charCode <= 255) // 确保在一个字节范围内
                {
                    return (char)charCode;
                }
            }
            catch
            {
                // 转换错误
            }

            return '\0';
        }

        #endregion

        #endregion
    }

    private sealed class IntegerLiteralState(LexerStateMachine fsm) : State(fsm)
    {
        public override bool Execute()
        {
            PushToken(Fsm.Lexer.CurrentCode.ToToken(TokenType.Int, int.Parse(Fsm.Lexer.CurrentCode.Value)));
            return true;
        }
    }

    private sealed class FloatLiteralState(LexerStateMachine fsm) : State(fsm)
    {
        public override bool Execute()
        {
            if (Fsm.Lexer.CurrentCode.Value == ".")
            {
                if (Fsm.Lexer.ExistNextCode() && Fsm.Lexer.SecondCode.Value.IsNumber())
                {
                    PushToken(Fsm.Lexer.CurrentCode.ToToken(TokenType.Float, float.Parse(
                        "." + Fsm.Lexer.SecondCode.Value, CultureInfo.InvariantCulture
                    )));
                    Fsm.Lexer.Advance();
                    return true;
                }
            }
            else if (Fsm.Lexer.CurrentCode.Value.IsNumber())
            {
                if (Fsm.Lexer.ExistNextCode(2) && Fsm.Lexer.SecondCode.Value.IsNumber())
                {
                    PushToken(Fsm.Lexer.CurrentCode.ToToken(TokenType.Float, float.Parse(
                        Fsm.Lexer.CurrentCode.Value + "." + Fsm.Lexer.SecondCode.Value, CultureInfo.InvariantCulture
                    )));
                    Fsm.Lexer.Advance(2);
                    return true;
                }
            }

            return false;
        }
    }

    private sealed class OperatorState(LexerStateMachine fsm) : State(fsm)
    {
        public override bool Execute()
        {
            var code = Fsm.Lexer.CurrentCode;
            if (Fsm.Lexer.ExistNextCode(2) && code.Value == "." &&
                Fsm.Lexer.SecondCode.Value.IsNumber()) return false;

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
                    if (Token.Operators.TryGetBySecond(slice.ToString(), out var type))
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

    private class Transition(Func<bool> condition, State to)
    {
        public readonly State To = to;
        public bool Match() => condition();
    }

    private class LexerStateMachine
    {
        private readonly Dictionary<State, List<Transition>> _transitions = new();
        private readonly Queue<Token> _tokenQueue = new();
        private readonly State _firstState;
        public readonly Lexer Lexer;

        public LexerStateMachine(Lexer lexer)
        {
            var initialState = new IdleState(this);
            var keywordState = new KeywordState(this);
            var normalState = new NormalState(this);
            var identifierState = new IdentifierState(this);
            var stringLiteralState = new StringLiteralState(this);
            var charLiteralState = new CharLiteralState(this);
            var integerLiteralState = new IntegerLiteralState(this);
            var floatLiteralState = new FloatLiteralState(this);
            var operatorState = new OperatorState(this);
            _firstState = initialState;
            Lexer = lexer;

            AddTransition(initialState, normalState, () => Lexer.CurrentCode.CodeToken == CodeToken.Normal);
            AddTransition(initialState, operatorState, () => Lexer.CurrentCode.CodeToken == CodeToken.Operator);
            AddTransition(initialState, stringLiteralState, () => lexer.CurrentCode.Value == "\"");
            AddTransition(initialState, charLiteralState, () => lexer.CurrentCode.Value == "'");

            AddTransition(normalState, identifierState, () => lexer.FirstChar == '_');
            AddTransition(normalState, keywordState, () => Token.Keywords.ContainsSecond(Lexer.CurrentCode.Value));
            AddTransition(keywordState, identifierState, () => true);
            AddTransition(normalState, floatLiteralState, () => lexer.CurrentCode.Value.IsNumber());
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

            if (_tokenQueue.Count == 0) _tokenQueue.Enqueue(Lexer.CurrentCode.ToToken(TokenType.Error));

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