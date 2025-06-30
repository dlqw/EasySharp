using EasySharp.Utility;

namespace EasySharp.Core.Lexers;

internal sealed partial class Lexer(string source)
{
    #region private

    private readonly List<Code> _codes = new();
    private readonly List<Token> _tokens = new();
    private int _index;
    private bool ExistNextCode(int length = 1) => _index + length < _codes.Count;
    private Code CurrentCode => _codes[_index];
    private Code SecondCode => _codes[_index + 1];
    private Code ThirdCode => _codes[_index + 2];
    private Code LastCode => _codes.Last();

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
        LexerFA lsm = new LexerFA(this);
        if (_codes.Count <= 0) return false;
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

        _tokens.Add(new Token(TokenType.EOF, LastCode.Line, LastCode.ColumnEnd, LastCode.ColumnEnd, string.Empty));

        return true;
    }

    #endregion
}