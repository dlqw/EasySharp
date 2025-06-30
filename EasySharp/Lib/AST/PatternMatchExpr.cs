using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class PatternMatchExpr : ASTNode
{
    public ASTNode Expr { get; set; }
    public List<CaseExpr> CaseExprList { get; }
    public override string NodeType => "PatternMatchExpr";

    public PatternMatchExpr(ASTNode expr, List<CaseExpr> caseExprList)
    {
        Expr = expr;
        CaseExprList = caseExprList;
    }
}