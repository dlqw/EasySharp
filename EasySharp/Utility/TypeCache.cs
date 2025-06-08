using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace EasySharp.Utility;

public static class TypeCache
{
    private static readonly Dictionary<string, Type> _types = new();

    public static bool TryGetType(string name, [NotNullWhen(true)] out Type? type)
    {
        if (_types.TryGetValue(name, out type)) return true;
        type = Assembly.GetExecutingAssembly().GetType(name);
        if (type == null) return false;
        _types.Add(name, type);
        return true;
    }
}