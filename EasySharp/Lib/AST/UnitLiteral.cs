namespace EasySharp.Core.Parsers;

public class UnitLiteral : ASTNode
{
    public override string NodeType => "UnitLiteral";
    public static UnitLiteral Instance { get; } = new();

    private UnitLiteral()
    {
    }
}