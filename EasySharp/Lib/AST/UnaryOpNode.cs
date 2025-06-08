using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class UnaryOpNode(string op, ASTNode expr) : ASTNode
{
    public override string NodeType => "UnaryOp";
    public string Operator { get; } = op;
    public ASTNode Expression { get; } = expr;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Operator}\n" +
               $"{Expression.ToTreeString(indent + 2)}";
    }
}