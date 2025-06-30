namespace EasySharp.Core.Parsers;

/// 文法符号
public partial class Symbol(string name, SymbolTypeEnum symbolType)
{
    public string Name { get; } = name;
    public SymbolTypeEnum SymbolType { get; } = symbolType;

    public override bool Equals(object? obj)
    {
        return obj is Symbol other && Name == other.Name && SymbolType == other.SymbolType;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, SymbolType);
    }

    public override string ToString()
    {
        return Name;
    }
}

public class SymbolInstance(string mame, SymbolTypeEnum symbolType) : Symbol(mame, symbolType)
{
    public bool IsMaybe;
    public bool IsList;
    public Symbol? RepeatSymbol;
    public static SymbolInstance M(Symbol symbol) => new(symbol.Name, symbol.SymbolType, true, false);
    public static SymbolInstance L(Symbol symbol) => new(symbol.Name, symbol.SymbolType, false, true);
    public static SymbolInstance L(Symbol symbol, Symbol repeat) 
        => new(symbol.Name, symbol.SymbolType, false, true, repeat);
    public static SymbolInstance ML(Symbol symbol) => new(symbol.Name, symbol.SymbolType, true, true);

    public SymbolInstance(string name, SymbolTypeEnum symbolType, bool isMaybe, bool isList) : this(name, symbolType)
    {
        IsMaybe = isMaybe;
        IsList = isList;
    }
    
    public SymbolInstance(string name, SymbolTypeEnum symbolType, bool isMaybe, bool isList, Symbol repeat) : this(name, symbolType)
    {
        IsMaybe = isMaybe;
        IsList = isList;
        RepeatSymbol = repeat;       
    }
}