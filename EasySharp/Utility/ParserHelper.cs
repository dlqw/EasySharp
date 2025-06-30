using System.Reflection;
using System.Text;
using EasySharp.Core.Parsers;
using EasySharp.Lib.AST;

namespace EasySharp.Utility;

public static class ParserHelper
{
    public static List<T> Span<T>(this ASTNode[] nodes, int left = 0, int right = 0) where T : ASTNode
    {
        List<T> listNodes = new();

        for (var i = left; i < nodes.Length - right; i++)
        {
            var node = nodes[i];

            // 如果是列表节点，需要展开它
            if (node is AstListNode listNode)
            {
                var expandedNodes = ExpandListNode<T>(listNode);
                listNodes.AddRange(expandedNodes);
            }
            else if (node is T t)
            {
                listNodes.Add(t);
            }
        }

        return listNodes;
    }

    private static List<T> ExpandListNode<T>(AstListNode listNode) where T : ASTNode
    {
        var result = new List<T>();

        if (listNode.Left is T left)
        {
            result.Add(left);
        }

        if (listNode.Next is AstListNode nextList)
        {
            result.AddRange(ExpandListNode<T>(nextList));
        }
        else if (listNode.Next is T next)
        {
            result.Add(next);
        }

        return result;
    }

    public static List<Symbol> GetSymbols()
    {
        return typeof(Symbol).GetFields(bindingAttr: BindingFlags.Static | BindingFlags.Public)
            .Where(field => field.FieldType == typeof(Symbol))
            .Select(field => field.GetValue(null))
            .OfType<Symbol>()
            .ToList();
    }

    public static List<(Symbol, FieldInfo)> GetSymbolFields()
    {
        List<(Symbol, FieldInfo)> result = new();
        foreach (var fieldInfo in typeof(Symbol).GetFields(bindingAttr: BindingFlags.Static | BindingFlags.Public))
        {
            if (fieldInfo.GetValue(null) is Symbol symbol)
            {
                result.Add((symbol, fieldInfo));
            }
        }

        return result;
    }

    public static void GenerateToken2Symbol()
    {
        StringBuilder sb = new();
        foreach (var symbol in GetSymbolFields())
        {
            if (symbol.Item1.SymbolType == SymbolTypeEnum.Terminal)
                sb.AppendLine($"{{ TokenType.{symbol.Item2.Name}, {symbol.Item2.Name} }},");
        }

        ClipboardHelper.SetText(sb.ToString());
        Console.WriteLine("复制文本:\n" + sb);
    }
}