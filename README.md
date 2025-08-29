# Keyboard‑Only Mode for Stardew Valley
Play comfortably without a mouse. This lightweight SMAPI mod adds:

- Menu confirm with your own keys (simulates a left‑click on the focused UI).
- Secondary action key for right-click in menus (default C).
- Optional toolbar cycling outside menus (Q/E by default).
- Cursor moves to the bottom‑right edge when menus close.

  
## ⌨️ Controls
- In menus: your configured ConfirmKeys trigger confirm. Escape/back is vanilla.
- In menus: press C for secondary action (right-click). Configurable or disable in settings.
- In gameplay: Q/E cycle the toolbar (configurable; on by default).

This mod doesn't change how you move around menus—it only adds reliable confirm, secondary action, and optional toolbar cycling.


## ✅ Requirements
- Stardew Valley 1.6+
- SMAPI 4.x (https://smapi.io/)

## 🧩 Install
1. Download or build the mod.
2. Copy the `KeyboardOnlyMode` folder into `Stardew Valley/Mods`.
3. Launch the game through SMAPI.

## 🎮 In‑game options (important)
In Options → Controls:
- Turn ON “Use controller‑style menus”.
- Set “Gamepad mode” to “Force on”.

These enable focus/snapping so confirm works reliably from the keyboard.

## ⌨️ Controls
- In menus: your configured ConfirmKeys trigger confirm. Escape/back is vanilla.
- In gameplay: Q/E cycle the toolbar (configurable; on by default).

This mod doesn’t change how you move around menus—it only adds reliable confirm and optional toolbar cycling.

## ⚙️ Config
Created on first run at `Mods/KeyboardOnlyMode/config.json`:

```json
{
	"EnableToolbarCycle": true,
	"ToolbarPrevKey": "Q",
	"ToolbarNextKey": "E",
	"ConfirmKeys": ["Enter", "Space"],
	"SecondaryActionKey": "C"
}
```

If you install Generic Mod Config Menu you can change all options in‑game.

## 📄 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

—
Issues or suggestions? Open an issue on the repository.
