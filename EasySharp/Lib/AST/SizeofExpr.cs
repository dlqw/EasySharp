using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class SizeofExpr(ASTNode type) : ASTNode
{
    public override string NodeType => "SizeofExpr";
    public ASTNode Type { get; } = type;

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Type.Print(indent + 2)}";
    }
}