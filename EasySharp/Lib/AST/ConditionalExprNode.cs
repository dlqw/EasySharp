using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class ConditionalExprNode(ASTNode condition, ASTNode trueExpr, ASTNode falseExpr) : ASTNode
{
    public override string NodeType => "ConditionalExpr";
    public ASTNode Condition { get; } = condition;
    public ASTNode TrueExpression { get; } = trueExpr;
    public ASTNode FalseExpression { get; } = falseExpr;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Condition.ToTreeString(indent + 2)}\n" +
               $"{TrueExpression.ToTreeString(indent + 2)}\n" +
               $"{FalseExpression.ToTreeString(indent + 2)}";
    }
}