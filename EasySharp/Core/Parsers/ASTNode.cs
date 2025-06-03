namespace EasySharp.Core.Parsers;

public abstract class ASTNode
{
    public abstract string NodeType { get; }
    
    public virtual string ToTreeString(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}";
    }
}