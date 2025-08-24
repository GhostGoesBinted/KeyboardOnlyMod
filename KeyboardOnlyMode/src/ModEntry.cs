using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

namespace KeyboardNavigation
{
    /// <summary>
    /// The mod entry point. Wires input handling for menu confirm and gameplay toolbar cycling,
    /// and recenters the cursor when menus close. Also registers config UI with GMCM if available.
    /// </summary>
    public class ModEntry : Mod
    {
        private ModConfig _config = new();

        /// <summary>
        /// SMAPI entry. Loads config, subscribes to input and menu events, and hooks GMCM.
        /// </summary>
        public override void Entry(IModHelper helper)
        {
            _config = helper.ReadConfig<ModConfig>() ?? new ModConfig();
            if (_config.ToolbarNextKey == SButton.None && _config.ToolbarCycleKey != SButton.None)
                _config.ToolbarNextKey = _config.ToolbarCycleKey;

            helper.Events.Input.ButtonPressed += OnButtonPressed;
            helper.Events.Display.MenuChanged += OnMenuChanged;
            helper.Events.GameLoop.GameLaunched += (_, _) => ConfigMenu.Register(
                mod: this,
                helper: this.Helper,
                getConfig: () => _config,
                setConfig: c => _config = c
            );
        }

        private void OnMenuChanged(object? sender, MenuChangedEventArgs e)
        {
            if (e.OldMenu != null && e.NewMenu == null)
            {
                int x = Game1.uiViewport.Width - 2;
                int y = Game1.uiViewport.Height - 2;
                if (x < 0) x = 0;
                if (y < 0) y = 0;
                Game1.setMousePosition(x, y);
            }
        }

        private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
        {
            if (!e.Button.TryGetKeyboard(out _))
                return;

            var menu = Game1.activeClickableMenu as IClickableMenu;

            if (menu != null)
            {
                if (e.Button == SButton.Enter || e.Button == SButton.Space || e.Button == SButton.Z)
                {
                    var cmp = menu.currentlySnappedComponent;
                    if (cmp != null)
                    {
                        var p = cmp.bounds.Center;
                        menu.receiveLeftClick(p.X, p.Y);
                    }
                    else
                    {
                        menu.receiveLeftClick(Game1.getMouseX(), Game1.getMouseY());
                    }
                    this.Helper.Input.Suppress(e.Button);
                }
                return;
            }

            if (_config.EnableToolbarCycle)
            {
                var f = Game1.player;
                if (f != null)
                {
                    int max = f.MaxItems;
                    if (max > 0)
                    {
                        if (_config.ToolbarNextKey != SButton.None && e.Button == _config.ToolbarNextKey)
                        {
                            f.CurrentToolIndex = (f.CurrentToolIndex + 1) % max;
                            this.Helper.Input.Suppress(e.Button);
                            return;
                        }
                        if (_config.ToolbarPrevKey != SButton.None && e.Button == _config.ToolbarPrevKey)
                        {
                            f.CurrentToolIndex = (f.CurrentToolIndex - 1 + max) % max;
                            this.Helper.Input.Suppress(e.Button);
                            return;
                        }
                    }
                }
            }
        }
    }
}
