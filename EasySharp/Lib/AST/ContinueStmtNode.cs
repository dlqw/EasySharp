using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class ContinueStmtNode : ASTNode
{
    public override string NodeType => "ContinueStmt";

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}";
    }
}