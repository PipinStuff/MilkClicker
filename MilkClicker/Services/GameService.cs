namespace MilkClicker.Services;

public class GameService
{
    private readonly DatabaseService _databaseService;
    private GameState? _currentState;
    private List<Upgrade> _availableUpgrades;

    public GameService(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        _availableUpgrades = InitializeUpgrades();
    }

    public async Task<GameState> LoadGameStateAsync()
    {
        _currentState = await _databaseService.GetGameStateAsync();
        UpdateUpgradeLevels();
        return _currentState;
    }

    public async Task SaveGameStateAsync()
    {
        if (_currentState != null)
        {
            await _databaseService.SaveGameStateAsync(_currentState);
        }
    }

    public async Task<double> PerformClickAsync()
    {
        if (_currentState == null)
            await LoadGameStateAsync();

        _currentState!.TotalPoints += _currentState.PointsPerClick;
        await SaveGameStateAsync();

        return _currentState.PointsPerClick;
    }

    public async Task<bool> PurchaseUpgradeAsync(Upgrade upgrade)
    {
        if (_currentState == null)
            await LoadGameStateAsync();

        var cost = upgrade.GetNextCost();

        if (_currentState!.TotalPoints < cost)
            return false;

        _currentState.TotalPoints -= cost;
        upgrade.CurrentLevel++;

        if (upgrade.Type == UpgradeType.ClickPower)
        {
            _currentState.ClickUpgradeLevel = upgrade.CurrentLevel;
            _currentState.PointsPerClick = CalculatePointsPerClick();
        }
        else if (upgrade.Type == UpgradeType.PassiveIncome)
        {
            _currentState.PassiveUpgradeLevel = upgrade.CurrentLevel;
            _currentState.PointsPerSecond = CalculatePointsPerSecond();
        }

        await SaveGameStateAsync();
        return true;
    }

    public async Task UpdatePassiveIncomeAsync()
    {
        if (_currentState == null)
            await LoadGameStateAsync();

        if (_currentState!.PointsPerSecond > 0)
        {
            _currentState.TotalPoints += _currentState.PointsPerSecond;
            await SaveGameStateAsync();
        }
    }

    public List<Upgrade> GetAvailableUpgrades()
    {
        return _availableUpgrades;
    }

    private List<Upgrade> InitializeUpgrades()
    {
        return new List<Upgrade>
        {
            new Upgrade
            {
                Name = "Stronger Fingers",
                Description = "Increase points per click",
                Type = UpgradeType.ClickPower,
                BaseValue = 1,
                BaseCost = 10,
                CostMultiplier = 1.15,
                CurrentLevel = 0
            },
            new Upgrade
            {
                Name = "Milk Farm",
                Description = "Automatically generate points per second",
                Type = UpgradeType.PassiveIncome,
                BaseValue = 1,
                BaseCost = 50,
                CostMultiplier = 1.20,
                CurrentLevel = 0
            }
        };
    }

    private void UpdateUpgradeLevels()
    {
        if (_currentState == null)
            return;

        var clickUpgrade = _availableUpgrades.FirstOrDefault(u => u.Type == UpgradeType.ClickPower);
        if (clickUpgrade != null)
            clickUpgrade.CurrentLevel = _currentState.ClickUpgradeLevel;

        var passiveUpgrade = _availableUpgrades.FirstOrDefault(u => u.Type == UpgradeType.PassiveIncome);
        if (passiveUpgrade != null)
            passiveUpgrade.CurrentLevel = _currentState.PassiveUpgradeLevel;
    }

    private double CalculatePointsPerClick()
    {
        var clickUpgrade = _availableUpgrades.FirstOrDefault(u => u.Type == UpgradeType.ClickPower);
        return 1 + (clickUpgrade?.GetCurrentValue() ?? 0);
    }

    private double CalculatePointsPerSecond()
    {
        var passiveUpgrade = _availableUpgrades.FirstOrDefault(u => u.Type == UpgradeType.PassiveIncome);
        return passiveUpgrade?.GetCurrentValue() ?? 0;
    }

    public GameState? CurrentState => _currentState;
}
