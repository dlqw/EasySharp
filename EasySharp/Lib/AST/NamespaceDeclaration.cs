using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class NamespaceDeclaration(ASTNode namespaceStmt, List<StructDeclaration> structList) : ASTNode
{
    public ASTNode NamespaceStmt { get; set; } = namespaceStmt;
    public List<StructDeclaration> StructList { get; set; } = structList;
    public override string NodeType => "NamespaceDeclaration";
    
}