using System.Globalization;

namespace EasySharp.Utility;

public static class LexerHelper
{
    public static bool IsNumber(this string str)
    {
        return str.All(char.IsDigit);
    }

    public static bool IsLetter(this string str)
    {
        return str.All(char.IsLetter);
    }
}