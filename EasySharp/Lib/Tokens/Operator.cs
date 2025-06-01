using EasySharp.Utility;

// ReSharper disable once CheckNamespace
namespace EasySharp.Core;

public partial class Token
{
    public static readonly BiDictionary<string, TokenType> Operators = new()
    {
        { "+", TokenType.Plus },
        { "-", TokenType.Minus },
        { "*", TokenType.Multiply },
        { "/", TokenType.Divide },
        { "%", TokenType.Modulus },
        { "++", TokenType.PlusPlus },
        { "--", TokenType.MinusMinus },
        { "=", TokenType.Assign },
        { "+=", TokenType.PlusAssign },
        { "-=", TokenType.MinusAssign },
        { "*=", TokenType.MultiplyAssign },
        { "/=", TokenType.DivideAssign },
        { "%=", TokenType.ModulusAssign },
        { "==", TokenType.Equal },
        { "!=", TokenType.NotEqual },
        { ">", TokenType.Greater },
        { ">=", TokenType.GreaterEqual },
        { "<", TokenType.Less },
        { "<=", TokenType.LessEqual },
        { "&&", TokenType.And },
        { "||", TokenType.Or },
        { "!", TokenType.Not },
        { "&", TokenType.BitwiseAnd },
        { "|", TokenType.BitwiseOr },
        { "^", TokenType.BitwiseXor },
        { "~", TokenType.BitwiseNot },
        { "<<", TokenType.ShiftLeft },
        { ">>", TokenType.ShiftRight },
        { "&=", TokenType.BitwiseAndAssign },
        { "|=", TokenType.BitwiseOrAssign },
        { "^=", TokenType.BitwiseXorAssign },
        { "<<=", TokenType.ShiftLeftAssign },
        { ">>=", TokenType.ShiftRightAssign },
        { "->", TokenType.RightArrow },
        { "<-", TokenType.LeftArrow },
        { ".", TokenType.Dot },
        { "?", TokenType.QuestionMark },
        { "?.", TokenType.QuestionDot },
        { "??", TokenType.QuestionQuestion },
        { ":", TokenType.Colon },
        { "::", TokenType.DoubleColon },
        { "#", TokenType.Hash },
        { "@", TokenType.At },
        { "$", TokenType.Dollar }
    };

    public static readonly HashSet<char> OperatorsChars = new()
    {
        '+',
        '-',
        '*',
        '/',
        '=',
        '~',
        '!',
        '@',
        '#',
        '$',
        '%',
        '^',
        '&',
        '>',
        '<',
        '.',
        '?',
        ':',
    };
}