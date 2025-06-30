using EasySharp.Lib.AST;

namespace EasySharp.Core.Parsers;

/// LALR(1)
public class LALRParser
{
    private readonly Grammar _grammar;
    private List<ItemSet> _states = new();
    private Dictionary<(int, Symbol), int> _gotoTable = new();
    private Dictionary<(int, Symbol), Action> _actionTable = new();
    private readonly Production _augmentedProduction;

    public LALRParser(Grammar grammar)
    {
        _grammar = grammar;

        // 添加增广产生式 S' -> S
        _augmentedProduction = new Production(
            SpecialSymbols.AugmentedStart,
            [grammar.StartSymbol],
            0, // 第一个产生式的编号
            nodes => nodes[0] // 语义动作：返回第一个节点
        );

        Console.WriteLine("开始构建语法分析表...");
        BuildParsingTable();
        Console.WriteLine("语法分析表构建完成");
    }

    private void BuildParsingTable()
    {
        var initialItem = new LRItem(_augmentedProduction, 0, SpecialSymbols.EndMarker);
        var initialItemSet = Closure(new ItemSet(initialItem));

        _states = [initialItemSet];
        initialItemSet.StateNumber = 0;

        var worklist = new Queue<ItemSet>();
        worklist.Enqueue(initialItemSet);

        _gotoTable = new Dictionary<(int, Symbol), int>();

        // 构建规范LR(1)项集族和GOTO表
        while (worklist.Count > 0)
        {
            var currentState = worklist.Dequeue();
            var nextSymbols = currentState.Items
                .Where(item => item.IsShiftItem)
                .Select(item => item.NextSymbol)
                .Distinct()
                .ToList();

            foreach (var symbol in nextSymbols)
            {
                if (symbol == null) continue;
                var gotoSet = Goto(currentState, symbol);

                bool found = false;
                for (int i = 0; i < _states.Count; i++)
                {
                    if (_states[i].Equals(gotoSet))
                    {
                        _gotoTable[(currentState.StateNumber, symbol)] = i;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    gotoSet.StateNumber = _states.Count;
                    _states.Add(gotoSet);
                    _gotoTable[(currentState.StateNumber, symbol)] = gotoSet.StateNumber;
                    worklist.Enqueue(gotoSet);
                }
            }
        }

        Console.WriteLine("构建GOTO表完成");

        MergeSimilarStates();

        Console.WriteLine("合并状态完成");

        BuildActionTable();

        Console.WriteLine("Action表构建完成");
    }

    // 计算项集的闭包
    private ItemSet Closure(ItemSet itemSet)
    {
        var result = new ItemSet(new HashSet<LRItem>(itemSet.Items));
        var worklist = new Queue<LRItem>(result.Items);

        while (worklist.Count > 0)
        {
            var item = worklist.Dequeue();

            if (!item.IsShiftItem || item.NextSymbol?.SymbolType != SymbolTypeEnum.NonTerminal)
                continue;

            var nonTerminal = item.NextSymbol;
            var beta = item.Production.Right.Skip(item.DotPosition + 1).ToList();
            beta.Add(item.Lookahead);

            var lookAheads = new HashSet<Symbol>();
            var betaFirst = _grammar.First(beta);

            foreach (var symbol in betaFirst)
            {
                if (!symbol.Equals(SpecialSymbols.Epsilon))
                    lookAheads.Add(symbol);
            }

            foreach (var production in _grammar.Productions.Where(p => p.Left.Equals(nonTerminal)))
            {
                foreach (var lookAhead in lookAheads)
                {
                    var newItem = new LRItem(production, 0, lookAhead);
                    if (result.Items.Add(newItem))
                        worklist.Enqueue(newItem);
                }
            }
        }

        return result;
    }

    // 计算GOTO函数
    private ItemSet Goto(ItemSet itemSet, Symbol symbol)
    {
        var result = new HashSet<LRItem>();

        foreach (var item in itemSet.Items)
        {
            if (item is { IsShiftItem: true, NextSymbol: not null } && item.NextSymbol.Equals(symbol))
            {
                result.Add(item.Advance());
            }
        }

        return Closure(new ItemSet(result));
    }

    private void MergeSimilarStates()
    {
        var stateGroups = new Dictionary<string, List<ItemSet>>();

        foreach (var state in _states)
        {
            var coreKey = string.Join("|",
                state.Items.Select(item => $"{item.Production.ProductionNumber}:{item.DotPosition}").OrderBy(s => s));

            if (!stateGroups.ContainsKey(coreKey))
                stateGroups[coreKey] = new List<ItemSet>();

            stateGroups[coreKey].Add(state);
        }

        var newStates = new List<ItemSet>();
        var stateMap = new Dictionary<int, int>(); // 旧状态号 -> 新状态号

        foreach (var group in stateGroups.Values)
        {
            if (group.Count == 1)
            {
                var state = group[0];
                stateMap[state.StateNumber] = newStates.Count;
                state.StateNumber = newStates.Count;
                newStates.Add(state);
            }
            else
            {
                var mergedItems = new HashSet<LRItem>();
                foreach (var state in group)
                {
                    foreach (var item in state.Items)
                    {
                        mergedItems.Add(item);
                    }

                    stateMap[state.StateNumber] = newStates.Count;
                }

                var mergedState = new ItemSet(mergedItems)
                {
                    StateNumber = newStates.Count
                };
                newStates.Add(mergedState);
            }
        }

        var newGotoTable = new Dictionary<(int, Symbol), int>();
        foreach (var entry in _gotoTable)
        {
            var oldFromState = entry.Key.Item1;
            var symbol = entry.Key.Item2;
            var oldToState = entry.Value;

            newGotoTable[(stateMap[oldFromState], symbol)] = stateMap[oldToState];
        }

        _states = newStates;
        _gotoTable = newGotoTable;
    }

    private void BuildActionTable()
    {
        _actionTable = new Dictionary<(int, Symbol), Action>();

        foreach (var state in _states)
        {
            foreach (var terminal in _grammar.Terminals.Concat([SpecialSymbols.EndMarker]))
            {
                _actionTable[(state.StateNumber, terminal)] = new Action(ActionType.Error);
                // Console.WriteLine($"ACTION[{state.StateNumber}, {terminal}] = Error");
            }
        }

        for (int stateIndex = 0; stateIndex < _states.Count; stateIndex++)
        {
            var state = _states[stateIndex];

            foreach (var item in state.Items)
            {
                if (item.IsShiftItem)
                {
                    var nextSymbol = item.NextSymbol;
                    if (nextSymbol?.SymbolType == SymbolTypeEnum.Terminal &&
                        _gotoTable.TryGetValue((stateIndex, nextSymbol), out int nextState))
                    {
                        _actionTable[(stateIndex, nextSymbol)] = new Action(ActionType.Shift, nextState);
                        //  Console.WriteLine($"ACTION[{stateIndex}, {nextSymbol}] = Shift({nextState})");
                    }
                }
                else // item.IsReduceItem
                {
                    // 接受操作 - 对于增广文法的起始产生式
                    if (item.Production.ProductionNumber == 0 && item.Lookahead.Equals(SpecialSymbols.EndMarker))
                    {
                        _actionTable[(stateIndex, SpecialSymbols.EndMarker)] = new Action(ActionType.Accept);
                        //Console.WriteLine($"ACTION[{stateIndex}, EndMarker] = Accept");
                    }
                    else
                    {
                        // 归约操作
                        _actionTable[(stateIndex, item.Lookahead)] =
                            new Action(ActionType.Reduce, item.Production.ProductionNumber);
                        // Console.WriteLine($"ACTION[{stateIndex}, {item.Lookahead}] = Reduce({item.Production.ProductionNumber})");
                    }
                }
            }
        }

        ResolveConflicts();
    }

    // 解决移入/归约冲突和归约/归约冲突
    private void ResolveConflicts()
    {
        // 优先移入
        foreach (var state in _states)
        {
            var stateActions = _actionTable
                .Where(entry => entry.Key.Item1 == state.StateNumber)
                .ToList();

            foreach (var terminal in _grammar.Terminals.Concat([SpecialSymbols.EndMarker]))
            {
                var conflictingActions = stateActions
                    .Where(entry => entry.Key.Item2.Equals(terminal) && entry.Value.Type != ActionType.Error)
                    .ToList();

                if (conflictingActions.Count > 1)
                {
                    // 如果有移入/归约冲突，优先选择移入
                    var shiftAction = conflictingActions.FirstOrDefault(a => a.Value.Type == ActionType.Shift);
                    if (shiftAction.Value != null)
                    {
                        _actionTable[(state.StateNumber, terminal)] = shiftAction.Value;
                    }
                    else
                    {
                        // 如果有归约/归约冲突，选择产生式编号较小的
                        var reduceActions = conflictingActions
                            .Where(a => a.Value.Type == ActionType.Reduce)
                            .OrderBy(a => a.Value.Value)
                            .ToList();

                        _actionTable[(state.StateNumber, terminal)] = reduceActions.First().Value;
                    }
                }
            }
        }
    }

    /// 使用语法分析表进行语法分析
    public ASTNode? Parse(List<(Symbol Symbol, string Value)> tokens)
    {
        var stateStack = new Stack<int>();
        var nodeStack = new Stack<ASTNode>();
        stateStack.Push(0); // 初始状态

        int index = 0;

        while (index <= tokens.Count)
        {
            var currentState = stateStack.Peek();
            var currentSymbol = index < tokens.Count ? tokens[index].Symbol : SpecialSymbols.EndMarker;
            var currentValue = index < tokens.Count ? tokens[index].Value : "";
            Console.WriteLine($"当前状态: {currentState}, 当前符号: {currentSymbol.Name}");
            if (!_actionTable.TryGetValue((currentState, currentSymbol), out var action))
            {
                Console.WriteLine($"语法错误：[NotFind] 状态 {currentState}，符号 {currentSymbol}");
                return null;
            }

            switch (action.Type)
            {
                case ActionType.Shift:
                    stateStack.Push(action.Value);

                    if (currentSymbol.SymbolType == SymbolTypeEnum.Terminal)
                    {
                        nodeStack.Push(new Terminal(currentSymbol.Name, currentValue));

                        Console.WriteLine($"移入：{currentSymbol.Name} -> {currentValue}");
                    }

                    index++;
                    break;

                case ActionType.Reduce:
                    var productionIndex = action.Value - 1;
                    var production = _grammar.Productions[productionIndex];

                    // 从栈中弹出右部符号对应的节点
                    var childNodes = new ASTNode[production.Right.Count];
                    for (int i = production.Right.Count - 1; i >= 0; i--)
                    {
                        if (!stateStack.TryPop(out _))
                        {
                            Console.WriteLine($"语法错误：无法弹出状态");
                            return null;
                        }

                        if (nodeStack.TryPop(out var childNode))
                        {
                            childNodes[i] = childNode;
                        }
                        else
                        {
                            Console.WriteLine($"语法错误：无法弹出节点");
                            return null;
                        }
                    }

                    var newNode = production.SemanticAction(childNodes);
                    nodeStack.Push(newNode);

                    var gotoState = stateStack.Peek();

                    if (!_gotoTable.TryGetValue((gotoState, production.Left), out int nextState))
                    {
                        Console.WriteLine($"语法错误：无法找到GOTO({gotoState}, {production.Left})");
                        return null;
                    }

                    stateStack.Push(nextState);
                    Console.WriteLine($"归约：{production.Left} -> {string.Join(" ", production.Right)}");
                    break;

                case ActionType.Accept:
                    Console.WriteLine("语法分析成功");
                    return nodeStack.Pop(); // 返回根节点

                case ActionType.Error:
                default:
                    Console.WriteLine($"语法错误：[ActionType.Error] 状态 {currentState}，符号 {currentSymbol}");
                    return null;
            }
        }

        return null;
    }

    public void PrintParsingTable()
    {
        Console.WriteLine("ACTION表：");
        foreach (var state in _states)
        {
            Console.WriteLine($"状态 {state.StateNumber}:");
            foreach (var terminal in _grammar.Terminals.Concat([SpecialSymbols.EndMarker]))
            {
                if (_actionTable.TryGetValue((state.StateNumber, terminal), out var action) &&
                    action.Type != ActionType.Error)
                {
                    Console.WriteLine($"  ACTION[{state.StateNumber}, {terminal}] = {action}");
                }
            }
        }

        Console.WriteLine("\nGOTO表：");
        foreach (var state in _states)
        {
            foreach (var nonTerminal in _grammar.NonTerminals)
            {
                if (_gotoTable.TryGetValue((state.StateNumber, nonTerminal), out int nextState))
                {
                    Console.WriteLine($"  GOTO[{state.StateNumber}, {nonTerminal}] = {nextState}");
                }
            }
        }
    }
}