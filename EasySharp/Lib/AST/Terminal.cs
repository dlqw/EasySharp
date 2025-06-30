using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class Terminal(string symbolName, string value) : ASTNode
{
    public override string NodeType => $"Terminal:{SymbolName}";
    public string SymbolName { get; } = symbolName;
    public string Value { get; } = value;

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Value}\n";
    }
}