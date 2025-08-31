using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using System.Linq;

namespace KeyboardNavigation
{
    /// <summary>
    /// The mod entry point. Wires input handling for menu confirm and gameplay toolbar cycling,
    /// and recenters the cursor when menus close. Also registers config UI with GMCM if available.
    /// </summary>
    public class ModEntry : Mod
    {
        private ModConfig _config = new();

       
    public override void Entry(IModHelper helper)
        {
            _config = helper.ReadConfig<ModConfig>() ?? new ModConfig();

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
                if (TryHandleConfirmKey(e.Button, menu))
                    return;
                
                if (TryHandleSecondaryActionKey(e.Button, menu))
                    return;
                
                return;
            }

            TryHandleToolbarCycling(e.Button);
        }

        /// <summary>Gets the click coordinates for the current menu context.</summary>
        private (int x, int y) GetMenuClickPosition(IClickableMenu menu)
        {
            var cmp = menu.currentlySnappedComponent;
            if (cmp != null)
            {
                var center = cmp.bounds.Center;
                return (center.X, center.Y);
            }
            return (Game1.getMouseX(), Game1.getMouseY());
        }

        /// <summary>Attempts to get the current toolbar page, with fallback calculation.</summary>
        private int GetCurrentToolbarPage(StardewValley.Farmer player)
        {
            try
            {
                var field = typeof(StardewValley.Farmer).GetField("currentParentTileIndex", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var netField = field?.GetValue(player);
                var value = netField?.GetType().GetProperty("Value")?.GetValue(netField);
                return (int)(value ?? 0);
            }
            catch
            {
                return player.CurrentToolIndex / 12; // fallback
            }
        }

        /// <summary>Handles confirm key presses in menus by simulating left-click.</summary>
        private bool TryHandleConfirmKey(SButton button, IClickableMenu menu)
        {
            var confirmButtons = _config.ConfirmKeys ?? System.Array.Empty<SButton>();
            if (!confirmButtons.Contains(button))
                return false;

            var (x, y) = GetMenuClickPosition(menu);
            menu.receiveLeftClick(x, y);
            this.Helper.Input.Suppress(button);
            return true;
        }

        /// <summary>Handles secondary action key presses in menus by simulating right-click.</summary>
        private bool TryHandleSecondaryActionKey(SButton button, IClickableMenu menu)
        {
            if (_config.SecondaryActionKey == SButton.None || button != _config.SecondaryActionKey)
                return false;

            var (x, y) = GetMenuClickPosition(menu);
            menu.receiveRightClick(x, y);
            this.Helper.Input.Suppress(button);
            return true;
        }

        /// <summary>Handles toolbar cycling within the current page.</summary>
        private void TryHandleToolbarCycling(SButton button)
        {
            if (!_config.EnableToolbarCycle)
                return;

            var player = Game1.player;
            if (player == null)
                return;

            int max = player.MaxItems;
            if (max <= 0)
                return;

            // Get current page and slot within that page (12 slots per page)
            int currentPage = GetCurrentToolbarPage(player);
            int currentSlotInPage = player.CurrentToolIndex % 12;
            int pageStartIndex = currentPage * 12;

            if (_config.ToolbarNextKey != SButton.None && button == _config.ToolbarNextKey)
            {
                // Cycle to next slot within current page, wrap to slot 0 if at end
                int newSlotInPage = (currentSlotInPage + 1) % 12;
                player.CurrentToolIndex = pageStartIndex + newSlotInPage;
                this.Helper.Input.Suppress(button);
            }
            else if (_config.ToolbarPrevKey != SButton.None && button == _config.ToolbarPrevKey)
            {
                // Cycle to previous slot within current page, wrap to slot 11 if at start
                int newSlotInPage = (currentSlotInPage - 1 + 12) % 12;
                player.CurrentToolIndex = pageStartIndex + newSlotInPage;
                this.Helper.Input.Suppress(button);
            }
        }

    }
}
