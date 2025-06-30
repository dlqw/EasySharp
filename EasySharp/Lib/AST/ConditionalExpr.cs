using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class ConditionalExpr(ASTNode condition, ASTNode trueExpr, ASTNode falseExpr) : ASTNode
{
    public ASTNode Condition { get; } = condition;
    public ASTNode TrueExpr { get; } = trueExpr;
    public ASTNode FalseExpr { get; } = falseExpr;

    public override string NodeType => "ConditionalExpr";
}

public class CaseExpr(ASTNode pattern, ASTNode expr) : ASTNode
{
    public ASTNode Pattern { get; } = pattern;
    public ASTNode Expr { get; } = expr;
    public override string NodeType => "CaseExpr";
}

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

public class BinaryOp(string op, ASTNode left, ASTNode right) : ASTNode
{
    public override string NodeType => "BinaryOp";
    public string Operator { get; } = op;
    public ASTNode Left { get; } = left;
    public ASTNode Right { get; } = right;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Operator}\n" +
               $"{Left.ToTreeString(indent + 2)}\n" +
               $"{Right.ToTreeString(indent + 2)}";
    }
}

public class UnaryOp(string op, ASTNode expr) : ASTNode
{
    public override string NodeType => "UnaryOp";
    public string Operator { get; } = op;
    public ASTNode Expression { get; } = expr;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Operator}\n" +
               $"{Expression.ToTreeString(indent + 2)}";
    }
}

public class CastExpr(ASTNode expression, ASTNode type) : ASTNode
{
    public override string NodeType => "CastExpr";
    public ASTNode Expression { get; } = expression;
    public ASTNode Type { get; } = type;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Expression.ToTreeString(indent + 2)}\n" +
               $"{Type.ToTreeString(indent + 2)}";
    }
}

public class SizeofExpr(ASTNode type) : ASTNode
{
    public override string NodeType => "SizeofExpr";
    public ASTNode Type { get; } = type;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Type.ToTreeString(indent + 2)}";
    }
}


public class MemberAccess(ASTNode left, ASTNode right) : ASTNode
{
    public override string NodeType => "MemberAccessExpr";
    public ASTNode Left { get; } = left;
    public ASTNode Right { get; } = right;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Left.ToTreeString(indent + 2)}\n" +
               $"{Right.ToTreeString(indent + 2)}";
    }
}

public class CallExpr(ASTNode callee, ASTNode args) : ASTNode
{
    public override string NodeType => "CallExpr";
    public ASTNode Callee { get; } = callee;
    public ASTNode Args { get; } = args;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Callee.ToTreeString(indent + 2)}\n" +
               $"{Args.ToTreeString(indent + 2)}";
    }
}

public class ArrayAccess(ASTNode array, ASTNode index) : ASTNode
{
    public override string NodeType => "ArrayAccessExpr";
    public ASTNode Array { get; } = array;
    public ASTNode Index { get; } = index;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Array.ToTreeString(indent + 2)}\n" +
               $"{Index.ToTreeString(indent + 2)}";
    }
}