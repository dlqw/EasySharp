using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class ExprListNode(ASTNode left) : ASTNode
{
    public override string NodeType => "ExprList";
    public ASTNode Left { get; } = left;
    public ASTNode? Next { get; }

    public ExprListNode(ASTNode left, ASTNode? next) : this(left)
    {
        Next = next;
    }

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Left.ToTreeString(indent + 2)}\n" +
               $"{Next?.ToTreeString(indent + 2)}";
    }
}