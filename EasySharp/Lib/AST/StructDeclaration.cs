using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class StructDeclaration(List<Modifier> modifierList, ASTNode id, ASTNode structScope) : ASTNode
{
    public List<Modifier> ModifierList { get; set; } = modifierList;
    public ASTNode Id = id;
    public ASTNode StructScope { get; set; } = structScope;
    public override string NodeType => "StructDeclaration";
    
}