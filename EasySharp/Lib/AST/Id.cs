using EasySharp.Core.Parsers;

namespace EasySharp.Lib.AST;

public class Id(string name) : ASTNode
{
    public string Name { get; set; } = name;
    public override string NodeType => "Id";

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Name}\n";
    }
}

public class IntLiteral(int value) : ASTNode
{
    public int Value { get; private set; } = value;
    public override string NodeType => "IntLiteral";

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Value}\n";
    }
}

public class FloatLiteral(float value) : ASTNode
{
    public float Value { get; private set; } = value;
    public override string NodeType => "FloatLiteral";

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Value}\n";
    }
}

public class StringLiteral(string value) : ASTNode
{
    public string Value { get; private set; } = value;
    public override string NodeType => "StringLiteral";

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Value}\n";
    }
}

public class CharLiteral(char value) : ASTNode
{
    public char Value { get; private set; } = value;
    public override string NodeType => "CharLiteral";

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Value}\n";
    }
}

public class BoolLiteral(bool value) : ASTNode
{
    public bool Value { get; private set; } = value;
    public override string NodeType => "BoolLiteral";

    public override string Print(int indent = 0)
    {
        return $"{new string(' ', indent)}{NodeType}:{Value}\n";
    }
}