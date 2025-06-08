using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class IfStmtNode(ASTNode condition, ASTNode body, ASTNode? elseBody = null) : ASTNode
{
    public override string NodeType => "IfStmt";
    public ASTNode Condition { get; } = condition;
    public ASTNode Body { get; } = body;
    public ASTNode? ElseBody { get; } = elseBody;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Condition.ToTreeString(indent + 2)}\n" +
               $"{Body.ToTreeString(indent + 2)}\n" +
               $"{ElseBody?.ToTreeString(indent + 2)}";
    }
}