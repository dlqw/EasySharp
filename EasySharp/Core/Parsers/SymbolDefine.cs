using EasySharp.Lib.AST;
using EasySharp.Utility;
using S = EasySharp.Core.Parsers.SymbolInstance;

namespace EasySharp.Core.Parsers;

public partial class Symbol
{
    #region 定义符号

    #region 非终结符

    public static readonly Symbol FileScope = new("FileScope", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol StructScope = new("StructScope", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol FuncScope = new("FuncScope", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol ExprScope = new("ExprScope", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol BlockScope = new("BlockScope", SymbolTypeEnum.NonTerminal);

    public static readonly Symbol FileScopedNamespaceDeclaration = new("FileScopedNamespaceDeclaration",
        SymbolTypeEnum.NonTerminal);

    public static readonly Symbol NamespaceDeclaration = new("NamespaceDeclaration", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol StructDeclaration = new("StructDeclaration", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol FuncDeclaration = new("FuncDeclaration", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol VarDeclaration = new("VarDeclaration", SymbolTypeEnum.NonTerminal);

    #region Expr

    public static readonly Symbol Expr = new("Expr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol AssignExpr = new("AssignExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol LambdaExpr = new("LambdaExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol PatternMatchExpr = new("PatternMatchExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol CaseExpr = new("CaseExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol ConditionalExpr = new("ConditionalExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol LogicalOrExpr = new("LogicalOrExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol LogicalAndExpr = new("LogicalAndExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol BitwiseOrExpr = new("BitwiseOrExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol BitwiseXorExpr = new("BitwiseXorExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol BitwiseAndExpr = new("BitwiseAndExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol EqualityExpr = new("EqualityExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol ComparisonExpr = new("ComparisonExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol ShiftExpr = new("ShiftExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol AdditiveExpr = new("AdditiveExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol MultiplicativeExpr = new("MultiplicativeExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol UnaryExpr = new("UnaryExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol PostfixExpr = new("PostfixExpr", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol PrimaryExpr = new("PrimaryExpr", SymbolTypeEnum.NonTerminal);

    #endregion

    public static readonly Symbol Modifier = new("Modifier", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol Arg = new("Arg", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol Param = new("Param", SymbolTypeEnum.NonTerminal);

    public static readonly Symbol Annotation = new("Annotation", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol QualifiedName = new("QualifiedName", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol StructMember = new("StructMember", SymbolTypeEnum.NonTerminal);

    public static readonly Symbol Stmt = new("Stmt", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol ReturnStmt = new("ReturnStmt", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol UseStmt = new("UseStmt", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol NamespaceStmt = new("NamespaceStmt", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol CaseStmt = new("CaseStmt", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol DefaultStmt = new("DefaultStmt", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol IfStmt = new("IfStmt", SymbolTypeEnum.NonTerminal);
    public static readonly Symbol ElseStmt = new("ElseStmt", SymbolTypeEnum.NonTerminal);

    #endregion

    #region 终结符

    #region KeyWord

    public static readonly Symbol Let = new("let", SymbolTypeEnum.Terminal);
    public static readonly Symbol Var = new("var", SymbolTypeEnum.Terminal);
    public static readonly Symbol Null = new("null", SymbolTypeEnum.Terminal);
    public static readonly Symbol Unit = new("unit", SymbolTypeEnum.Terminal);
    public static readonly Symbol This = new("this", SymbolTypeEnum.Terminal);
    public static readonly Symbol Func = new("func", SymbolTypeEnum.Terminal);
    public static readonly Symbol Marco = new("marco", SymbolTypeEnum.Terminal);
    public static readonly Symbol Static = new("static", SymbolTypeEnum.Terminal);
    public static readonly Symbol Const = new("const", SymbolTypeEnum.Terminal);
    public static readonly Symbol Readonly = new("readonly", SymbolTypeEnum.Terminal);
    public static readonly Symbol Loop = new("loop", SymbolTypeEnum.Terminal);
    public static readonly Symbol Break = new("break", SymbolTypeEnum.Terminal);
    public static readonly Symbol Continue = new("continue", SymbolTypeEnum.Terminal);
    public static readonly Symbol If = new("if", SymbolTypeEnum.Terminal);
    public static readonly Symbol Then = new("then", SymbolTypeEnum.Terminal);
    public static readonly Symbol Else = new("else", SymbolTypeEnum.Terminal);
    public static readonly Symbol Switch = new("switch", SymbolTypeEnum.Terminal);
    public static readonly Symbol Case = new("case", SymbolTypeEnum.Terminal);
    public static readonly Symbol Enum = new("enum", SymbolTypeEnum.Terminal);
    public static readonly Symbol Struct = new("struct", SymbolTypeEnum.Terminal);
    public static readonly Symbol Private = new("private", SymbolTypeEnum.Terminal);
    public static readonly Symbol Public = new("public", SymbolTypeEnum.Terminal);
    public static readonly Symbol Protected = new("protected", SymbolTypeEnum.Terminal);
    public static readonly Symbol Internal = new("internal", SymbolTypeEnum.Terminal);
    public static readonly Symbol Friend = new("friend", SymbolTypeEnum.Terminal);
    public static readonly Symbol Async = new("async", SymbolTypeEnum.Terminal);
    public static readonly Symbol Await = new("await", SymbolTypeEnum.Terminal);
    public static readonly Symbol Promise = new("promise", SymbolTypeEnum.Terminal);
    public static readonly Symbol Do = new("do", SymbolTypeEnum.Terminal);
    public static readonly Symbol Try = new("try", SymbolTypeEnum.Terminal);
    public static readonly Symbol As = new("as", SymbolTypeEnum.Terminal);
    public static readonly Symbol Move = new("move", SymbolTypeEnum.Terminal);
    public static readonly Symbol Self = new("self", SymbolTypeEnum.Terminal);
    public static readonly Symbol Use = new("use", SymbolTypeEnum.Terminal);
    public static readonly Symbol Yield = new("yield", SymbolTypeEnum.Terminal);
    public static readonly Symbol Virtual = new("virtual", SymbolTypeEnum.Terminal);
    public static readonly Symbol Abstract = new("abstract", SymbolTypeEnum.Terminal);
    public static readonly Symbol Namespace = new("namespace", SymbolTypeEnum.Terminal);

    #endregion

    #region 算数运算符

    public static readonly Symbol Plus = new("+", SymbolTypeEnum.Terminal);
    public static readonly Symbol Minus = new("-", SymbolTypeEnum.Terminal);
    public static readonly Symbol Multiply = new("*", SymbolTypeEnum.Terminal);
    public static readonly Symbol Divide = new("/", SymbolTypeEnum.Terminal);
    public static readonly Symbol Modulus = new("%", SymbolTypeEnum.Terminal);
    public static readonly Symbol PlusPlus = new("++", SymbolTypeEnum.Terminal);
    public static readonly Symbol MinusMinus = new("--", SymbolTypeEnum.Terminal);

    #endregion

    #region 赋值运算符

    public static readonly Symbol Assign = new("=", SymbolTypeEnum.Terminal);
    public static readonly Symbol PlusAssign = new("+=", SymbolTypeEnum.Terminal);
    public static readonly Symbol MinusAssign = new("-=", SymbolTypeEnum.Terminal);
    public static readonly Symbol MultiplyAssign = new("*=", SymbolTypeEnum.Terminal);
    public static readonly Symbol DivideAssign = new("/=", SymbolTypeEnum.Terminal);
    public static readonly Symbol ModulusAssign = new("%=", SymbolTypeEnum.Terminal);
    public static readonly Symbol BitwiseAndAssign = new("&=", SymbolTypeEnum.Terminal);
    public static readonly Symbol BitwiseOrAssign = new("|=", SymbolTypeEnum.Terminal);
    public static readonly Symbol BitwiseXorAssign = new("^=", SymbolTypeEnum.Terminal);
    public static readonly Symbol ShiftLeftAssign = new("<<=", SymbolTypeEnum.Terminal);
    public static readonly Symbol ShiftRightAssign = new(">>=", SymbolTypeEnum.Terminal);

    #endregion

    #region 比较运算符

    public static readonly Symbol Equal = new("==", SymbolTypeEnum.Terminal);
    public static readonly Symbol NotEqual = new("!=", SymbolTypeEnum.Terminal);
    public static readonly Symbol Greater = new(">", SymbolTypeEnum.Terminal);
    public static readonly Symbol GreaterEqual = new(">=", SymbolTypeEnum.Terminal);
    public static readonly Symbol Less = new("<", SymbolTypeEnum.Terminal);
    public static readonly Symbol LessEqual = new("<=", SymbolTypeEnum.Terminal);

    #endregion

    public static readonly Symbol And = new("&&", SymbolTypeEnum.Terminal);
    public static readonly Symbol Or = new("||", SymbolTypeEnum.Terminal);
    public static readonly Symbol Not = new("!", SymbolTypeEnum.Terminal);
    public static readonly Symbol BitwiseAnd = new("&", SymbolTypeEnum.Terminal);
    public static readonly Symbol BitwiseOr = new("|", SymbolTypeEnum.Terminal);
    public static readonly Symbol BitwiseXor = new("^", SymbolTypeEnum.Terminal);
    public static readonly Symbol BitwiseNot = new("~", SymbolTypeEnum.Terminal);
    public static readonly Symbol ShiftLeft = new("<<", SymbolTypeEnum.Terminal);
    public static readonly Symbol ShiftRight = new(">>", SymbolTypeEnum.Terminal);
    public static readonly Symbol RightArrow = new("->", SymbolTypeEnum.Terminal);
    public static readonly Symbol LeftArrow = new("<-", SymbolTypeEnum.Terminal);
    public static readonly Symbol Dot = new(".", SymbolTypeEnum.Terminal);
    public static readonly Symbol QuestionMark = new("?", SymbolTypeEnum.Terminal);
    public static readonly Symbol QuestionDot = new("?.", SymbolTypeEnum.Terminal);
    public static readonly Symbol QuestionQuestion = new("??", SymbolTypeEnum.Terminal);
    public static readonly Symbol Colon = new(":", SymbolTypeEnum.Terminal);
    public static readonly Symbol DoubleColon = new("::", SymbolTypeEnum.Terminal);
    public static readonly Symbol Hash = new("#", SymbolTypeEnum.Terminal);
    public static readonly Symbol At = new("@", SymbolTypeEnum.Terminal);
    public static readonly Symbol Dollar = new("Dollar", SymbolTypeEnum.Terminal);

    #region 分隔符

    public static readonly Symbol LeftParen = new("(", SymbolTypeEnum.Terminal);
    public static readonly Symbol RightParen = new(")", SymbolTypeEnum.Terminal);
    public static readonly Symbol LeftBrace = new("{", SymbolTypeEnum.Terminal);
    public static readonly Symbol RightBrace = new("}", SymbolTypeEnum.Terminal);
    public static readonly Symbol LeftBracket = new("[", SymbolTypeEnum.Terminal);
    public static readonly Symbol RightBracket = new("]", SymbolTypeEnum.Terminal);
    public static readonly Symbol Semicolon = new(";", SymbolTypeEnum.Terminal);
    public static readonly Symbol Comma = new(",", SymbolTypeEnum.Terminal);

    #endregion

    public static readonly Symbol Id = new("Id", SymbolTypeEnum.Terminal);
    public static readonly Symbol IntLiteral = new("IntLiteral", SymbolTypeEnum.Terminal);
    public static readonly Symbol FloatLiteral = new("FloatLiteral", SymbolTypeEnum.Terminal);
    public static readonly Symbol StringLiteral = new("StringLiteral", SymbolTypeEnum.Terminal);
    public static readonly Symbol CharLiteral = new("CharLiteral", SymbolTypeEnum.Terminal);
    public static readonly Symbol BoolLiteral = new("BoolLiteral", SymbolTypeEnum.Terminal);
    public static readonly Symbol UnitLiteral = new("UnitLiteral", SymbolTypeEnum.Terminal);
    public static Symbol EOF => SpecialSymbols.EndMarker;

    #endregion

    public static readonly HashSet<Symbol> NTS = new();

    public static readonly BiDictionary<TokenType, Symbol> TS = new()
    {
        { TokenType.Let, Let },
        { TokenType.Var, Var },
        { TokenType.Null, Null },
        { TokenType.Unit, Unit },
        { TokenType.This, This },
        { TokenType.Func, Func },
        { TokenType.Marco, Marco },
        { TokenType.Static, Static },
        { TokenType.Const, Const },
        { TokenType.Readonly, Readonly },
        { TokenType.Loop, Loop },
        { TokenType.Break, Break },
        { TokenType.Continue, Continue },
        { TokenType.If, If },
        { TokenType.Then, Then },
        { TokenType.Else, Else },
        { TokenType.Switch, Switch },
        { TokenType.Case, Case },
        { TokenType.Enum, Enum },
        { TokenType.Struct, Struct },
        { TokenType.Private, Private },
        { TokenType.Public, Public },
        { TokenType.Protected, Protected },
        { TokenType.Internal, Internal },
        { TokenType.Friend, Friend },
        { TokenType.Async, Async },
        { TokenType.Await, Await },
        { TokenType.Promise, Promise },
        { TokenType.Do, Do },
        { TokenType.Try, Try },
        { TokenType.As, As },
        { TokenType.Move, Move },
        { TokenType.Self, Self },
        { TokenType.Use, Use },
        { TokenType.Yield, Yield },
        { TokenType.Virtual, Virtual },
        { TokenType.Abstract, Abstract },
        { TokenType.Namespace, Namespace },
        { TokenType.Plus, Plus },
        { TokenType.Minus, Minus },
        { TokenType.Multiply, Multiply },
        { TokenType.Divide, Divide },
        { TokenType.Modulus, Modulus },
        { TokenType.PlusPlus, PlusPlus },
        { TokenType.MinusMinus, MinusMinus },
        { TokenType.Assign, Assign },
        { TokenType.PlusAssign, PlusAssign },
        { TokenType.MinusAssign, MinusAssign },
        { TokenType.MultiplyAssign, MultiplyAssign },
        { TokenType.DivideAssign, DivideAssign },
        { TokenType.ModulusAssign, ModulusAssign },
        { TokenType.BitwiseAndAssign, BitwiseAndAssign },
        { TokenType.BitwiseOrAssign, BitwiseOrAssign },
        { TokenType.BitwiseXorAssign, BitwiseXorAssign },
        { TokenType.ShiftLeftAssign, ShiftLeftAssign },
        { TokenType.ShiftRightAssign, ShiftRightAssign },
        { TokenType.Equal, Equal },
        { TokenType.NotEqual, NotEqual },
        { TokenType.Greater, Greater },
        { TokenType.GreaterEqual, GreaterEqual },
        { TokenType.Less, Less },
        { TokenType.LessEqual, LessEqual },
        { TokenType.And, And },
        { TokenType.Or, Or },
        { TokenType.Not, Not },
        { TokenType.BitwiseAnd, BitwiseAnd },
        { TokenType.BitwiseOr, BitwiseOr },
        { TokenType.BitwiseXor, BitwiseXor },
        { TokenType.BitwiseNot, BitwiseNot },
        { TokenType.ShiftLeft, ShiftLeft },
        { TokenType.ShiftRight, ShiftRight },
        { TokenType.RightArrow, RightArrow },
        { TokenType.LeftArrow, LeftArrow },
        { TokenType.Dot, Dot },
        { TokenType.QuestionMark, QuestionMark },
        { TokenType.QuestionDot, QuestionDot },
        { TokenType.QuestionQuestion, QuestionQuestion },
        { TokenType.Colon, Colon },
        { TokenType.DoubleColon, DoubleColon },
        { TokenType.Hash, Hash },
        { TokenType.At, At },
        { TokenType.Dollar, Dollar },
        { TokenType.LeftParen, LeftParen },
        { TokenType.RightParen, RightParen },
        { TokenType.LeftBrace, LeftBrace },
        { TokenType.RightBrace, RightBrace },
        { TokenType.LeftBracket, LeftBracket },
        { TokenType.RightBracket, RightBracket },
        { TokenType.Semicolon, Semicolon },
        { TokenType.Comma, Comma },
        { TokenType.Identifier, Id },
        { TokenType.IntLiteral, IntLiteral },
        { TokenType.FloatLiteral, FloatLiteral },
        { TokenType.StringLiteral, StringLiteral },
        { TokenType.CharLiteral, CharLiteral },
        { TokenType.BoolLiteral, BoolLiteral },
        { TokenType.UnitLiteral, UnitLiteral },
        { TokenType.EOF, EOF },
    };

    public bool TryGetSymbolByTokenType(TokenType tokenType, out Symbol symbol)
    {
        return TS.TryGetByFirst(tokenType, out symbol);
    }

    #endregion

    #region 定义产生式

    public static readonly List<Production> Productions =
    [
        #region Scope

        #region FileScope

        new(FileScope, [S.L(UseStmt), FileScopedNamespaceDeclaration],
            nodes => new FileScope(nodes.Span<UseStmt>(0, 1), nodes[^1])),
        new(FileScope, [S.L(UseStmt), S.L(NamespaceDeclaration)],
            nodes =>
            {
                List<UseStmt> useStmts = new List<UseStmt>();
                List<NamespaceDeclaration> namespaceDeclarations = new List<NamespaceDeclaration>();
                foreach (var node in nodes)
                {
                    if (node is UseStmt useStmt) useStmts.Add(useStmt);
                    else if (node is NamespaceDeclaration namespaceDeclaration)
                        namespaceDeclarations.Add(namespaceDeclaration);
                    else throw new Exception("Invalid node type");
                }

                return new FileScope(useStmts, namespaceDeclarations);
            }),
        new(FileScope, [FileScopedNamespaceDeclaration], nodes => new FileScope(nodes[0])),
        new(FileScope, [S.L(NamespaceDeclaration)], nodes => new FileScope(nodes.Span<NamespaceDeclaration>())),

        #endregion

        #endregion

        new(UseStmt, [Use, QualifiedName, Semicolon], nodes => new UseStmt(nodes[1])),
        new(NamespaceStmt, [Use, QualifiedName, Semicolon], nodes => new NamespaceStmt(nodes[1])),

        new(FileScopedNamespaceDeclaration, [NamespaceStmt, S.L(StructDeclaration)],
            nodes => new NamespaceDeclaration(nodes[0], nodes.Span<StructDeclaration>(1))),
        new(NamespaceDeclaration, [Namespace, QualifiedName, LeftBrace, S.L(StructDeclaration), RightBrace],
            nodes => new NamespaceDeclaration(nodes[1], nodes.Span<StructDeclaration>(3, 1))),
        new(QualifiedName, [S.L(Id, Dot)],
            nodes => new QualifiedName(nodes.Span<Id>())),
        new(StructScope, [S.L(StructMember)],
            nodes => new StructScope(nodes.Span<StructMember>())),
        new(StructDeclaration, [S.L(Modifier), Struct, Id, LeftBrace, StructScope, RightBrace],
            nodes => new StructDeclaration(nodes.Span<Modifier>(0, 5), nodes[^4], nodes[^2])),

        #region StructMember

        new(StructMember, [FuncDeclaration],
            nodes => new StructMember(Lib.AST.StructMember.MemberType.Function, nodes[0])),
        new(StructMember, [VarDeclaration],
            nodes => new StructMember(Lib.AST.StructMember.MemberType.Variable, nodes[0])),

        #endregion

        #region VarDeclaration
        
        new(VarDeclaration, [S.L(Modifier), Let, Annotation, Assign, Expr, Semicolon],
            nodes => new VarDeclaration(nodes.Span<Modifier>(0, 5), nodes[^4], nodes[^2])),

        #endregion

        new(Annotation, [Id, Colon, QualifiedName], nodes => new Annotation(nodes[0], nodes[2])),
        new(Param, [S.L(Annotation, Comma)], nodes => new Param(nodes.Span<Annotation>())),
        new(Arg, [S.L(Expr, Comma)], nodes => new Param(nodes.Span<Annotation>())),

        #region FuncScope

        new(FuncScope, [BlockScope, ExprScope], nodes => new FuncScope(nodes[0], nodes[1])),
        new(FuncScope, [BlockScope], nodes => new FuncScope(nodes[0])),

        #endregion

        #region Modifier

        new(Modifier, [Public], _ => new Modifier("Public")),
        new(Modifier, [Private], _ => new Modifier("Private")),
        new(Modifier, [Protected], _ => new Modifier("Protected")),
        new(Modifier, [Internal], _ => new Modifier("Internal")),
        new(Modifier, [Friend], _ => new Modifier("Friend")),
        new(Modifier, [Static], _ => new Modifier("Static")),
        new(Modifier, [Readonly], _ => new Modifier("Readonly")),
        new(Modifier, [Const], _ => new Modifier("Const")),
        new(Modifier, [Virtual], _ => new Modifier("Virtual")),
        new(Modifier, [Abstract], _ => new Modifier("Abstract")),
        new(Modifier, [Async], _ => new Modifier("Async")),

        #endregion

        #region Expr

        #region AssignExpr

        new(Expr, [AssignExpr]),
        new(AssignExpr, [Id, Assign, Expr],
            nodes => new AssignExpr(Lib.AST.AssignExpr.AssignTypeEnum.Assign, nodes[0], nodes[2])),
        new(AssignExpr, [Id, PlusAssign, Expr],
            nodes => new AssignExpr(Lib.AST.AssignExpr.AssignTypeEnum.AddAssign, nodes[0], nodes[2])),
        new(AssignExpr, [Id, MinusAssign, Expr],
            nodes => new AssignExpr(Lib.AST.AssignExpr.AssignTypeEnum.SubAssign, nodes[0], nodes[2])),
        new(AssignExpr, [Id, MultiplyAssign, Expr],
            nodes => new AssignExpr(Lib.AST.AssignExpr.AssignTypeEnum.MulAssign, nodes[0], nodes[2])),
        new(AssignExpr, [Id, DivideAssign, Expr],
            nodes => new AssignExpr(Lib.AST.AssignExpr.AssignTypeEnum.DivAssign, nodes[0], nodes[2])),
        new(AssignExpr, [Id, ModulusAssign, Expr],
            nodes => new AssignExpr(Lib.AST.AssignExpr.AssignTypeEnum.ModAssign, nodes[0], nodes[2])),
        new(AssignExpr, [Id, BitwiseAndAssign, Expr],
            nodes => new AssignExpr(Lib.AST.AssignExpr.AssignTypeEnum.BitAndAssign, nodes[0], nodes[2])),
        new(AssignExpr, [Id, BitwiseOrAssign, Expr],
            nodes => new AssignExpr(Lib.AST.AssignExpr.AssignTypeEnum.BitOrAssign, nodes[0], nodes[2])),
        new(AssignExpr, [Id, BitwiseXorAssign, Expr],
            nodes => new AssignExpr(Lib.AST.AssignExpr.AssignTypeEnum.BitXorAssign, nodes[0], nodes[2])),
        new(AssignExpr, [Id, ShiftLeftAssign, Expr],
            nodes => new AssignExpr(Lib.AST.AssignExpr.AssignTypeEnum.ShiftLeftAssign, nodes[0], nodes[2])),
        new(AssignExpr, [Id, ShiftRightAssign, Expr],
            nodes => new AssignExpr(Lib.AST.AssignExpr.AssignTypeEnum.ShiftRightAssign, nodes[0], nodes[2])),

        #endregion

        #region LambdaExpr

        new(Expr, [LambdaExpr]),
        new(LambdaExpr, [LeftParen, Param, RightParen, RightArrow, QualifiedName, Expr],
            nodes => new LambdaExpr(nodes[1], nodes[4], nodes[5])),
        new(LambdaExpr, [LeftParen, Param, RightParen, RightArrow, QualifiedName, LeftBrace, FuncScope, RightBrace],
            nodes => new LambdaExpr(nodes[1], nodes[4], nodes[6])),

        #endregion

        #region ConditionalExpr

        new(Expr, [ConditionalExpr]),
        new(ConditionalExpr, [LogicalOrExpr, QuestionMark, Expr, Colon, Expr],
            nodes => new ConditionalExpr(nodes[0], nodes[2], nodes[4])),
        new(ConditionalExpr, [If, LogicalOrExpr, Then, Expr, Else, Expr],
            nodes => new ConditionalExpr(nodes[2], nodes[5], nodes[7])),

        #endregion

        #region PatternMatchExpr

        new(Expr, [PatternMatchExpr]),
        new(PatternMatchExpr,
            [Expr, Switch, LeftBrace, S.L(Case, Comma), RightBrace],
            nodes => new PatternMatchExpr(nodes[0], nodes.Span<CaseExpr>(3, 1))),

        #endregion

        #region LogicalOrExpr

        new(Expr, [LogicalOrExpr]),
        new(LogicalOrExpr, [LogicalAndExpr]),
        new(LogicalOrExpr, [LogicalOrExpr, Or, LogicalAndExpr],
            nodes => new BinaryOp("||", nodes[0], nodes[2])),

        #endregion

        #region LogicalAndExpr

        new(Expr, [LogicalAndExpr]),
        new(LogicalAndExpr, [BitwiseOrExpr]),
        new(LogicalAndExpr, [LogicalAndExpr, And, BitwiseOrExpr],
            nodes => new BinaryOp("&&", nodes[0], nodes[2])),

        #endregion

        #region BitwiseOrExpr

        new(Expr, [BitwiseOrExpr]),
        new(BitwiseOrExpr, [BitwiseXorExpr]),
        new(BitwiseOrExpr, [BitwiseOrExpr, BitwiseOr, BitwiseXorExpr],
            nodes => new BinaryOp("|", nodes[0], nodes[2])),

        #endregion

        #region BitXorExpr

        new(Expr, [BitwiseXorExpr]),
        new(BitwiseXorExpr, [BitwiseAndExpr]),
        new(BitwiseXorExpr, [BitwiseXorExpr, BitwiseXor, BitwiseAndExpr],
            nodes => new BinaryOp("^", nodes[0], nodes[2])),

        #endregion

        #region BitAndExpr

        new(Expr, [BitwiseAndExpr]),
        new(BitwiseAndExpr, [EqualityExpr]),
        new(BitwiseAndExpr, [BitwiseAndExpr, BitwiseAnd, EqualityExpr],
            nodes => new BinaryOp("&", nodes[0], nodes[2])),

        #endregion

        #region EqualityExpr

        new(Expr, [EqualityExpr]),
        new(EqualityExpr, [ComparisonExpr]),
        new(EqualityExpr, [EqualityExpr, Equal, ComparisonExpr],
            nodes => new BinaryOp("==", nodes[0], nodes[2])),
        new(EqualityExpr, [EqualityExpr, NotEqual, ComparisonExpr],
            nodes => new BinaryOp("!=", nodes[0], nodes[2])),

        #endregion

        #region ComparisonExpr

        new(Expr, [ComparisonExpr]),
        new(ComparisonExpr, [ShiftExpr]),
        new(ComparisonExpr, [ComparisonExpr, Less, ShiftExpr],
            nodes => new BinaryOp("<", nodes[0], nodes[2])),
        new(ComparisonExpr, [ComparisonExpr, Greater, ShiftExpr],
            nodes => new BinaryOp(">", nodes[0], nodes[2])),
        new(ComparisonExpr, [ComparisonExpr, LessEqual, ShiftExpr],
            nodes => new BinaryOp("<=", nodes[0], nodes[2])),
        new(ComparisonExpr, [ComparisonExpr, GreaterEqual, ShiftExpr],
            nodes => new BinaryOp(">=", nodes[0], nodes[2])),

        #endregion

        #region ShiftExpr

        new(Expr, [ShiftExpr]),
        new(ShiftExpr, [AdditiveExpr]),
        new(ShiftExpr, [ShiftExpr, ShiftLeft, AdditiveExpr],
            nodes => new BinaryOp("<<", nodes[0], nodes[2])),
        new(ShiftExpr, [ShiftExpr, ShiftRight, AdditiveExpr],
            nodes => new BinaryOp(">>", nodes[0], nodes[2])),

        #endregion

        #region AdditiveExpr

        new(Expr, [AdditiveExpr]),
        new(AdditiveExpr, [AdditiveExpr, Plus, MultiplicativeExpr],
            nodes => new BinaryOp("+", nodes[0], nodes[2])),
        new(AdditiveExpr, [AdditiveExpr, Minus, MultiplicativeExpr],
            nodes => new BinaryOp("-", nodes[0], nodes[2])),
        new(AdditiveExpr, [MultiplicativeExpr]),

        #endregion

        #region MultiplicativeExpr

        new(Expr, [MultiplicativeExpr]),
        new(MultiplicativeExpr, [MultiplicativeExpr, Multiply, UnaryExpr],
            nodes => new BinaryOp("*", nodes[0], nodes[2])),
        new(MultiplicativeExpr, [MultiplicativeExpr, Divide, UnaryExpr],
            nodes => new BinaryOp("/", nodes[0], nodes[2])),
        new(MultiplicativeExpr, [MultiplicativeExpr, Modulus, UnaryExpr],
            nodes => new BinaryOp("%", nodes[0], nodes[2])),
        new(MultiplicativeExpr, [UnaryExpr]),

        #endregion

        #region UnaryExpr

        new(Expr, [UnaryExpr]),
        new(UnaryExpr, [Plus, PrimaryExpr], nodes => new UnaryOp("+", nodes[1])),
        new(UnaryExpr, [Minus, PrimaryExpr], nodes => new UnaryOp("-", nodes[1])),
        new(UnaryExpr, [BitwiseNot, PrimaryExpr], nodes => new UnaryOp("~", nodes[1])),
        new(UnaryExpr, [Not, PrimaryExpr], nodes => new UnaryOp("!", nodes[1])),
        new(UnaryExpr, [PlusPlus, PrimaryExpr], nodes => new UnaryOp("++e", nodes[1])),
        new(UnaryExpr, [MinusMinus, PrimaryExpr], nodes => new UnaryOp("--e", nodes[1])),
        new(UnaryExpr, [LeftParen, QualifiedName, RightParen, PrimaryExpr], nodes => new CastExpr(nodes[3], nodes[1])),
        new(UnaryExpr, [Hash, PrimaryExpr], nodes => new SizeofExpr(nodes[1])),
        new(UnaryExpr, [PrimaryExpr]),

        #endregion

        #region PrimaryExpr

        new(Expr, [PrimaryExpr]),
        new(PrimaryExpr, [Id]),
        new(PrimaryExpr, [IntLiteral]),
        new(PrimaryExpr, [FloatLiteral]),
        new(PrimaryExpr, [StringLiteral]),
        new(PrimaryExpr, [CharLiteral]),
        new(PrimaryExpr, [BoolLiteral]),
        new(PrimaryExpr, [UnitLiteral]),
        new(PrimaryExpr, [This]),
        new(PrimaryExpr, [Unit]),
        new(PrimaryExpr, [LeftParen, Expr, RightParen], nodes => nodes[1]),
        new(PrimaryExpr, [PrimaryExpr, Dot, Id],
            nodes => new MemberAccess(nodes[0], nodes[2])),
        new(PrimaryExpr, [PrimaryExpr, LeftParen, Arg, RightParen],
            nodes => new CallExpr(nodes[0], nodes[2])),
        new(PrimaryExpr, [PrimaryExpr, LeftBracket, PrimaryExpr, RightBracket],
            nodes => new ArrayAccess(nodes[0], nodes[2])),
        new(PrimaryExpr, [PrimaryExpr, PlusPlus], nodes => new UnaryOp("e++", nodes[0])),
        new(PrimaryExpr, [PrimaryExpr, MinusMinus], nodes => new UnaryOp("e--", nodes[0])),

        #endregion

        #endregion
    ];

    #endregion
}