namespace EasySharp.Core;

public partial class Token(
    TokenType type,
    int line,
    int columnStart,
    int columnEnd,
    string lexeme,
    object? value = null)
{
    public TokenType Type { get; set; } = type;
    public int Line { get; set; } = line;
    public int ColumnStart { get; set; } = columnStart;
    public int ColumnEnd { get; set; } = columnEnd;
    public string Lexeme { get; set; } = lexeme;
    public object? Value { get; set; } = value;

    public override string ToString()
    {
        string typeStr = $"[{Type}]";
        if (typeStr.Length > 8)
        {
            return $"{typeStr}\t {Lexeme} \t {Line}:({ColumnStart} - {ColumnEnd})";
        }
        else
        {
            return $"{typeStr}\t\t {Lexeme} \t {Line}:({ColumnStart} - {ColumnEnd})";
        }
    }
}