using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class BlockNode(ASTNode? statements = null, ASTNode? returnNode = null) : ASTNode
{
    public override string NodeType => "Block";
    public ASTNode? Statements { get; } = statements;
    public ASTNode? ReturnNode { get; } = returnNode;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Statements?.ToTreeString(indent + 2)}\n" +
               $"{ReturnNode?.ToTreeString(indent + 2)}";
    }
}