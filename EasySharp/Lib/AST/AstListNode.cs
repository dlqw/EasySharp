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
    
}