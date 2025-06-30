using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class UnitLiteral : ASTNode
{
    public override string NodeType => "UnitLiteral";
    public static UnitLiteral Instance { get; } = new();

    private UnitLiteral()
    {
    }
}