using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class LambdaExpr(ASTNode param, ASTNode body) : ASTNode
{
    public override string NodeType => "LambdaExpr";
    public ASTNode Param { get; } = param;
    public ASTNode? ReturnType { get; }
    public ASTNode Body { get; } = body;

    public LambdaExpr(ASTNode param, ASTNode returnType, ASTNode body) : this(param, body)
    {
        ReturnType = returnType;
    }

    public override string Print(int indent = 0)
    {
        string hasReturnType = ReturnType != null ? "\n" : "";
        return $"{new string(' ', indent)}{NodeType}\n" +
               $"{Param.Print(indent + 2)}\n" +
               $"{ReturnType?.Print(indent + 2)} [Return]{hasReturnType}" +
               $"{Body.Print(indent + 2)}";
    }
}