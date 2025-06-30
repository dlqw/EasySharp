using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class Annotation(ASTNode id, ASTNode qualifiedName) : ASTNode
{
    public ASTNode Id { get; set; } = id;
    public ASTNode QualifiedName { get; set; } = qualifiedName;
    public override string NodeType => "Annotation";

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               Id.Print(indent + 2) +
               QualifiedName.Print(indent + 2);
    }
}