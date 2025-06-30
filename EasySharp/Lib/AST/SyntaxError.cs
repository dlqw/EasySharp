using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class SyntaxError(string msg) : ASTNode
{
    private string Msg { get; } = msg;
    public override string NodeType => "SyntaxError";

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Msg}";
    }
}