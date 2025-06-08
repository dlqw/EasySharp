using System.Reflection;
using EasySharp.Lib.AST;
using EasySharp.Utility;

namespace EasySharp.Core.Parsers;

public partial class Parser
{
    private readonly Grammar _grammar;
    private readonly HashSet<Symbol> _terminals = [SpecialSymbols.EndMarker];
    private readonly HashSet<Symbol> _nonTerminals = [SpecialSymbols.AugmentedStart];

    public Parser()
    {
        foreach (var symbol in Symbol.TS.SecondKeys)
        {
            _terminals.Add(symbol);
        }

        foreach (var symbol in Symbol.NTS.SecondKeys)
        {
            _nonTerminals.Add(symbol);
        }

        int productionId = 1;
        var productions = new List<Production>();
        foreach (var production in Symbol.Productions)
        {
            var expanded = ExpandListProductions(production, ref productionId);
            productions.AddRange(expanded);
        }

        _grammar = new Grammar(Symbol.FileScope, productions, _terminals, _nonTerminals);
    }

    #region ExpandList

    private List<Production> ExpandListProductions(Production production, ref int productionId)
    {
        var result = new List<Production>();
        var hasListSymbol = false;
        var listSymbols = new List<(int index, Symbol originalSymbol)>();

        for (int i = 0; i < production.Right.Count; i++)
        {
            if (production.Right[i] is SymbolInstance symbolInstance && symbolInstance.IsList)
            {
                hasListSymbol = true;
                listSymbols.Add((i, symbolInstance));
            }
        }

        if (!hasListSymbol)
        {
            result.Add(new Production(production.Left, production.Right, productionId++, production.SemanticAction));
            return result;
        }

        // 为每个列表符号创建对应的列表非终结符
        foreach (var (index, symbolInstance) in listSymbols)
        {
            var listNonTerminal = new Symbol($"{symbolInstance.Name}List", SymbolType.NonTerminal);

            if (!_nonTerminals.Add(listNonTerminal))
            {
                // ListSymbol -> Symbol
                var singleProduction = new Production(
                    listNonTerminal,
                    [new Symbol(symbolInstance.Name, symbolInstance.Type)],
                    productionId++,
                    nodes => CreateListNode(nodes, symbolInstance.Name, single: true)
                );
                result.Add(singleProduction);

                // ListSymbol -> Symbol ListSymbol
                var multipleProduction = new Production(
                    listNonTerminal,
                    [new Symbol(symbolInstance.Name, symbolInstance.Type), listNonTerminal],
                    productionId++,
                    nodes => CreateListNode(nodes, symbolInstance.Name, single: false)
                );
                result.Add(multipleProduction);
            }

            // 替换原产生式中的列表符号
            var newRight = new List<Symbol>(production.Right)
            {
                [index] = listNonTerminal
            };

            var newProduction = new Production(
                production.Left,
                newRight,
                productionId++,
                CreateSemanticActionForList(production.SemanticAction, index)
            );
            result.Add(newProduction);
        }

        return result;
    }

    private Func<ASTNode[], ASTNode> CreateSemanticActionForList(
        Func<ASTNode[], ASTNode> originalAction,
        int listIndex)
    {
        return nodes =>
        {
            // 将列表节点转换为实际的列表
            var convertedNodes = new ASTNode[nodes.Length];
            for (int i = 0; i < nodes.Length; i++)
            {
                if (i == listIndex && nodes[i] is AstListNode listNode)
                {
                    convertedNodes[i] = listNode;
                }
                else
                {
                    convertedNodes[i] = nodes[i];
                }
            }

            return originalAction(convertedNodes);
        };
    }

    private ASTNode CreateListNode(ASTNode[] nodes, string symbolName, bool single)
    {
        if (!TypeCache.TryGetType($"EasySharp.Lib.AST.{symbolName}List", out var type))
        {
            throw new Exception($"Cannot find type {symbolName}List");
        }

        var result = single
            ? Activator.CreateInstance(type, BindingFlags.CreateInstance, nodes[0])
            : Activator.CreateInstance(type, BindingFlags.CreateInstance, nodes[0], nodes[1]);
        if (result is ASTNode node)
        {
            return node;
        }

        throw new Exception($"Cannot create instance of {symbolName}List");
    }

    #endregion

    public void Parse(List<Token> tokens)
    {
        var parser = new LALRParser(_grammar);
        List<(Symbol Symbol, string)> input = tokens
            .Select(token => (Symbol.TS[token.Type], token.Lexeme))
            .ToList();

        var ast = parser.Parse(input);

        if (ast == null)
        {
            Console.WriteLine("Error: Invalid syntax.");
            return;
        }

        Console.WriteLine("AST:");
        Console.WriteLine(ast.ToTreeString());
    }
}