namespace EasySharp.Core.Parsers;

/// 文法符号
public partial class Symbol(string name, SymbolType type)
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

public class SymbolInstance(string mame, SymbolType type) : Symbol(mame, type)
{
    public bool IsMaybe;
    public bool IsList;
    public static SymbolInstance M(Symbol symbol) => new(symbol.Name, symbol.Type, true, false);
    public static SymbolInstance L(Symbol symbol) => new(symbol.Name, symbol.Type, false, true);
    public static SymbolInstance ML(Symbol symbol) => new(symbol.Name, symbol.Type, true, true);

    public SymbolInstance(string name, SymbolType type, bool isMaybe, bool isList) : this(name, type)
    {
        IsMaybe = isMaybe;
        IsList = isList;
    }
}