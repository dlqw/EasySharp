using EasySharp.Utility;

// ReSharper disable once CheckNamespace
namespace EasySharp.Core;

public partial class Token
{
    public static readonly BiDictionary<string, TokenType> Keywords = new()
    {
        {"let", TokenType.Let},
        {"var", TokenType.Var},
        {"null", TokenType.Null},
        {"unit", TokenType.Unit},
        {"func", TokenType.Func},
        {"marco", TokenType.Marco},
        {"static", TokenType.Static},
        {"const", TokenType.Const},
        {"readonly", TokenType.Readonly},
        {"loop", TokenType.Loop},
        {"break", TokenType.Break},
        {"continue", TokenType.Continue},
        {"if", TokenType.If},
        {"then", TokenType.Then},
        {"else", TokenType.Else},
        {"switch", TokenType.Switch},
        {"case", TokenType.Case},
        {"enum", TokenType.Enum},
        {"struct", TokenType.Struct},
        {"private", TokenType.Private},
        {"public", TokenType.Public},
        {"protected", TokenType.Protected},
        {"internal", TokenType.Internal},
        {"friend", TokenType.Friend},
        {"async", TokenType.Async},
        {"await", TokenType.Await},
        {"promise", TokenType.Promise},
        {"do", TokenType.Do},
        {"try", TokenType.Try},
        {"as", TokenType.As},
        {"move", TokenType.Move},
        {"self", TokenType.Self},
        {"use", TokenType.Use},
        {"yield", TokenType.Yield},
        {"virtual", TokenType.Virtual},
        {"abstract", TokenType.Abstract}
    };
}