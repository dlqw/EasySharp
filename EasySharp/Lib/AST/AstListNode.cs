using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class AstListNode : ASTNode
{
    public ASTNode Left { get; }
    public ASTNode? Next { get; }

    public AstListNode(string name, ASTNode left)
    {
        NodeType = name;
        Left = left;
    }

    public AstListNode(string name, ASTNode left, ASTNode next)
    {
        NodeType = name;
        Left = left;
        Next = next;
    }

    public override string NodeType { get; } = "";

    public override string ToTreeString(int indent = 0)
    {
        if (Next == null)
        {
            return $"{new string(' ', indent)}{NodeType}List\n" +
                   $"{Left.ToTreeString(indent + 2)}";
        }

        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Left.ToTreeString(indent + 2)}\n" +
               $"{Next?.ToTreeString(indent + 2)}";
    }
}