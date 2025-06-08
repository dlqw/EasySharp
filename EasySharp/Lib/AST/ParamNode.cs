using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class ParamNode(ASTNode left, ASTNode? next = null) : AstListNode(left, next)
{
    public override string NodeType => "Param";
}