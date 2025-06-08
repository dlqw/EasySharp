using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class FileScope(ASTNode namespaceScope, ASTNode? useStmt = null) : ASTNode
{
    public ASTNode? UseStmt { get; set; } = useStmt;
    public ASTNode NamespaceScope { get; set; } = namespaceScope;
    public override string NodeType => "FileScope";
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

public class StructMember(StructMember.MemberType member) : ASTNode
{
    public enum MemberType { Variable, Function, }
    public MemberType Member { get; set; } = member;
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