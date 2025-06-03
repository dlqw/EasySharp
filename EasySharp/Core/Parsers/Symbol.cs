namespace EasySharp.Core.Parsers;

/// 文法符号
public class Symbol(string name, SymbolType type)
{
    public string Name { get; } = name;
    public SymbolType Type { get; } = type;

    public override bool Equals(object? obj)
    {
        return obj is Symbol other && Name == other.Name && Type == other.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Type);
    }

    public override string ToString()
    {
        return Name;
    }
}