using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class BinaryOp(string op, ASTNode left, ASTNode right) : ASTNode
{
    public override string NodeType => "BinaryOp";
    public string Operator { get; } = op;
    public ASTNode Left { get; } = left;
    public ASTNode Right { get; } = right;

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Operator}\n" +
               $"{Left.Print(indent + 2)}\n" +
               $"{Right.Print(indent + 2)}";
    }
}