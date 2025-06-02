using System.Text;

namespace EasySharp.Core.Lexers;

public class CodeSplitter
{
    #region private

    private int _line = 0;
    private int _columnStart = 0;
    private int _columnEnd = 0;
    private int _index = 0;
    private CodeToken _codeToken = CodeToken.None;
    private Ruler _ruler;
    private NormalRuler _normalRuler;
    private SingleLineCommentRuler _singleLineCommentRuler;
    private MultiLineCommentRuler _multiLineCommentRuler;
    private readonly string _source;
    private readonly StringBuilder value = new();

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
    }

    public IEnumerable<Code> Split()
    {
        _line = 1;
        _columnStart = 0;
        _columnEnd = -1;
        _codeToken = CodeToken.None;
        this.value.Clear();
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
        protected CodeSplitter Splitter = splitter;
        protected StringBuilder Value => Splitter.value;

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
}