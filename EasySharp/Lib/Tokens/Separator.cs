using EasySharp.Utility;

// ReSharper disable once CheckNamespace
namespace EasySharp.Core;

public partial class Token
{
    public static readonly BiDictionary<char, TokenType> Separators = new()
    {
        {';', TokenType.Semicolon },
        {'(', TokenType.LeftParen},
        {')', TokenType.RightParen},
        {'[', TokenType.LeftBracket},
        {']', TokenType.RightBracket},
        {'{', TokenType.LeftBrace},
        {'}', TokenType.RightBrace},
        {',' , TokenType.Comma}
    };
}