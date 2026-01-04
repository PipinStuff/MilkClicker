using SQLite;

namespace MilkClicker.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection? _database;
    private readonly string _databasePath;

    public DatabaseService()
    {
        _databasePath = Path.Combine(FileSystem.AppDataDirectory, "milkclicker.db3");
    }

    private async Task InitializeAsync()
    {
        if (_database != null)
            return;

        _database = new SQLiteAsyncConnection(_databasePath);
        await _database.CreateTableAsync<GameState>();
    }

    public async Task<GameState> GetGameStateAsync()
    {
        await InitializeAsync();

        var states = await _database!.Table<GameState>().ToListAsync();

        if (states.Count == 0)
        {
            var newState = new GameState();
            await _database.InsertAsync(newState);
            return newState;
        }

        var state = states[0];

        var timeSinceLastUpdate = DateTime.Now - state.LastPassiveUpdate;
        if (timeSinceLastUpdate.TotalSeconds > 0 && state.PointsPerSecond > 0)
        {
            state.TotalPoints += state.PointsPerSecond * timeSinceLastUpdate.TotalSeconds;
            state.LastPassiveUpdate = DateTime.Now;
        }

        return state;
    }

    public async Task SaveGameStateAsync(GameState state)
    {
        await InitializeAsync();

        state.LastSaved = DateTime.Now;
        state.LastPassiveUpdate = DateTime.Now;

        await _database!.UpdateAsync(state);
    }

    public async Task ResetGameStateAsync()
    {
        await InitializeAsync();

        await _database!.DeleteAllAsync<GameState>();

        var newState = new GameState();
        await _database.InsertAsync(newState);
    }
}
