using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class StructMember(StructMember.MemberType member, ASTNode o) : ASTNode
{
    public enum MemberType
    {
        Variable,
        Function,
    }

    public MemberType Member { get; set; } = member;
    public ASTNode Object { get; set; } = o;
    public override string NodeType => "StructMember";
}