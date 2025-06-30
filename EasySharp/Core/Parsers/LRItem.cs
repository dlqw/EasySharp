namespace EasySharp.Core.Parsers;

/// LR(1)项
public class LRItem(Production production, int dotPosition, Symbol lookahead)
{
    public Production Production { get; } = production;
    public int DotPosition { get; } = dotPosition;
    public Symbol Lookahead { get; } = lookahead;

    public bool IsShiftItem => DotPosition < Production.Right.Count;
    public Symbol? NextSymbol => IsShiftItem ? Production.Right[DotPosition] : null;
    public bool IsReduceItem => DotPosition == Production.Right.Count; // 归约项

    public LRItem Advance()
    {
        return new LRItem(Production, DotPosition + 1, Lookahead);
    }

    /// LR(0)项的核心，用于LALR(1)合并
    public override int GetHashCode()
    {
        return HashCode.Combine(Production.ProductionNumber, DotPosition);
    }

    public override bool Equals(object? obj)
    {
        if (obj is LRItem other)
        {
            // 比较核心部分和向前看符号
            return Production.ProductionNumber == other.Production.ProductionNumber &&
                   DotPosition == other.DotPosition &&
                   Lookahead.Equals(other.Lookahead);
        }

        return false;
    }

    /// 只比较核心部分，不考虑向前看符号
    public bool CoreEquals(LRItem other)
    {
        return Production.ProductionNumber == other.Production.ProductionNumber &&
               DotPosition == other.DotPosition;
    }

    public override string ToString()
    {
        var parts = Production.Right.ToList();
        parts.Insert(DotPosition, new Symbol("•", SymbolTypeEnum.Terminal));
        return $"{Production.Left} -> {string.Join(" ", parts)}, {Lookahead}";
    }
}