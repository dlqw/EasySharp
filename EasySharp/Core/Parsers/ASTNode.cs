using System.Text;
using EasySharp.Utility;

namespace EasySharp.Core.Parsers;

public abstract class ASTNode
{
    // public static StringBuilder Sb = new StringBuilder();
    public abstract string NodeType { get; }

    public virtual string Print(int indent = 0)
    {
        StringBuilder sb = new StringBuilder(32);
        sb.Append($"{new string(' ', indent)}{NodeType}\n");

        foreach (var propertyInfo in GetType().GetProperties())
        {
            var type = propertyInfo.PropertyType;
            if (type.IsListOf<ASTNode>(out var elementType))
            {
                var listValue = propertyInfo.GetValue(this);
                if (listValue == null) continue;
                
                if (listValue is System.Collections.IEnumerable enumerable)
                {
                    foreach (var item in enumerable)
                    {
                        if (item is ASTNode node)
                        {
                            sb.Append(node.Print(indent + 2));
                        }
                    }
                }
            }
            else if (typeof(ASTNode).IsAssignableFrom(type))
            {
                if (propertyInfo.GetValue(this) is not ASTNode node) continue;
                sb.Append(node.Print(indent + 2));
            }
        }

        return sb.ToString();
    }

    // public static string PrintSub<T>(T? node, int indent = 0) where T : ASTNode
    // {
    //     if (node == null) return "";
    //     return node.Print(indent + 2) + "\n";
    // }
    //
    // public static string PrintSub<T>(List<T>? nodes, int indent = 0) where T : ASTNode
    // {
    //     if (nodes == null) return "";
    //     Sb.Clear();
    //     foreach (var node in nodes)
    //     {
    //         Sb.Append(node.Print(indent + 2) + "\n");
    //     }
    //
    //     return Sb.ToString();
    // }
}

public static class AstNodeHelper
{
}