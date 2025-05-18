namespace EasySharp.Utility;

public static class Debug
{
    public static bool ShowLog = true;

    public static void Log(string message)
    {
        Console.WriteLine(message);
    }

    public static void Log(object message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void Error(string message)
    {
        // 输出红色字体的日志
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}