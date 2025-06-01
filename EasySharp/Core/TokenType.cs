namespace EasySharp.Core;

public enum TokenType
{
    #region 关键字

    Let,
    Var,
    
    Null,
    Unit,
    
    Func,
    Marco,
    
    Static,
    Const,
    Readonly,
    
    Loop,
    Break,
    Continue,
    
    If,
    Then,
    Else,
    Switch,
    Case,
    
    Enum,
    Struct,
    Private,
    Public,
    Protected,
    Internal,
    Friend,
    
    Async,
    Await,
    Promise,
    Do,
    Try,
    As,
    
    Move,
    Self,
    Use,
    Yield,
    Virtual,
    Abstract,
    

    #endregion
    
    #region 算数运算符

    /// +
    Plus,

    /// -
    Minus,

    /// *
    Multiply,

    /// /
    Divide,

    /// %
    Modulus,

    /// ++
    PlusPlus,

    /// --
    MinusMinus,

    #endregion

    #region 赋值运算符

    /// =
    Assign,

    /// +=
    PlusAssign,

    /// -=
    MinusAssign,

    /// *=
    MultiplyAssign,

    /// /=
    DivideAssign,

    /// %=
    ModulusAssign,

    /// &=
    BitwiseAndAssign,

    /// |=
    BitwiseOrAssign,

    /// ^=
    BitwiseXorAssign,

    /// <<=
    ShiftLeftAssign,

    /// >>=
    ShiftRightAssign,

    #endregion

    #region 比较运算符

    /// ==
    Equal,

    /// !=
    NotEqual,

    /// >
    Greater,

    /// >=
    GreaterEqual,

    /// <
    Less,

    /// <=
    LessEqual,

    #endregion

    #region 布尔运算符

    /// &&
    And,

    /// ||
    Or,

    /// !
    Not,

    #endregion

    #region 位运算符

    /// &
    BitwiseAnd,

    /// |
    BitwiseOr,

    /// ^
    BitwiseXor,

    /// ~
    BitwiseNot,

    /// <<
    ShiftLeft,

    /// >>
    ShiftRight,

    #endregion

    #region 其他运算符
    /// ->
    RightArrow,

    /// <-
    LeftArrow,

    /// .
    Dot,

    /// ?
    QuestionMark,

    /// ?.
    QuestionDot,

    /// ??
    QuestionQuestion,

    /// :
    Colon,

    /// ::
    DoubleColon,

    /// #
    Hash,

    /// @
    At,

    /// $
    Dollar,
    
    #endregion

    #region 分隔符

    /// (
    LeftParen,

    /// )
    RightParen,

    /// {
    LeftBrace,

    /// }
    RightBrace,

    /// [
    LeftBracket,

    /// ]
    RightBracket,

    /// ;
    Semicolon,

    /// ,
    Comma,

    #endregion

    Identifier,
    Int,
    Float,
    String,
    Char,
    Bool,
    Error,
    EOF,
}