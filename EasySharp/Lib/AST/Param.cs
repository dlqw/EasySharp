using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class Param(List<Annotation> annotationList) : ASTNode
{
    public List<Annotation> AnnotationList { get; set; } = annotationList;
    public override string NodeType => "Param";
    
}