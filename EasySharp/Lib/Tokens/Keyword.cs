using System.Text;
using EasySharp.Utility;

// ReSharper disable once CheckNamespace
namespace EasySharp.Core;

public partial class Token
{
    public static readonly BiDictionary<string, TokenType> Keywords = new()
    {
        { "let", TokenType.Let },
        { "var", TokenType.Var },
        { "null", TokenType.Null },
        { "unit", TokenType.Unit },
        { "this", TokenType.This },
        { "func", TokenType.Func },
        { "marco", TokenType.Marco },
        { "static", TokenType.Static },
        { "const", TokenType.Const },
        { "readonly", TokenType.Readonly },
        { "loop", TokenType.Loop },
        { "break", TokenType.Break },
        { "continue", TokenType.Continue },
        { "if", TokenType.If },
        { "then", TokenType.Then },
        { "else", TokenType.Else },
        { "switch", TokenType.Switch },
        { "case", TokenType.Case },
        { "enum", TokenType.Enum },
        { "struct", TokenType.Struct },
        { "private", TokenType.Private },
        { "public", TokenType.Public },
        { "protected", TokenType.Protected },
        { "internal", TokenType.Internal },
        { "friend", TokenType.Friend },
        { "async", TokenType.Async },
        { "await", TokenType.Await },
        { "promise", TokenType.Promise },
        { "do", TokenType.Do },
        { "try", TokenType.Try },
        { "as", TokenType.As },
        { "move", TokenType.Move },
        { "self", TokenType.Self },
        { "use", TokenType.Use },
        { "yield", TokenType.Yield },
        { "virtual", TokenType.Virtual },
        { "abstract", TokenType.Abstract },
        { "namespace", TokenType.Namespace }
    };

    public static void GeneratorKeywordSymbols()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var kv in Keywords)
        {
            sb.Append($"public static readonly Symbol {kv.Value} = new(\"{kv.Key}\", SymbolType.Terminal);\n");
        }

        ClipboardHelper.SetText(sb.ToString());
        Console.WriteLine("复制文本:\n" + sb);
    }
}