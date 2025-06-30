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

        foreach (var symbol in ParserHelper.GetSymbols())
        {
            if (symbol.SymbolType == SymbolTypeEnum.NonTerminal)
            {
                Symbol.NTS.Add(symbol);
            }
        }

        foreach (var symbol in Symbol.NTS)
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
        var repeatSymbols = new List<(int index, Symbol originalSymbol, Symbol repeatSymbol)>();

        for (int i = 0; i < production.Right.Count; i++)
        {
            if (production.Right[i] is not SymbolInstance { IsList: true } symbolInstance) continue;
            hasListSymbol = true;
            if (symbolInstance.RepeatSymbol == null) listSymbols.Add((i, symbolInstance));
            else repeatSymbols.Add((i, symbolInstance, symbolInstance.RepeatSymbol));
        }

        if (!hasListSymbol)
        {
            result.Add(new Production(production.Left, production.Right, productionId++, production.SemanticAction));
            return result;
        }

        Console.WriteLine($"ExpandList for {production.Left} {listSymbols.Count} {repeatSymbols.Count}");

        foreach (var (index, symbolInstance) in listSymbols)
        {
            var symbol = new Symbol(symbolInstance.Name, symbolInstance.SymbolType);
            var listNonTerminal = new Symbol($"{symbolInstance.Name}List", SymbolTypeEnum.NonTerminal);

            if (_nonTerminals.Add(listNonTerminal))
            {
                // ListSymbol -> Symbol
                var singleProduction = new Production(
                    listNonTerminal,
                    [symbol],
                    productionId++,
                    nodes => CreateListNode(nodes, symbolInstance.Name, single: true)
                );
                result.Add(singleProduction);

                // ListSymbol -> Symbol ListSymbol
                var multipleProduction = new Production(
                    listNonTerminal,
                    [symbol, listNonTerminal],
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

        foreach (var (index, symbolInstance, repeatSymbol) in repeatSymbols)
        {
            // todo 实现 repeat 语法
            var symbol = new Symbol(symbolInstance.Name, symbolInstance.SymbolType);
            var listNonTerminal = new Symbol($"{symbolInstance.Name}{repeatSymbol.Name}List", SymbolTypeEnum.NonTerminal);

            if (_nonTerminals.Add(listNonTerminal))
            {
                // ListSymbol -> Symbol
                var singleProduction = new Production(
                    listNonTerminal,
                    [symbol],
                    productionId++,
                    nodes => CreateListNode(nodes, symbolInstance.Name, single: true)
                );
                result.Add(singleProduction);

                // ListSymbol -> Symbol RepeatSymbol ListSymbol
                var multipleProduction = new Production(
                    listNonTerminal,
                    [symbol, repeatSymbol, listNonTerminal],
                    productionId++,
                    nodes => CreateListNode(nodes, symbolInstance.Name, single: false, middle: true)
                );
                result.Add(multipleProduction);
            }

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

    private ASTNode CreateListNode(ASTNode[] nodes, string symbolName, bool single, bool middle = false)
    {
        var result = single
            ? Activator.CreateInstance(typeof(AstListNode), symbolName, nodes[0])
            : Activator.CreateInstance(typeof(AstListNode), symbolName, nodes[0], nodes[middle ? 2 : 1]);
        if (result is ASTNode node)
        {
            return node;
        }

        throw new Exception($"Cannot create instance of {symbolName}List");
    }

    #endregion

    public ASTNode? Parse(List<Token> tokens)
    {
        var parser = new LALRParser(_grammar);
        List<(Symbol Symbol, string)> input = tokens
            .Select(token => (Symbol.TS[token.Type], token.Lexeme))
            .ToList();

        var ast = parser.Parse(input);

        if (ast == null)
        {
            Console.WriteLine("Error: Invalid syntax.");
            return null;
        }

        Console.WriteLine("AST:");
        Console.WriteLine(ast.ToTreeString());
        return ast;
    }
}