# Keyboard-Only Mode (SMAPI mod)

Forces controller-style snappy menus and binds Enter/Space/Z to confirm, Esc/X/Back to cancel, arrows to navigate, and Q/E to scroll. Optional: Q/E cycles toolbar outside menus.

## Build

1. Install .NET SDK 6.0+.
2. Set STARDEW_PATH environment variable to your Stardew install folder (contains `Stardew Valley.exe`).
3. Build the project; it will auto-copy to the Mods folder if STARDEW_PATH is set:
   - `dotnet build -c Release`
   - Optional: specify another game path `dotnet build -c Release /p:GamePath="C:\\Path\\To\\Stardew Valley"`

If no path is set, copy `KeyboardOnlyMode.dll` + `manifest.json` into `%STARDEW_PATH%/Mods/KeyboardOnlyMode` manually.

## Config

A `config.json` will be created on first run next to your mod. Keys:

- Confirm1/2/3 (default: Enter/Space/Z)
- Cancel1/2/3 (default: Escape/X/Back)
- ScrollPrev/ScrollNext (default: Q/E)
- ToolbarCycleOutsideMenus (default: true)
- ForceSnappyMenus (default: true)
- PreferGamepadPrompts (default: true)

## Notes

- Works on Stardew 1.6+ and SMAPI 4.x. If a menu doesn't implement snapping, the mod falls back safely.
