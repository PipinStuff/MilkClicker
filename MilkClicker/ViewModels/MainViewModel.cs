using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MilkClicker.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly GameService _gameService;
    private System.Timers.Timer? _passiveIncomeTimer;

    [ObservableProperty]
    private double totalPoints;

    [ObservableProperty]
    private double pointsPerClick;

    [ObservableProperty]
    private double pointsPerSecond;



    [ObservableProperty]
    private ObservableCollection<Upgrade> availableUpgrades = new();

    [ObservableProperty]
    private double clickAnimationScale = 1.0;

    public MainViewModel(GameService gameService)
    {
        _gameService = gameService;
        Title = "Milk Clicker";

        Task.Run(async () => await InitializeAsync());
    }

    private async Task InitializeAsync()
    {
        IsBusy = true;

        try
        {
            await _gameService.LoadGameStateAsync();
            UpdateDisplayValues();

            var upgrades = _gameService.GetAvailableUpgrades();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                AvailableUpgrades.Clear();
                foreach (var upgrade in upgrades)
                {
                    AvailableUpgrades.Add(upgrade);
                }
            });

            StartPassiveIncomeTimer();
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task ClickMilkAsync()
    {
        if (IsBusy)
            return;

        var pointsEarned = await _gameService.PerformClickAsync();
        UpdateDisplayValues();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            ClickAnimationScale = 1.2;
            await Task.Delay(100);
            ClickAnimationScale = 1.0;
        });
    }

    [RelayCommand]
    private async Task PurchaseUpgradeAsync(Upgrade upgrade)
    {
        if (IsBusy || upgrade == null)
            return;

        var success = await _gameService.PurchaseUpgradeAsync(upgrade);

        if (success)
        {
            UpdateDisplayValues();
            OnPropertyChanged(nameof(AvailableUpgrades));
        }
        else
        {
            await Shell.Current.DisplayAlert("Not Enough Points",
                $"You need {upgrade.GetNextCost():F0} points to purchase this upgrade.",
                "OK");
        }
    }

    private void StartPassiveIncomeTimer()
    {
        _passiveIncomeTimer = new System.Timers.Timer(1000);
        _passiveIncomeTimer.Elapsed += async (sender, e) =>
        {
            await _gameService.UpdatePassiveIncomeAsync();
            UpdateDisplayValues();
        };
        _passiveIncomeTimer.Start();
    }

    private void UpdateDisplayValues()
    {
        var state = _gameService.CurrentState;
        if (state != null)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TotalPoints = state.TotalPoints;
                PointsPerClick = state.PointsPerClick;
                PointsPerSecond = state.PointsPerSecond;
            });
            var upgrades = _gameService.GetAvailableUpgrades();
            ObservableCollection<Upgrade> tempLista = new();
            AvailableUpgrades = tempLista;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                AvailableUpgrades.Clear();
                foreach (var upgrade in upgrades)
                {
                    AvailableUpgrades.Add(upgrade);
                }
            });
        }
    }

    public void Cleanup()
    {
        _passiveIncomeTimer?.Stop();
        _passiveIncomeTimer?.Dispose();
    }
}
