using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class MemberAccess(ASTNode left, ASTNode right) : ASTNode
{
    public override string NodeType => "MemberAccessExpr";
    public ASTNode Left { get; } = left;
    public ASTNode Right { get; } = right;

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Left.Print(indent + 2)}\n" +
               $"{Right.Print(indent + 2)}";
    }
}