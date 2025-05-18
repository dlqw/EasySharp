using EasySharp.Utility;

// ReSharper disable once CheckNamespace
namespace EasySharp.Core;

internal partial class Token
{
    public static readonly HashSet<char> Separators =
    [
        ';',
        '\"',
        '\''
    ];
}