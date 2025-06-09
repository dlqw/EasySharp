using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class ModifierList(ASTNode left, ASTNode? next = null) : AstListNode(left, next)
{
    public override string NodeType => "ModifierList";
}