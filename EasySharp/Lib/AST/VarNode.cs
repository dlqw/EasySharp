using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class VarNode(ASTNode name, ASTNode type) : ASTNode
{
    public override string NodeType => "Var";
    public ASTNode? Modifier { get; }
    public ASTNode Name { get; } = name;
    public ASTNode Type { get; } = type;

    public VarNode(ASTNode modifier, ASTNode name, ASTNode type) : this(name, type)
    {
        Modifier = modifier;
    }

    public override string ToTreeString(int indent = 0)
    {
        string hasModifier = Modifier != null ? "\n" : "";
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Modifier?.ToTreeString(indent + 2)}{hasModifier}" +
               $"{Name.ToTreeString(indent + 2)}\n" +
               $"{Type.ToTreeString(indent + 2)}";
    }
}