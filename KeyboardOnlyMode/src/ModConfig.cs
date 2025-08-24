using StardewModdingAPI;

namespace KeyboardNavigation
{
    /// <summary>
    /// Mod configuration for toolbar cycling and legacy mapping support.
    /// </summary>
    internal class ModConfig
    {
        /// <summary>Enable cycling the toolbar from the keyboard.</summary>
        public bool EnableToolbarCycle { get; set; } = true;
        /// <summary>Key to move to the next toolbar slot (default E).</summary>
        public SButton ToolbarNextKey { get; set; } = SButton.E;
        /// <summary>Key to move to the previous toolbar slot (default Q).</summary>
        public SButton ToolbarPrevKey { get; set; } = SButton.Q;
        /// <summary>Legacy single-key setting; mapped to <see cref="ToolbarNextKey"/> on load if set.</summary>
        public SButton ToolbarCycleKey { get; set; } = SButton.None;
    }
}
