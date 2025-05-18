using EasySharp.Core.Lexers;

namespace EasySharp.Core;

internal static class TokenExtension
{
    public static Token ToToken(this Code code, TokenType type, object? value = null) =>
        new(type, code.Line, code.ColumnStart, code.ColumnEnd, code.Value, value);
}