using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class StructScope(List<StructMember> structMemberList) : ASTNode
{
    public List<StructMember> StructMemberList { get; set; } = structMemberList;
    public override string NodeType => "StructScope";
}