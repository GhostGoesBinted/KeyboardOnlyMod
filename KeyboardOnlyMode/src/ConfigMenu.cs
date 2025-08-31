using System;
using System.Linq;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace KeyboardNavigation
{
    internal static class ConfigMenu
    {
        internal static void Register(Mod mod, IModHelper helper, Func<ModConfig> getConfig, Action<ModConfig> setConfig)
        {
            var gmcm = helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (gmcm is null)
                return;

            gmcm.Register(
                mod: mod.ModManifest,
                reset: () => setConfig(new ModConfig()),
                save: () => helper.WriteConfig(getConfig())
            );

            gmcm.AddBoolOption(
                mod: mod.ModManifest,
                getValue: () => getConfig().EnableToolbarCycle,
                setValue: v => { var c = getConfig(); c.EnableToolbarCycle = v; setConfig(c); },
                name: () => "Enable toolbar cycling",
                tooltip: () => "Allow cycling the toolbar with configured keys."
            );

            gmcm.AddKeybindList(
                mod: mod.ModManifest,
                getValue: () => ToKeybindList(getConfig().ToolbarPrevKey),
                setValue: kb => { var c = getConfig(); c.ToolbarPrevKey = FromKeybindList(kb); setConfig(c); },
                name: () => "Toolbar previous key",
                tooltip: () => "Default: Q"
            );

            gmcm.AddKeybindList(
                mod: mod.ModManifest,
                getValue: () => ToKeybindList(getConfig().ToolbarNextKey),
                setValue: kb => { var c = getConfig(); c.ToolbarNextKey = FromKeybindList(kb); setConfig(c); },
                name: () => "Toolbar next key",
                tooltip: () => "Default: E"
            );

            gmcm.AddKeybindList(
                mod: mod.ModManifest,
                getValue: () => new KeybindList(getConfig().ConfirmKeys.Select(k => new Keybind(k)).ToArray()),
                setValue: kb => { var c = getConfig(); c.ConfirmKeys = (kb?.Keybinds ?? Array.Empty<Keybind>()).SelectMany(k => k.Buttons ?? Array.Empty<SButton>()).Distinct().ToArray(); setConfig(c); },
                name: () => "Confirm keys (in menus)",
                tooltip: () => "Keys that trigger confirm in menus (simulate left-click)."
            );

            gmcm.AddKeybindList(
                mod: mod.ModManifest,
                getValue: () => ToKeybindList(getConfig().SecondaryActionKey),
                setValue: kb => { var c = getConfig(); c.SecondaryActionKey = FromKeybindList(kb); setConfig(c); },
                name: () => "Secondary action key (right-click)",
                tooltip: () => "Key for right-click action in menus. Default: C"
            );

            gmcm.AddBoolOption(
                mod: mod.ModManifest,
                getValue: () => getConfig().EnableSmartCursor,
                setValue: v => { var c = getConfig(); c.EnableSmartCursor = v; setConfig(c); },
                name: () => "Enable smart cursor",
                tooltip: () => "Move cursor to bottom-right when menus close."
            );
        }

        private static KeybindList ToKeybindList(SButton button)
        {
            return button == SButton.None ? new KeybindList() : new KeybindList(new Keybind(button));
        }

        private static SButton FromKeybindList(KeybindList list)
        {
            if (list == null)
                return SButton.None;
            var first = list.Keybinds?.FirstOrDefault(k => k?.Buttons != null && k.Buttons.Any());
            if (first?.Buttons == null || !first.Buttons.Any())
                return SButton.None;
            return first.Buttons.First();
        }
    }

    // Optional GMCM API (no direct dependency). Only required members.
    public interface IGenericModConfigMenuApi
    {
        void Register(IManifest mod, Action reset, Action save, bool titleScreenOnly = false);
        void AddBoolOption(IManifest mod, Func<bool> getValue, Action<bool> setValue, Func<string> name, Func<string>? tooltip = null, string? fieldId = null);
        void AddKeybindList(IManifest mod, Func<KeybindList> getValue, Action<KeybindList> setValue, Func<string> name, Func<string>? tooltip = null, string? fieldId = null);
    }
}
