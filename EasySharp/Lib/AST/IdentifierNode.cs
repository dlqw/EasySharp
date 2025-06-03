using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class IdentifierNode(string name) : ASTNode
{
    public override string NodeType => "Id";
    public string Name { get; } = name;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Name}";
    }
}