using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class AssignExprNode(string @operator, ASTNode left, ASTNode right) : ASTNode
{
    public override string NodeType => "AssignExpr";
    public string Operator { get; } = @operator;
    public ASTNode Left { get; } = left;
    public ASTNode Right { get; } = right;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Operator}\n" +
               $"{Left.ToTreeString(indent + 2)}\n" +
               $"{Right.ToTreeString(indent + 2)}";
    }
}