using EasySharp.Utility;

namespace EasySharp.Core.Lexers;

internal sealed partial class Lexer(string source)
{
    #region private

    private readonly List<Code> _codes = new();
    private readonly List<Token> _tokens = new();
    private readonly string _source = source;
    private int _index = 0;
    private bool ExistNextCode(int length = 1) => _index + length < _codes.Count;
    private Code CurrentCode => _codes[_index];
    private Code SecondCode => _codes[_index + 1];
    private char FirstChar => _codes[_index].Value[0];
    private Code GetCodeByIndex(int index) => _codes[index];

    private int FirstMatchString(string value)
    {
        for (int i = _index + 1; _index < _codes.Count; _index++)
        {
            if (_codes[i].Value == value)
            {
                return i;
            }
        }

        return -1;
    }

    private void Advance(int index = 1) => _index += index;

    #endregion

    #region public

    public IReadOnlyList<Token> Tokens => _tokens;
    
    public bool Tokenize()
    {
        Debug.Log("开始 Tokenize");
        _tokens.Clear();
        _codes.Clear();
        _codes.AddRange(new CodeSplitter(source).Split());
        Debug.Log("代码分割完成");
        // 代码分割完成后 取到的是 只有分隔符，数字，字母，下划线，运算符混合成的 Code 流
        LexerStateMachine lsm = new LexerStateMachine(this);
        for (_index = 0; _index < _codes.Count; _index++)
        {
            foreach (var token in lsm.Tokenize())
            {
                _tokens.Add(token);
                if (token.Type == TokenType.Error)
                {
                    return false;
                }
            }
        }

        _tokens.Add(new Token(TokenType.EOF, _codes.Last().Line, _codes.Last().ColumnEnd, _codes.Last().ColumnEnd,
            "EOF"));

        return true;
    }

    #endregion
}