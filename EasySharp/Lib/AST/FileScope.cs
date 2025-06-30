using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class FileScope : ASTNode
{
    public List<UseStmt>? UseStmtList { get; set; }
    public List<NamespaceDeclaration>? NamespaceDeclarationList { get; set; }
    public ASTNode? FileScopedFileScopedNamespaceDeclaration { get; set; }
    public bool IsFileScoped { get; } = false;
    public override string NodeType => "FileScope";

    public FileScope(List<UseStmt> useStmtList, List<NamespaceDeclaration> namespaceDeclarationList)
    {
        UseStmtList = useStmtList;
        NamespaceDeclarationList = namespaceDeclarationList;
    }

    public FileScope(List<UseStmt> useStmtList, ASTNode fileScopedFileScopedNamespaceDeclaration)
    {
        UseStmtList = useStmtList;
        FileScopedFileScopedNamespaceDeclaration = fileScopedFileScopedNamespaceDeclaration;
        IsFileScoped = true;
    }

    public FileScope(List<NamespaceDeclaration> namespaceDeclarationList)
    {
        NamespaceDeclarationList = namespaceDeclarationList;
    }

    public FileScope(ASTNode fileScopedFileScopedNamespaceDeclaration)
    {
        FileScopedFileScopedNamespaceDeclaration = fileScopedFileScopedNamespaceDeclaration;
        IsFileScoped = true;
    }
}