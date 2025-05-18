namespace EasySharp.Core;

internal partial class Token(TokenType type, int line, int columnStart, int columnEnd, string lexeme, object? value = null)
{
    public TokenType Type { get; set; } = type;
    public int Line { get; set; } = line;
    public int ColumnStart { get; set; } = columnStart;
    public int ColumnEnd { get; set; } = columnEnd;
    public string Lexeme { get; set; } = lexeme;
    public object? Value { get; set; } = value;
    
    public static bool IsSpecial(string lexeme) => true;
}