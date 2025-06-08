using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class Modifier(string value) : ASTNode
{
    public override string NodeType => "Modifier";
    public string Value { get; } = value;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Value}";
    }
}