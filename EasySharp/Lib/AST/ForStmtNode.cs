using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class ForStmtNode(ASTNode init, ASTNode condition, ASTNode increment, ASTNode body) : ASTNode
{
    public override string NodeType => "ForStmt";
    public ASTNode Init { get; } = init;
    public ASTNode Condition { get; } = condition;
    public ASTNode Increment { get; } = increment;
    public ASTNode Body { get; } = body;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Init.ToTreeString(indent + 2)}\n" +
               $"{Condition.ToTreeString(indent + 2)}\n" +
               $"{Increment.ToTreeString(indent + 2)}\n" +
               $"{Body.ToTreeString(indent + 2)}";
    }
}