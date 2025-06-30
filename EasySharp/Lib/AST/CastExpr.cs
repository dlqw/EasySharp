using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class CastExpr(ASTNode expression, ASTNode type) : ASTNode
{
    public override string NodeType => "CastExpr";
    public ASTNode Expression { get; } = expression;
    public ASTNode Type { get; } = type;

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Expression.Print(indent + 2)}\n" +
               $"{Type.Print(indent + 2)}";
    }
}