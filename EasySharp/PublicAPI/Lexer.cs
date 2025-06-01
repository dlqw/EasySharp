using EasySharp.Core;

namespace EasySharp.PublicAPI;
using innerLexer = Core.Lexers.Lexer;

public class Lexer
{
    private innerLexer _lexer;
    
    public IReadOnlyList<Token> Tokenize(string str)
    {
        _lexer = new innerLexer(str);
        _lexer.Tokenize();
        return _lexer.Tokens;
    }
}