namespace EasySharp.Core.Parsers;

/// 特殊符号常量
public static class SpecialSymbols
{
    public static readonly Symbol Epsilon = new Symbol("ε", SymbolType.Terminal); // 空串
    public static readonly Symbol EndMarker = new Symbol("$", SymbolType.Terminal); // 输入结束符
    public static readonly Symbol AugmentedStart = new Symbol("S'", SymbolType.NonTerminal); // 增广起始符
}