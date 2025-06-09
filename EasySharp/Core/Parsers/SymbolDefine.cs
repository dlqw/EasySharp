using System.Xml.Schema;
using EasySharp.Lib.AST;
using EasySharp.Utility;
using S = EasySharp.Core.Parsers.SymbolInstance;

namespace EasySharp.Core.Parsers;

public partial class Symbol
{
    #region 定义符号

    #region 非终结符

    public static readonly Symbol FileScope = new("FileScope", SymbolType.NonTerminal);
    public static readonly Symbol StructScope = new("StructScope", SymbolType.NonTerminal);
    public static readonly Symbol FuncScope = new("FuncScope", SymbolType.NonTerminal);
    public static readonly Symbol ExprScope = new("ExprScope", SymbolType.NonTerminal);
    public static readonly Symbol BlockScope = new("BlockScope", SymbolType.NonTerminal);

    public static readonly Symbol FileScopedNamespaceDeclaration = new("FileScopedNamespaceDeclaration",
        SymbolType.NonTerminal);

    public static readonly Symbol NamespaceDeclaration = new("NamespaceDeclaration", SymbolType.NonTerminal);
    public static readonly Symbol StructDeclaration = new("StructDeclaration", SymbolType.NonTerminal);
    public static readonly Symbol FuncDeclaration = new("FuncDeclaration", SymbolType.NonTerminal);
    public static readonly Symbol VarDeclaration = new("VarDeclaration", SymbolType.NonTerminal);

    public static readonly Symbol Expr = new("Expr", SymbolType.NonTerminal);
    public static readonly Symbol AssignExpr = new("AssignExpr", SymbolType.NonTerminal);
    public static readonly Symbol LambdaExpr = new("LambdaExpr", SymbolType.NonTerminal);
    public static readonly Symbol PatternMatchExpr = new("PatternMatchExpr", SymbolType.NonTerminal);
    public static readonly Symbol ConditionalExpr = new("ConditionalExpr", SymbolType.NonTerminal);
    public static readonly Symbol LogicalOrExpr = new("LogicalOrExpr", SymbolType.NonTerminal);
    public static readonly Symbol LogicalAndExpr = new("LogicalAndExpr", SymbolType.NonTerminal);
    public static readonly Symbol BitwiseOrExpr = new("BitwiseOrExpr", SymbolType.NonTerminal);
    public static readonly Symbol BitwiseXorExpr = new("BitwiseXorExpr", SymbolType.NonTerminal);
    public static readonly Symbol BitwiseAndExpr = new("BitwiseAndExpr", SymbolType.NonTerminal);
    public static readonly Symbol EqualityExpr = new("EqualityExpr", SymbolType.NonTerminal);
    public static readonly Symbol ComparisonExpr = new("ComparisonExpr", SymbolType.NonTerminal);
    public static readonly Symbol ShiftExpr = new("ShiftExpr", SymbolType.NonTerminal);
    public static readonly Symbol AdditiveExpr = new("AdditiveExpr", SymbolType.NonTerminal);
    public static readonly Symbol MultiplicativeExpr = new("MultiplicativeExpr", SymbolType.NonTerminal);
    public static readonly Symbol UnaryExpr = new("UnaryExpr", SymbolType.NonTerminal);
    public static readonly Symbol PostfixExpr = new("PostfixExpr", SymbolType.NonTerminal);
    public static readonly Symbol PrimaryExpr = new("PrimaryExpr", SymbolType.NonTerminal);

    public static readonly Symbol Modifier = new("Modifier", SymbolType.NonTerminal);
    public static readonly Symbol Arg = new("Arg", SymbolType.NonTerminal);
    public static readonly Symbol Param = new("Param", SymbolType.NonTerminal);
    public static readonly Symbol Id = new("Id", SymbolType.NonTerminal);
    public static readonly Symbol Annotation = new("Annotation", SymbolType.NonTerminal);
    public static readonly Symbol Literal = new("Literal", SymbolType.NonTerminal);
    public static readonly Symbol NamespaceName = new Symbol("NamespaceName", SymbolType.NonTerminal);

    public static readonly Symbol StructMember = new("StructMember", SymbolType.NonTerminal);

    public static readonly Symbol Stmt = new("Stmt", SymbolType.NonTerminal);
    public static readonly Symbol ReturnStmt = new("ReturnStmt", SymbolType.NonTerminal);
    public static readonly Symbol UseStmt = new("UseStmt", SymbolType.NonTerminal);
    public static readonly Symbol NamespaceStmt = new("NamespaceStmt", SymbolType.NonTerminal);

    #endregion

    #region 终结符

    #region KeyWord

    public static readonly Symbol Let = new("let", SymbolType.Terminal);
    public static readonly Symbol Var = new("var", SymbolType.Terminal);
    public static readonly Symbol Null = new("null", SymbolType.Terminal);
    public static readonly Symbol Unit = new("unit", SymbolType.Terminal);
    public static readonly Symbol This = new("this", SymbolType.Terminal);
    public static readonly Symbol Func = new("func", SymbolType.Terminal);
    public static readonly Symbol Marco = new("marco", SymbolType.Terminal);
    public static readonly Symbol Static = new("static", SymbolType.Terminal);
    public static readonly Symbol Const = new("const", SymbolType.Terminal);
    public static readonly Symbol Readonly = new("readonly", SymbolType.Terminal);
    public static readonly Symbol Loop = new("loop", SymbolType.Terminal);
    public static readonly Symbol Break = new("break", SymbolType.Terminal);
    public static readonly Symbol Continue = new("continue", SymbolType.Terminal);
    public static readonly Symbol If = new("if", SymbolType.Terminal);
    public static readonly Symbol Then = new("then", SymbolType.Terminal);
    public static readonly Symbol Else = new("else", SymbolType.Terminal);
    public static readonly Symbol Switch = new("switch", SymbolType.Terminal);
    public static readonly Symbol Case = new("case", SymbolType.Terminal);
    public static readonly Symbol Enum = new("enum", SymbolType.Terminal);
    public static readonly Symbol Struct = new("struct", SymbolType.Terminal);
    public static readonly Symbol Private = new("private", SymbolType.Terminal);
    public static readonly Symbol Public = new("public", SymbolType.Terminal);
    public static readonly Symbol Protected = new("protected", SymbolType.Terminal);
    public static readonly Symbol Internal = new("internal", SymbolType.Terminal);
    public static readonly Symbol Friend = new("friend", SymbolType.Terminal);
    public static readonly Symbol Async = new("async", SymbolType.Terminal);
    public static readonly Symbol Await = new("await", SymbolType.Terminal);
    public static readonly Symbol Promise = new("promise", SymbolType.Terminal);
    public static readonly Symbol Do = new("do", SymbolType.Terminal);
    public static readonly Symbol Try = new("try", SymbolType.Terminal);
    public static readonly Symbol As = new("as", SymbolType.Terminal);
    public static readonly Symbol Move = new("move", SymbolType.Terminal);
    public static readonly Symbol Self = new("self", SymbolType.Terminal);
    public static readonly Symbol Use = new("use", SymbolType.Terminal);
    public static readonly Symbol Yield = new("yield", SymbolType.Terminal);
    public static readonly Symbol Virtual = new("virtual", SymbolType.Terminal);
    public static readonly Symbol Abstract = new("abstract", SymbolType.Terminal);
    public static readonly Symbol Namespace = new("namespace", SymbolType.Terminal);

    #endregion

    #region 赋值运算符

    public static readonly Symbol Assign = new("=", SymbolType.Terminal);
    public static readonly Symbol PlusAssign = new("+=", SymbolType.Terminal);
    public static readonly Symbol MinusAssign = new("-=", SymbolType.Terminal);
    public static readonly Symbol MultiplyAssign = new("*=", SymbolType.Terminal);
    public static readonly Symbol DivideAssign = new("/=", SymbolType.Terminal);
    public static readonly Symbol ModulusAssign = new("%=", SymbolType.Terminal);
    public static readonly Symbol BitwiseAndAssign = new("&=", SymbolType.Terminal);
    public static readonly Symbol BitwiseOrAssign = new("|=", SymbolType.Terminal);
    public static readonly Symbol BitwiseXorAssign = new("^=", SymbolType.Terminal);
    public static readonly Symbol ShiftLeftAssign = new("<<=", SymbolType.Terminal);
    public static readonly Symbol ShiftRightAssign = new(">>=", SymbolType.Terminal);

    #endregion

    #region 比较运算符

    public static readonly Symbol Equal = new("==", SymbolType.Terminal);
    public static readonly Symbol NotEqual = new("!=", SymbolType.Terminal);
    public static readonly Symbol Greater = new(">", SymbolType.Terminal);
    public static readonly Symbol GreaterEqual = new(">=", SymbolType.Terminal);
    public static readonly Symbol Less = new("<", SymbolType.Terminal);
    public static readonly Symbol LessEqual = new("<=", SymbolType.Terminal);

    #endregion

    public static readonly Symbol And = new("&&", SymbolType.Terminal);
    public static readonly Symbol Or = new("||", SymbolType.Terminal);
    public static readonly Symbol Not = new("!", SymbolType.Terminal);
    public static readonly Symbol BitwiseAnd = new("&", SymbolType.Terminal);
    public static readonly Symbol BitwiseOr = new("|", SymbolType.Terminal);
    public static readonly Symbol BitwiseXor = new("^", SymbolType.Terminal);
    public static readonly Symbol BitwiseNot = new("~", SymbolType.Terminal);
    public static readonly Symbol ShiftLeft = new("<<", SymbolType.Terminal);
    public static readonly Symbol ShiftRight = new(">>", SymbolType.Terminal);
    public static readonly Symbol RightArrow = new("->", SymbolType.Terminal);
    public static readonly Symbol LeftArrow = new("<-", SymbolType.Terminal);
    public static readonly Symbol Dot = new(".", SymbolType.Terminal);
    public static readonly Symbol QuestionMark = new("?", SymbolType.Terminal);
    public static readonly Symbol QuestionDot = new("?.", SymbolType.Terminal);
    public static readonly Symbol QuestionQuestion = new("??", SymbolType.Terminal);
    public static readonly Symbol Colon = new(":", SymbolType.Terminal);
    public static readonly Symbol DoubleColon = new("::", SymbolType.Terminal);
    public static readonly Symbol Hash = new("#", SymbolType.Terminal);
    public static readonly Symbol At = new("@", SymbolType.Terminal);
    public static readonly Symbol Dollar = new("$", SymbolType.Terminal);

    #region 分隔符

    public static readonly Symbol LeftParen = new("(", SymbolType.Terminal);
    public static readonly Symbol RightParen = new(")", SymbolType.Terminal);
    public static readonly Symbol LeftBrace = new("{", SymbolType.Terminal);
    public static readonly Symbol RightBrace = new("}", SymbolType.Terminal);
    public static readonly Symbol LeftBracket = new("[", SymbolType.Terminal);
    public static readonly Symbol RightBracket = new("]", SymbolType.Terminal);
    public static readonly Symbol Semicolon = new(";", SymbolType.Terminal);
    public static readonly Symbol Comma = new(",", SymbolType.Terminal);

    #endregion

    public static readonly Symbol EOF = SpecialSymbols.EndMarker;
    public static readonly Symbol Error = SpecialSymbols.EndMarker;

    #endregion

    public static readonly BiDictionary<TokenType, Symbol> NTS = new()
    {
    };

    public static readonly BiDictionary<TokenType, Symbol> TS = new()
    {
    };

    public bool TryGetSymbolByTokenType(TokenType tokenType, out Symbol symbol)
    {
        return TS.TryGetByFirst(tokenType, out symbol) || NTS.TryGetByFirst(tokenType, out symbol);
    }

    #endregion

    #region 定义产生式

    public static readonly List<Production> Productions =
    [
        new(FileScope, [S.L(UseStmt), FileScopedNamespaceDeclaration],
            nodes => new FileScope(nodes.Span<UseStmt>(0, 1), nodes[^1])),
        new(FileScope, [S.L(UseStmt), S.L(NamespaceDeclaration)],
            nodes => {
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
        new(FileScope, [FileScopedNamespaceDeclaration],
            nodes => new FileScope(nodes[0])),
        new(FileScope, [S.L(NamespaceDeclaration)],
            nodes => new FileScope(nodes.Span<NamespaceDeclaration>())),

        new(UseStmt, [Use, NamespaceName, Semicolon],
            nodes => new UseStmt(nodes[1])),
        new(NamespaceStmt, [Use, NamespaceName, Semicolon],
            nodes => new NamespaceStmt(nodes[1])),

        new(FileScopedNamespaceDeclaration, [NamespaceStmt, S.L(StructDeclaration)],
            nodes => new NamespaceDeclaration(nodes[0], nodes.Span<StructDeclaration>(1))),
        new(NamespaceDeclaration, [Namespace, NamespaceName, LeftBrace, S.L(StructDeclaration), RightBrace],
            nodes => new NamespaceDeclaration(nodes[1], nodes.Span<StructDeclaration>(3, 1))),

        new(StructScope, [S.L(StructMember)],
            nodes => new StructScope(nodes.Span<StructMember>())),
        new(StructDeclaration, [S.L(Modifier), Struct, Id, LeftBrace, StructScope, RightBrace],
            nodes => new StructDeclaration(nodes.Span<Modifier>(0, 5), nodes[^4], nodes[^2])),

        new(StructMember, [FuncDeclaration],
            nodes => new StructMember(Lib.AST.StructMember.MemberType.Function, nodes[0])),
        new(StructMember, [VarDeclaration],
            nodes => new StructMember(Lib.AST.StructMember.MemberType.Variable, nodes[0])),

        new(VarDeclaration, [S.L(Modifier), Let, Id, Assign, Expr, Semicolon],
            nodes => new VarDeclaration(nodes.Span<Modifier>(0, 5), nodes[^4], nodes[^2])),
        new(VarDeclaration, [S.L(Modifier), Let, Annotation, Assign, Expr, Semicolon],
            nodes => new VarDeclaration(nodes.Span<Modifier>(0, 5), nodes[^4], nodes[^2])),
        
        new(Annotation, [Id, Colon, Id],
            nodes => new Annotation(nodes[0], nodes[2])),
        new(Param, [S.L(Annotation, Comma)],
            nodes => new Param(nodes.Span<Annotation>())),
        new(Arg, [S.L(Expr, Comma)],
            nodes => new Param(nodes.Span<Annotation>())),
    ];

    #endregion
}