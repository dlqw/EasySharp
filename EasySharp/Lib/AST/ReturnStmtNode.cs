using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class ReturnStmtNode(ASTNode expression) : ASTNode
{
    public override string NodeType => "ReturnStmt";
    public ASTNode Expression { get; } = expression;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Expression.ToTreeString(indent + 2)}";
    }
}