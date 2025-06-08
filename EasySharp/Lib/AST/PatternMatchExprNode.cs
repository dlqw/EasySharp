using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class PatternMatchExprNode(ASTNode expr, ASTNode cases) : ASTNode
{
    public override string NodeType => "PatternMatchExpr";
    public ASTNode Expression { get; } = expr;
    public ASTNode Cases { get; } = cases;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Expression.ToTreeString(indent + 2)}\n" +
               $"{Cases.ToTreeString(indent + 2)}";
    }
}