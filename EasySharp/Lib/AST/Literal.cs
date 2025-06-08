using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class Literal(Literal.LiteralType type) : ASTNode
{
    public enum LiteralType
    {
        String,
        Char,
        Integer,
        Float,
        Boolean,
        Unit,
        Null
    }
    public override string NodeType => "Literal";
    public LiteralType Type { get; } = type;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Type}\n";
    }
}