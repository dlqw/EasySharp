using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public abstract class AstListNode(ASTNode left, ASTNode? next = null) : ASTNode
{
    public ASTNode Left { get; } = left;
    public ASTNode? Next { get; } = next;

    public abstract override string NodeType { get; }

    public override string ToTreeString(int indent = 0)
    {
        if (Next == null)
        {
            return $"{new string(' ', indent)}{NodeType}\n" +
                   $"{Left.ToTreeString(indent + 2)}";
        }

        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Left.ToTreeString(indent + 2)}\n" +
               $"{Next?.ToTreeString(indent + 2)}";
    }
}