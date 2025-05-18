namespace EasySharp.Core.Lexers;

internal enum CodeToken
{
    None,
    Normal,
    Operator,
    Separator,
}

internal struct Code(int line, int columnStart, int columnEnd, string value, CodeToken codeToken = CodeToken.None)
{
    public readonly int Line = line;
    public readonly int ColumnStart = columnStart;
    public readonly int ColumnEnd = columnEnd;
    public readonly string Value = value;
    public readonly CodeToken CodeToken = codeToken;
}