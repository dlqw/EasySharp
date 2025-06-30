using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class VarDeclaration(List<Modifier> modifierList, ASTNode annotation, ASTNode expression) : ASTNode
{
    public List<Modifier> ModifierList { get; set; } = modifierList;
    public ASTNode Annotation = annotation;
    public ASTNode Expression { get; set; } = expression;
    public override string NodeType => "VarDeclaration";

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n"
               + $"{new string(' ', indent + 2)}Modifier:{string.Join(", ", ModifierList.Select(m => m.Value))}\n"
               + Annotation.Print(indent + 2)
               + Expression.Print(indent + 2);
    }
}