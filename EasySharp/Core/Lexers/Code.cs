namespace EasySharp.Core.Lexers;

public enum CodeToken
{
    None,
    Letter,
    Digit,
    Operator,
    Separator,
    RawString,
    RawChar,
}

public readonly struct Code(
    int line,
    int columnStart,
    int columnEnd,
    string value,
    CodeToken codeToken = CodeToken.None)
{
    public readonly int Line = line;
    public readonly int ColumnStart = columnStart;
    public readonly int ColumnEnd = columnEnd;
    public readonly string Value = value;
    public readonly CodeToken CodeToken = codeToken;
    public static Code Empty => new(0, 0, 0, string.Empty);

    public static CodeToken GetToken(char c)
    {
        if (char.IsLetter(c) || c == '_' || c == '`') return CodeToken.Letter;
        if (char.IsDigit(c)) return CodeToken.Digit;
        if (Token.OperatorsChars.Contains(c)) return CodeToken.Operator;
        if (Token.Separators.ContainsFirst(c)) return CodeToken.Separator;
        if (c == '\"') return CodeToken.RawString;
        if (c == '\'') return CodeToken.RawChar;
        return CodeToken.None;
    }

    public static bool Equals(CodeToken first, CodeToken second)
    {
        if (first == CodeToken.Letter && second == CodeToken.Digit) return true;
        return first == second;
    }

    public override string ToString()
    {
        return $"[{CodeToken}] \t {Value} \t {Line}:({ColumnStart} - {ColumnEnd})";
    }
}