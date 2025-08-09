# HexaHue

![Menu Screenshot](https://github.com/NolanOhnemus/HexaHue/blob/main/Menu.png)
![Gameplay Screenshot](https://github.com/NolanOhnemus/HexaHue/blob/main/Screenshot.png)

## Overview

This project is a Unity-based puzzle game inspired by the classic **Minesweeper**, reimagined with a vibrant and modern twist. Rather than uncovering mines, players use visual hints to deduce and **fill a grid with correct colors**. With limited guesses and strategic clues, the game challenges players to complete each board using logic and pattern recognition.

## Objective

Create a fully featured puzzle game where the player must fill each cell of a customizable grid with the correct color, based on hint patterns. The player has **three chances** to solve the puzzle correctly before losing. This game serves as a demonstration of advanced Unity mechanics, C# and UI development, and scalable gameplay logic.

## Key Features

- **Color Deduction Gameplay**:
  - Fill each tile with the correct color using logic based on surrounding hints.
  - Lose a life with each incorrect guess, limited to three per game.
- **Grid Customization**:
  - Adjustable grid sizes for increased replayability and difficulty scaling.
- **Difficulty Levels**:
  - Multiple modes (e.g., Easy, Medium, Hard) that vary puzzle complexity and hint count.
- **Polished User Interface**:
  - Main menu system, settings, restart functionality, and game-over screens.
- **Replayability Enhancements**:
  - Dynamic grid generation
  - Randomized puzzle layouts
  - Visual feedback for right/wrong guesses

## Gameplay Loop

1. Launch into the main menu and select difficulty.
2. A new color grid puzzle is generated.
3. Use the provided hints to deduce color placement.
4. Fill the entire grid within three incorrect guesses to win.

## File Structure

```
ColorGridPuzzle/
├── Assets/
│   ├── Scripts/                # Game logic and behavior scripts
│   ├── Scenes/                 # Unity scenes (Menu, Game, Game Over)
│   ├── Prefabs/                # UI and grid components
│   └── Art/                    # Sprites, tiles, and UI icons
├── ProjectSettings/
├── README.md
```

## Requirements

- Unity Editor Version 2022.3.3f1
- Windows/macOS for build targets

## How to Play

1. Open the project in Unity.
2. Press Play from the main menu scene.
3. Select a grid size and difficulty.
4. Start guessing tiles based on hints.
5. Complete the board before losing all three lives.

## Technologies

- Unity Engine
- C# Scripting (MonoBehaviour-based architecture)
- Unity UI Toolkit (Canvas-based UI design)

---
