using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class LambdaExprNode(ASTNode param, ASTNode body) : ASTNode
{
    public override string NodeType => "LambdaExpr";
    public ASTNode Param { get; } = param;
    public ASTNode? ReturnType { get; }
    public ASTNode Body { get; } = body;

    public LambdaExprNode(ASTNode param, ASTNode returnType, ASTNode body) : this(param, body)
    {
        ReturnType = returnType;
    }

    public override string ToTreeString(int indent = 0)
    {
        string hasReturnType = ReturnType != null ? "\n" : "";
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Param.ToTreeString(indent + 2)}\n" +
               $"{ReturnType?.ToTreeString(indent + 2)} [Return]{hasReturnType}" +
               $"{Body.ToTreeString(indent + 2)}";
    }
}