using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class AssignExpr(AssignExpr.AssignTypeEnum type, ASTNode leftNode, ASTNode astNode)
    : ASTNode
{
    public enum AssignTypeEnum
    {
        Assign,
        AddAssign,
        SubAssign,
        MulAssign,
        DivAssign,
        ModAssign,
        BitAndAssign,
        BitOrAssign,
        BitXorAssign,
        ShiftLeftAssign,
        ShiftRightAssign,
        MovAssign,
    }

    public override string NodeType => $"AssignExpr({AssignType})";
    public AssignTypeEnum AssignType { get; } = type;
    public ASTNode LeftNode { get; } = leftNode;
    public ASTNode RightNode { get; } = astNode;
}