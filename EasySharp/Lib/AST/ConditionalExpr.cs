using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class ConditionalExpr(ASTNode condition, ASTNode trueExpr, ASTNode falseExpr) : ASTNode
{
    public ASTNode Condition { get; } = condition;
    public ASTNode TrueExpr { get; } = trueExpr;
    public ASTNode FalseExpr { get; } = falseExpr;

    public override string NodeType => "ConditionalExpr";
    
    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{TrueExpr.Print(indent + 1)}\n" +
               $"{FalseExpr.Print(indent + 1)}\n";
    }
}