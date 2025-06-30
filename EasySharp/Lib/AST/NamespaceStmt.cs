using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class NamespaceStmt(ASTNode namespaceName) : ASTNode
{
    public ASTNode NamespaceName { get; set; } = namespaceName;
    public override string NodeType => "NamespaceStmt";
    
}