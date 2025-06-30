using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class ArrayAccess(ASTNode array, ASTNode index) : ASTNode
{
    public override string NodeType => "ArrayAccessExpr";
    public ASTNode Array { get; } = array;
    public ASTNode Index { get; } = index;
    
}