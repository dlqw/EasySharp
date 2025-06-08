using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class BreakStmtNode : ASTNode
{
    public override string NodeType => "BreakStmt";

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}";
    }
}