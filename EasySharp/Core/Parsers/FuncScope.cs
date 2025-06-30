using EasySharp.Lib.AST;

namespace EasySharp.Core.Parsers;

public class FuncScope : ASTNode
{
    public ASTNode BlockScope { get; }
    public ASTNode ExprScope { get; } = UnitLiteral.Instance;

    public FuncScope(ASTNode blockScope, ASTNode astNode)
    {
        BlockScope = blockScope;
        ExprScope = astNode;
    }

    public FuncScope(ASTNode blockScope)
    {
        BlockScope = blockScope;
    }

    public override string NodeType => "FuncScope";
}