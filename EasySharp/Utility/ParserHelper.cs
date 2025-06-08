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
}