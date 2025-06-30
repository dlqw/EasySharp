using EasySharp.Core.Lexers;
using EasySharp.Core.Parsers;
using EasySharp.PublicAPI;

string src = File.ReadAllText("D:\\rdququ\\RiderProjects\\EasySharp\\EasySharp.Demo\\src");
Console.WriteLine("Code Split State:");
CodeSplitter splitter = new CodeSplitter(src);
foreach (var code in splitter.Split())
{
    Console.WriteLine(code);
}

Console.WriteLine($"\nLexer State:");
Lexer lexer = new Lexer();
var tokens = lexer.Tokenize(src);
foreach (var token in tokens)
{
    Console.WriteLine(token);
}

Parser parser = new Parser();
parser.Parse(tokens.ToList());