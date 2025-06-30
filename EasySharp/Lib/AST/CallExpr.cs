using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class CallExpr(ASTNode callee, ASTNode args) : ASTNode
{
    public override string NodeType => "CallExpr";
    public ASTNode Callee { get; } = callee;
    public ASTNode Args { get; } = args;

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Callee.Print(indent + 2)}\n" +
               $"{Args.Print(indent + 2)}";
    }
}