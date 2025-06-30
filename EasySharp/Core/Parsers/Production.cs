namespace EasySharp.Core.Parsers;

/// 产生式
public class Production(
    Symbol left,
    List<Symbol> right,
    Func<ASTNode[], ASTNode> semanticAction)
{
    public Symbol Left { get; } = left;
    public List<Symbol> Right { get; } = right;
    public int ProductionNumber { get; } = 0;
    public Func<ASTNode[], ASTNode> SemanticAction { get; } = semanticAction;
    private static readonly Func<ASTNode[], ASTNode> DefaultSemanticAction = nodes => nodes[0];

    public Production(Symbol left, List<Symbol> right, int productionNumber, Func<ASTNode[], ASTNode> semanticAction) : this(left, right, semanticAction)
    {
        Left = left;
        Right = right;
        ProductionNumber = productionNumber;
        SemanticAction = semanticAction;
    }
    
    public Production(Symbol left, List<Symbol> right) : this(left, right, DefaultSemanticAction)
    {
        Left = left;
        Right = right;
        SemanticAction = DefaultSemanticAction;
    }
    
    public override string ToString()
    {
        return $"{Left} -> {string.Join(" ", Right)}";
    }
}