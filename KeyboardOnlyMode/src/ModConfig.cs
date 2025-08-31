using StardewModdingAPI;

namespace KeyboardNavigation
{
    /// <summary>
    /// Mod configuration for toolbar cycling and confirm keys.
    /// </summary>
    internal class ModConfig
    {
        /// <summary>Enable cycling the toolbar from the keyboard.</summary>
        public bool EnableToolbarCycle { get; set; } = true;
        /// <summary>Key to move to the next toolbar slot (default E).</summary>
        public SButton ToolbarNextKey { get; set; } = SButton.E;
        /// <summary>Key to move to the previous toolbar slot (default Q).</summary>
        public SButton ToolbarPrevKey { get; set; } = SButton.Q;
    /// <summary>Keys that confirm in menus (defaults to Enter and Space).</summary>
    public SButton[] ConfirmKeys { get; set; } = new[] { SButton.Enter, SButton.Space };
        /// <summary>Key for secondary action (right-click) in menus (default C). Set to None to disable.</summary>
        public SButton SecondaryActionKey { get; set; } = SButton.C;
        /// <summary>Enable smart cursor repositioning to bottom-right when menus close.</summary>
        public bool EnableSmartCursor { get; set; } = true;
    }
}
