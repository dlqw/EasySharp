using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class ArgNode(ASTNode left, ASTNode? next = null) : AstListNode(left, next)
{
    public override string NodeType => "Arg";
}