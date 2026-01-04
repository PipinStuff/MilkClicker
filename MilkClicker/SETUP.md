# Milk Clicker - Setup Instructions

## Missing Components

This project structure is complete, but you'll need to add the following font files to make the project build successfully:

### Required Fonts

Download and place in `Resources/Fonts/` directory:

1. **OpenSans-Regular.ttf**
2. **OpenSans-Semibold.ttf**

You can download these fonts from:
- [Google Fonts - Open Sans](https://fonts.google.com/specimen/Open+Sans)

## Quick Setup

1. Open the project in **Visual Studio 2022** (17.8 or later)
2. Ensure you have the **.NET MAUI workload** installed
3. Add the required font files to `Resources/Fonts/`
4. Restore NuGet packages (should happen automatically)
5. Select **Android** as your target platform
6. Choose an Android emulator or physical device
7. Press **F5** to build and run

## First Build

The first build may take several minutes as it:
- Downloads NuGet packages
- Compiles the Android bindings
- Deploys to the emulator/device

## Verifying the Installation

After building, you should see:
1. A green splash screen with a milk emoji
2. The main game screen with:
   - Total points display at the top
   - A large milk button in the center
   - Upgrade options at the bottom

## Troubleshooting

### Missing Fonts Error
If you get an error about missing fonts, ensure:
- Font files are in `Resources/Fonts/` directory
- Font files are named exactly as specified
- The project file includes the fonts correctly

### Android Emulator Issues
- Ensure you have Android SDK 21 or higher
- Try creating a new emulator with API level 30+
- Check that Hyper-V or HAXM is enabled for hardware acceleration

### Build Errors
- Clean the solution: `Build > Clean Solution`
- Rebuild: `Build > Rebuild Solution`
- Delete `bin` and `obj` folders manually
- Restart Visual Studio

## Testing the Game

Once running:
1. Tap the milk emoji to earn 1 point
2. After earning 10 points, purchase the "Stronger Fingers" upgrade
3. Notice your clicks now earn more points
4. After earning 50 points, purchase the "Milk Farm" upgrade
5. Watch as points generate automatically each second
6. Close and reopen the app - your progress should be saved

## Architecture Details

- **MVVM Pattern**: ViewModels handle business logic, Views handle UI
- **Dependency Injection**: Services registered in MauiProgram.cs
- **Data Binding**: Two-way binding between ViewModels and Views
- **SQLite Database**: Automatic creation and schema management
- **Async/Await**: All database operations are asynchronous

## Next Steps

Consider adding:
- More upgrade types (multipliers, automation, special bonuses)
- Visual feedback (animations, particle effects)
- Sound effects
- Multiple save slots
- Achievement system
- Statistics tracking
- Theme customization options

## Support

For .NET MAUI issues, consult:
- [Microsoft .NET MAUI Documentation](https://docs.microsoft.com/dotnet/maui/)
- [.NET MAUI GitHub Repository](https://github.com/dotnet/maui)
