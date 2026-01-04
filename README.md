[README.md](https://github.com/user-attachments/files/24424132/README.md)
# Milk Clicker

A cookie-clicker style incremental game built with .NET MAUI for Android.

## Overview

Milk Clicker is a casual mobile game where players tap a milk icon to earn points. Points can be used to purchase upgrades that increase:
- Points earned per click
- Automatic points generated per second

## Technology Stack

- **Framework**: .NET MAUI 8.0
- **Language**: C#
- **Database**: SQLite (local device storage)
- **Architecture**: MVVM pattern with CommunityToolkit.Mvvm
- **UI**: XAML with default Microsoft UI elements

## Project Structure

```
MilkClicker/
├── Models/              # Data models (GameState, Upgrade)
├── Services/            # Business logic (DatabaseService, GameService)
├── ViewModels/          # MVVM ViewModels
├── Views/               # XAML pages
├── Converters/          # Value converters for data binding
├── Resources/           # Styles, colors, fonts, images
└── Platforms/           # Platform-specific code (Android, iOS, etc.)
```

## Features

- **Manual Clicking**: Tap the milk icon to earn points
- **Automatic Income**: Purchase upgrades to generate passive income
- **Persistent Storage**: Game progress is saved locally using SQLite
- **Real-time Updates**: UI updates instantly when clicking or earning passive income
- **Upgrade System**: Two types of upgrades:
  - Stronger Fingers: Increases points per click
  - Milk Farm: Generates automatic points per second

## Building the Project

### Prerequisites

- Visual Studio 2022 (17.8 or later) with .NET MAUI workload installed
- .NET 8.0 SDK
- Android SDK (for Android builds)

### Build Steps

1. Open the solution in Visual Studio 2022
2. Select your target platform (Android recommended)
3. Set the target device or emulator
4. Build and run the project (F5)

### Command Line Build

```bash
dotnet build
```

### Android APK

```bash
dotnet publish -f net8.0-android -c Release
```

## Requirements Met

### Functional Requirements
- **FR-001**: Manual clicking with immediate point rewards
- **FR-002**: Upgrade purchasing system
- **FR-003**: Display of upgrade effects
- **FR-004**: Real-time score and rate display

### Non-Functional Requirements
- **NFR1**: UI response under 0.5s for all interactions
- **NFR2**: Progression scaling through upgrades
- **NFR3**: Theme-able design with separatedresources
- **NFR4**: Local device data storage with SQLite
- **NFR5**: Responsive layout (scalable UI)
- **NFR6**: APK output format
- **NFR7**: Optimized application size
- **NFR8**: Stable gameplay with error handling
- **NFR9**: Testable game mechanics
- **NFR10**: Efficient click handling

## Architecture Decisions

The project follows the architecture decisions documented in the ADR:
- **.NET MAUI** for cross-platform mobile development
- **C#** as the primary language
- **SQLite** for local data persistence
- **Monolithic** design for simplicity
- **Default Microsoft UI** elements for consistency

## Game Mechanics

### Clicking
- Each click earns points based on current upgrade levels
- Base value: 1 point per click
- Increases with "Stronger Fingers" upgrades

### Passive Income
- Automatically generates points every second
- Base value: 0 points per second
- Increases with "Milk Farm" upgrades

### Upgrades
- Cost increases exponentially with each level
- Effects stack additively
- Instant application after purchase

## Database Schema

### GameState Table
- `Id`: Primary key (auto-increment)
- `TotalPoints`: Current point total
- `PointsPerClick`: Current click value
- `PointsPerSecond`: Current passive income rate
- `ClickUpgradeLevel`: Level of click upgrades
- `PassiveUpgradeLevel`: Level of passive upgrades
- `LastSaved`: Last save timestamp
- `LastPassiveUpdate`: Last passive income calculation

## Future Enhancements

- Additional upgrade types
- Achievement system
- Leaderboards
- Multiple themes
- Sound effects and animations
- Cloud save functionality

## License

Educational project - see requirements specification for details.
