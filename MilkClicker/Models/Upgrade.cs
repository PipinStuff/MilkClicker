namespace MilkClicker.Models;

public class Upgrade
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public UpgradeType Type { get; set; }
    public int CurrentLevel { get; set; }
    public double BaseValue { get; set; }
    public double BaseCost { get; set; }
    public double CostMultiplier { get; set; }

    public double GetCurrentValue()
    {
        return BaseValue * CurrentLevel;
    }

    public double GetNextCost()
    {
        return BaseCost * Math.Pow(CostMultiplier, CurrentLevel);
    }

    public double GetNextValue()
    {
        return BaseValue * (CurrentLevel + 1);
    }
}

public enum UpgradeType
{
    ClickPower,
    PassiveIncome
}
