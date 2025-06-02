using System.Text;

namespace EasySharp.Core.Lexers;

public class CodeSplitter
{
    #region private

    private int _line;
    private int _columnStart;
    private int _columnEnd;
    private int _index;
    private CodeToken _codeToken = CodeToken.None;
    private Ruler _ruler;
    private readonly NormalRuler _normalRuler;
    private readonly SingleLineCommentRuler _singleLineCommentRuler;
    private readonly MultiLineCommentRuler _multiLineCommentRuler;
    private RawCharRuler _rawCharRuler;
    private RawStringRuler _rawStringRuler;
    private readonly string _source;
    private readonly StringBuilder _value = new();

    private bool Match(char first)
    {
        if (_index < 0 || _index >= _source.Length) return false;
        return _source[_index] == first;
    }

    private bool Match(char first, char second)
    {
        if (_index < 0 || _index >= _source.Length - 1) return false;
        return _source[_index] == first && _source[_index + 1] == second;
    }

    #endregion

    #region public

    public CodeSplitter(string source)
    {
        _source = source;
        _normalRuler = new NormalRuler(this);
        _singleLineCommentRuler = new SingleLineCommentRuler(this);
        _multiLineCommentRuler = new MultiLineCommentRuler(this);
        _rawCharRuler = new RawCharRuler(this);
        _rawStringRuler = new RawStringRuler(this);
        _ruler = _normalRuler;
    }

    public IEnumerable<Code> Split()
    {
        _line = 1;
        _columnStart = 0;
        _columnEnd = -1;
        _codeToken = CodeToken.None;
        this._value.Clear();
        NormalRuler normalRuler = new NormalRuler(this);
        _ruler = normalRuler;
        StringBuilder value = new StringBuilder();
        for (_index = 0; _index < _source.Length; _index++)
        {
            #region Line Step

            _columnEnd++;

            #endregion

            #region Next Ruler

            if (_ruler.NextState(out var ruler))
            {
                _ruler = ruler;
            }

            #endregion

            #region Yield Return Code

            if (_ruler.NextCode(_source[_index], out var code))
            {
                yield return code;
                if(code.CodeToken == CodeToken.Error) yield break;
            }

            #endregion

            if (value.Length > 0 && _index < _source.Length)
            {
                yield return new Code(_line, _columnStart, _columnEnd, value.ToString(), _codeToken);
            }
        }
    }

    #endregion

    private abstract class Ruler(CodeSplitter splitter)
    {
        protected readonly CodeSplitter Splitter = splitter;
        protected StringBuilder Value => Splitter._value;

        protected virtual void NextLine()
        {
            Splitter._line++;
            Splitter._columnStart = 0;
            Splitter._columnEnd = -1;
        }

        public virtual bool NextState(out Ruler ruler)
        {
            ruler = this;
            return false;
        }

        public virtual bool NextCode(char c, out Code code)
        {
            code = Code.Empty;
            return false;
        }
    }

    private sealed class NormalRuler(CodeSplitter splitter) : Ruler(splitter)
    {
        public override bool NextState(out Ruler ruler)
        {
            ruler = this;
            if (Splitter.Match('/', '/'))
            {
                ruler = Splitter._singleLineCommentRuler;
                return true;
            }

            if (Splitter.Match('/', '*'))
            {
                ruler = Splitter._multiLineCommentRuler;
                return true;
            }

            if (Splitter.Match('"'))
            {
                ruler = Splitter._rawStringRuler;
                return true;
            }

            if (Splitter.Match('\''))
            {
                ruler = Splitter._rawCharRuler;
                return true;
            }

            return false;
        }

        public override bool NextCode(char c, out Code code)
        {
            code = new Code();

            #region New Line

            if (c == '\n')
            {
                NextLine();
                return false;
            }

            #endregion

            #region WhiteSpace

            if (char.IsWhiteSpace(c))
            {
                if (Value.Length > 0)
                {
                    code = new Code(Splitter._line, Splitter._columnStart, Splitter._columnEnd, Value.ToString(),
                        Splitter._codeToken);
                    Value.Clear();
                    Splitter._codeToken = CodeToken.None;
                    Splitter._columnStart = Splitter._columnEnd + 1;
                    return true;
                }

                Splitter._columnStart++;
                return false;
            }

            #endregion

            #region Split Different Tokens

            bool success = false;
            CodeToken newCodeToken = Code.GetToken(c);
            if (!Code.Equals(Splitter._codeToken, newCodeToken))
            {
                if (Value.Length > 0)
                {
                    code = new Code(Splitter._line, Splitter._columnStart, Splitter._columnEnd, Value.ToString(),
                        Splitter._codeToken);
                    Value.Clear();
                    Splitter._columnStart = Splitter._columnEnd + 1;
                    success = true;
                }
                else
                {
                    Splitter._columnStart++;
                }

                Splitter._codeToken = newCodeToken;
            }

            #endregion

            Value.Append(c);
            return success;
        }
    }

    private sealed class SingleLineCommentRuler(CodeSplitter splitter) : Ruler(splitter)
    {
        public override bool NextState(out Ruler ruler)
        {
            ruler = this;
            if (Splitter.Match('\n'))
            {
                ruler = Splitter._normalRuler;
                return true;
            }

            return false;
        }
    }

    private sealed class MultiLineCommentRuler(CodeSplitter splitter) : Ruler(splitter)
    {
        public override bool NextState(out Ruler ruler)
        {
            ruler = this;
            if (Splitter.Match('*', '/'))
            {
                ruler = Splitter._normalRuler;
                Splitter._index += 2;
                Splitter._columnEnd += 2;
                Splitter._columnStart = Splitter._columnEnd;
                return true;
            }

            return false;
        }
    }

    private sealed class RawCharRuler(CodeSplitter splitter) : Ruler(splitter)
    {
        public override bool NextState(out Ruler ruler)
        {
            ruler = this;
            if (Splitter.Match('\''))
            {
                ruler = Splitter._normalRuler;
                return true;
            }

            return false;
        }
        
        public override bool NextCode(char c, out Code code)
        {
            code = new Code();
            if (c == '\'')
            {
                Splitter._codeToken = CodeToken.RawChar;
                Splitter._columnStart = Splitter._columnEnd + 1;
                Value.Clear();
            }
            
            // todo 处理转义字符 支持 UTF-8 编码格式等

            Value.Append(c);
            if (Value.Length >= 3) Splitter._codeToken = CodeToken.Error;
            return false;
        }
    }

    private sealed class RawStringRuler(CodeSplitter splitter) : Ruler(splitter)
    {
        public override bool NextState(out Ruler ruler)
        {
            ruler = this;
            if (Splitter.Match('"'))
            {
                ruler = Splitter._normalRuler;
                return true;
            }

            return false;
        }

        public override bool NextCode(char c, out Code code)
        {
            code = new Code();
            if (c == '"')
            {
                Splitter._codeToken = CodeToken.RawString;
                Splitter._columnStart = Splitter._columnEnd + 1;
                Value.Clear();
            }

            Value.Append(c);
            return false;
        }
    }
}