using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

/// <summary>
/// 括号表达式节点
/// </summary>
/// <param name="expr">表达式</param>
public class ParenExprNode(ASTNode expr) : ASTNode
{
    public override string NodeType => "ParenExpr";
    public ASTNode Expression { get; } = expr;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Expression.ToTreeString(indent + 2)}";
    }
}

public class CastExprNode(ASTNode expression, ASTNode type) : ASTNode
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

public class NewExprNode(ASTNode constructor) : ASTNode
{
    public override string NodeType => "NewExpr";
    public ASTNode Constructor { get; } = constructor;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Constructor.ToTreeString(indent + 2)}\n";
    }
}

public class SizeofExprNode(ASTNode type) : ASTNode
{
    public override string NodeType => "SizeofExpr";
    public ASTNode Type { get; } = type;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Type.ToTreeString(indent + 2)}";
    }
}

public class ConstructorExprNode(ASTNode type, ASTNode args) : ASTNode
{
    public override string NodeType => "ConstructorExpr";
    public ASTNode Type { get; } = type;
    public ASTNode Args { get; } = args;

    public override string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Type.ToTreeString(indent + 2)}\n" +
               $"{Args.ToTreeString(indent + 2)}";
    }
}

public class MemberAccessNode(ASTNode left, ASTNode right) : ASTNode
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

public class CallExprNode(ASTNode callee, ASTNode args) : ASTNode
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

public class ArrayAccessNode(ASTNode array, ASTNode index) : ASTNode
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