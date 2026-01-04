using SQLite;

namespace MilkClicker.Models;

[Table("GameState")]
public class GameState
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public double TotalPoints { get; set; }

    public double PointsPerClick { get; set; }

    public double PointsPerSecond { get; set; }

    public int ClickUpgradeLevel { get; set; }

    public int PassiveUpgradeLevel { get; set; }

    public DateTime LastSaved { get; set; }

    public DateTime LastPassiveUpdate { get; set; }

    public GameState()
    {
        TotalPoints = 0;
        PointsPerClick = 1;
        PointsPerSecond = 0;
        ClickUpgradeLevel = 0;
        PassiveUpgradeLevel = 0;
        LastSaved = DateTime.Now;
        LastPassiveUpdate = DateTime.Now;
    }
}
