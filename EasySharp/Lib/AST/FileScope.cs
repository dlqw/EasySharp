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

public class NamespaceDeclaration(ASTNode namespaceStmt, List<StructDeclaration> structList) : ASTNode
{
    public ASTNode NamespaceStmt { get; set; } = namespaceStmt;
    public  List<StructDeclaration> StructList { get; set; } = structList;
    public override string NodeType => "NamespaceScope";
}

public class NamespaceStmt(ASTNode namespaceName) : ASTNode
{
    public ASTNode NamespaceName { get; set; } = namespaceName;
    public override string NodeType => "NamespaceStmt";
}

public class NamespaceDeclarationList(ASTNode left, ASTNode? next = null) : AstListNode(left, next)
{
    public override string NodeType => "NamespaceScopeList";   
}

public class NamespaceName() : ASTNode
{
    public override string NodeType => "NamespaceName";
}

public class UseStmt(ASTNode namespaceName) : ASTNode
{
    public ASTNode NamespaceName { get; set; } = namespaceName;
    public override string NodeType => "UseStmt";
}

public class UseStmtList(ASTNode left, ASTNode? next = null) : AstListNode(left, next)
{
    public override string NodeType => "UseStmtList";
}

public class StructDeclarationList(ASTNode left, ASTNode? next = null) : AstListNode(left, next)
{
    public override string NodeType => "StructScopeList";
}

public class SyntaxError(string msg) : ASTNode
{
    private string Msg { get; } = msg;
    public override string NodeType => "SyntaxError";

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Msg}";   
    }
}

public class StructMember(StructMember.MemberType member, ASTNode o) : ASTNode
{
    public enum MemberType { Variable, Function, }
    public MemberType Member { get; set; } = member;
    public ASTNode Object { get; set; } = o;
    public override string NodeType => "StructMember"; 
}

public class StructMemberList(ASTNode left, ASTNode? next = null) : AstListNode(left, next)
{
    public override string NodeType => "StructMemberList";
}

public class StructScope(List<StructMember> structMemberList) : ASTNode
{
    public List<StructMember> StructMemberList { get; set; } = structMemberList;
    public override string NodeType => "StructScope";  
}
public class StructDeclaration(List<Modifier> modifierList, ASTNode id, ASTNode structScope) : ASTNode
{
    public List<Modifier> ModifierList { get; set; } = modifierList;
    public ASTNode Id = id;
    public ASTNode StructScope { get; set; } = structScope; 
    public override string NodeType => "StructDeclaration";
}

public class VarDeclaration(List<Modifier> modifierList, ASTNode id, ASTNode expression) : ASTNode
{
    public List<Modifier> ModifierList { get; set; } = modifierList;
    public ASTNode Id = id;
    public ASTNode Expression { get; set; } = expression;
    public override string NodeType => "VarDeclaration";
}

public class Id(ASTNode name) : ASTNode
{
    public ASTNode Name { get; set; } = name;
    public override string NodeType => "Id";
}

public class Annotation(ASTNode name, ASTNode type) : Id(name)
{
    public ASTNode Type { get; set; } = type;
    public override string NodeType => "Annotation";
}

public class Param(List<Annotation> annotationList) : ASTNode
{
    public List<Annotation> AnnotationList { get; set; } = annotationList;
    public override string NodeType => "Param";
}