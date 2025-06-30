using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class UseStmt(ASTNode namespaceName) : ASTNode
{
    public ASTNode NamespaceName { get; set; } = namespaceName;
    public override string NodeType => "UseStmt";
}