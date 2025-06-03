namespace EasySharp.Core.Parsers;

internal class Parser
{
    private readonly Grammar _grammar;
    private readonly HashSet<Symbol> _terminals = new HashSet<Symbol>() { SpecialSymbols.EndMarker };
    private readonly HashSet<Symbol> _nonTerminals = new HashSet<Symbol>() { SpecialSymbols.AugmentedStart };

    public Parser()
    {
        // foreach (var symbol in Syntax.TS.Values)
        // {
        //     if (symbol.Type == SymbolType.Terminal)
        //         _terminals.Add(symbol);
        //     else
        //         _nonTerminals.Add(symbol);
        // }
        //
        // foreach (var nonTerminal in Syntax.NonTerminals)
        // {
        //     if (nonTerminal.Type == SymbolType.NonTerminal)
        //         _nonTerminals.Add(nonTerminal);
        // }
        //
        // var productions = new List<Production>();
        // int productionId = 1;
        // foreach (var production in Syntax.Productions)
        // {
        //     var p = new Production(production.Left, production.Right, productionId++, production.SemanticAction);
        //     productions.Add(p);
        // }
        //
        // _grammar = new Grammar(Syntax.Chunk, productions, _terminals, _nonTerminals);
    }

    public void Parse(List<Token> tokens)
    {
        // var parser = new LALRParser(_grammar);
        // List<(Symbol Symbol, string)> input = tokens
        //     .Select(token => (Syntax.TS[token.Type], token.Name))
        //     .ToList();
        //
        // var ast = parser.Parse(input);
        //
        // if (ast == null)
        // {
        //     Console.WriteLine("Error: Invalid syntax.");
        //     return;
        // }
        //
        // Console.WriteLine("AST:");
        // Console.WriteLine(ast.ToTreeString());
    }
}