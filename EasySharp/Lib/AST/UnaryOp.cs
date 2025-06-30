using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class UnaryOp(string op, ASTNode expr) : ASTNode
{
    public override string NodeType => "UnaryOp";
    public string Operator { get; } = op;
    public ASTNode Expression { get; } = expr;

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Operator}\n" +
               $"{Expression.Print(indent + 2)}";
    }
}