using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class DefaultCaseExprNode(ASTNode expr) : ASTNode
{
    public override string NodeType => "DefaultCaseExpr";
    public ASTNode Expression { get; } = expr;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Expression.ToTreeString(indent + 2)}";
    }
}