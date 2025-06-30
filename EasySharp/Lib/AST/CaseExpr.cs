using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class CaseExpr(ASTNode pattern, ASTNode expr) : ASTNode
{
    public ASTNode Pattern { get; } = pattern;
    public ASTNode Expr { get; } = expr;
    public override string NodeType => "CaseExpr";
    
    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{new string(' ', indent + 2)}{Pattern}\n" +
               $"{new string(' ', indent + 2)}{Expr}\n";
    }
}