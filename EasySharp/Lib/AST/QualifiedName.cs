using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class QualifiedName(List<Id> idList) : ASTNode
{
    public List<Id> IdList { get; set; } = idList;
    public override string NodeType => "QualifiedName";

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}QualifiedName:{string.Join(".", IdList.Select(id => id.Name.ToString()))}\n";
    }
}