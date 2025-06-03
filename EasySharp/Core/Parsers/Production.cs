namespace EasySharp.Core.Parsers;

/// 产生式
public class Production(
    Symbol left,
    List<Symbol> right,
    int productionNumber,
    Func<ASTNode[], ASTNode> semanticAction)
{
    public Symbol Left { get; } = left;
    public List<Symbol> Right { get; } = right;
    public int ProductionNumber { get; } = productionNumber;
    public Func<ASTNode[], ASTNode> SemanticAction { get; } = semanticAction;

    public override string ToString()
    {
        return $"{Left} -> {string.Join(" ", Right)}";
    }
}