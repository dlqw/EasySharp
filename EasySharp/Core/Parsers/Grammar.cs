namespace EasySharp.Core.Parsers;

/// 文法定义
public class Grammar
{
    public Symbol StartSymbol { get; }
    public List<Production> Productions { get; }
    public HashSet<Symbol> Terminals { get; }
    public HashSet<Symbol> NonTerminals { get; }

    private Dictionary<Symbol, HashSet<Symbol>> _firstSets = new();
    private Dictionary<Symbol, HashSet<Symbol>> _followSets = new();

    public Grammar(Symbol startSymbol, List<Production> productions, HashSet<Symbol> terminals,
        HashSet<Symbol> nonTerminals)
    {
        StartSymbol = startSymbol;
        Productions = productions;
        Terminals = terminals;
        NonTerminals = nonTerminals;

        // 计算 FIRST 和 FOLLOW 集
        ComputeFirstSets();
        ComputeFollowSets();
    }

    /// 计算 FIRST 集
    private void ComputeFirstSets()
    {
        _firstSets = new Dictionary<Symbol, HashSet<Symbol>>();

        // 初始化每个符号的FIRST集
        foreach (var terminal in Terminals)
        {
            _firstSets[terminal] = new HashSet<Symbol> { terminal };
        }

        foreach (var nonTerminal in NonTerminals)
        {
            _firstSets[nonTerminal] = new HashSet<Symbol>();
        }

        // 特殊处理空串
        _firstSets[SpecialSymbols.Epsilon] = new HashSet<Symbol> { SpecialSymbols.Epsilon };

        bool changed;
        do
        {
            changed = false;

            foreach (var production in Productions)
            {
                var left = production.Left;
                var right = production.Right;

                if (right.Count == 0 || right[0].Equals(SpecialSymbols.Epsilon))
                {
                    // 如果右侧为空，添加ε到FIRST集
                    if (_firstSets[left].Add(SpecialSymbols.Epsilon))
                        changed = true;
                }
                else
                {
                    bool allCanDeriveEpsilon = true;

                    foreach (var symbol in right)
                    {
                        var symbolFirstSet = _firstSets[symbol];

                        // 添加除ε外的所有符号
                        foreach (var terminal in symbolFirstSet.Where(s => !s.Equals(SpecialSymbols.Epsilon)))
                        {
                            if (_firstSets[left].Add(terminal))
                                changed = true;
                        }

                        // 检查是否可以推导出ε
                        if (!symbolFirstSet.Contains(SpecialSymbols.Epsilon))
                        {
                            allCanDeriveEpsilon = false;
                            break;
                        }
                    }

                    // 如果所有符号都可以推导出ε，那么左侧也可以
                    if (allCanDeriveEpsilon)
                    {
                        if (_firstSets[left].Add(SpecialSymbols.Epsilon))
                            changed = true;
                    }
                }
            }
        } while (changed);
    }

    /// 获取序列的FIRST集
    public HashSet<Symbol> First(List<Symbol> sequence)
    {
        var result = new HashSet<Symbol>();

        if (sequence.Count == 0)
        {
            result.Add(SpecialSymbols.Epsilon);
            return result;
        }

        bool allCanDeriveEpsilon = true;

        foreach (var symbol in sequence)
        {
            var symbolFirstSet = _firstSets[symbol];

            // 添加除ε外的所有符号
            foreach (var terminal in symbolFirstSet.Where(s => !s.Equals(SpecialSymbols.Epsilon)))
            {
                result.Add(terminal);
            }

            // 检查是否可以推导出ε
            if (!symbolFirstSet.Contains(SpecialSymbols.Epsilon))
            {
                allCanDeriveEpsilon = false;
                break;
            }
        }

        // 如果所有符号都可以推导出ε，那么序列也可以
        if (allCanDeriveEpsilon)
        {
            result.Add(SpecialSymbols.Epsilon);
        }

        return result;
    }

    // 计算FOLLOW集
    private void ComputeFollowSets()
    {
        _followSets = new Dictionary<Symbol, HashSet<Symbol>>();

        // 初始化每个非终结符的FOLLOW集
        foreach (var nonTerminal in NonTerminals)
        {
            _followSets[nonTerminal] = new HashSet<Symbol>();
        }

        // 将$加入起始符号的FOLLOW集
        _followSets[StartSymbol].Add(SpecialSymbols.EndMarker);

        bool changed;
        do
        {
            changed = false;

            foreach (var production in Productions)
            {
                var left = production.Left;
                var right = production.Right;

                for (int i = 0; i < right.Count; i++)
                {
                    var symbol = right[i];

                    // 只对非终结符计算FOLLOW集
                    if (symbol.Type != SymbolType.NonTerminal)
                        continue;

                    // 计算symbol后面的部分的FIRST集
                    var beta = right.Skip(i + 1).ToList();
                    var betaFirst = First(beta);

                    // 将First(beta)中除ε外的所有符号加入FOLLOW(symbol)
                    foreach (var terminal in betaFirst.Where(s => !s.Equals(SpecialSymbols.Epsilon)))
                    {
                        if (_followSets[symbol].Add(terminal))
                            changed = true;
                    }

                    // 如果First(beta)包含ε或beta为空，将FOLLOW(left)加入FOLLOW(symbol)
                    if (beta.Count == 0 || betaFirst.Contains(SpecialSymbols.Epsilon))
                    {
                        foreach (var terminal in _followSets[left])
                        {
                            if (_followSets[symbol].Add(terminal))
                                changed = true;
                        }
                    }
                }
            }
        } while (changed);
    }
}