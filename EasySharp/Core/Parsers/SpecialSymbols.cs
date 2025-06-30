namespace EasySharp.Core.Parsers;

/// 特殊符号常量
public static class SpecialSymbols
{
    public static readonly Symbol Epsilon = new Symbol("ε", SymbolTypeEnum.Terminal); // 空串
    public static readonly Symbol EndMarker = new Symbol("$", SymbolTypeEnum.Terminal); // 输入结束符
    public static readonly Symbol AugmentedStart = new Symbol("S'", SymbolTypeEnum.NonTerminal); // 增广起始符
}