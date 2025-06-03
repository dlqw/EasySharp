namespace EasySharp.Core.Parsers;

/// 分析表动作
public class Action(ActionType type, int value = 0)
{
    public ActionType Type { get; } = type;
    public int Value { get; } = value; // 对于Shift动作，该值是状态号；对于Reduce动作，该值是产生式编号

    public override string ToString()
    {
        return Type switch
        {
            ActionType.Shift => $"s{Value}",
            ActionType.Reduce => $"r{Value}",
            ActionType.Accept => "acc",
            _ => "err"
        };
    }
}