using ToSic.Cre8magic.Settings.Internal.Docs;
using ToSic.Cre8magic.Settings.Internal.Journal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// Helper to figure out what name we should use for the settings we retrieve.
/// Reason is that a Theme can have many parts - such as the "menu", "sidebar", etc.
/// and these in turn can have many different variations of settings.
///
/// This helper will
/// 1. First resolve any keys such as "=" to the main name
/// 2. If no name was provided, or it is blank, will use "default"
/// 3. Then check the parts list for further renames
/// </summary>
/// <param name="mainName"></param>
/// <param name="themeSettingsParts"></param>
internal class ThemePartNameResolver(string mainName, Dictionary<string, MagicThemePartSettings> themeSettingsParts)
{
    internal ThemePartNameResolver(CmThemeContext themeCtx)
        : this(themeCtx.SettingsName, themeCtx.ThemeSpell.Parts)
    { }

    /// <summary>
    /// Generic method to check for names, since it could be run on the Settings property or on the DesignSettings property
    /// </summary>
    internal DataWithJournal<string> FindBestNameAccordingToParts(FindSettingsSpecs specs)
    {
        var (initialName, journal) = PickBestSettingsName(specs.PartName, mainName);

        // Check if we have a name-remap to consider
        // If the first test fails, we try again with the prefix
        var betterName = themeSettingsParts.TryGetValue(initialName, out var part)
            ? part.GetSettingName(specs.Section)
            : null;

        // If the better name wants to use the main config name ("=") then use that and exit
        if (betterName == MagicConstants.InheritName)
            return new(mainName, journal.With($"switched to inherit '{mainName}'"));

        if (betterName == null && !string.IsNullOrEmpty(specs.Prefix) && !initialName.StartsWith(specs.Prefix))
            betterName = themeSettingsParts.TryGetValue($"{specs.Prefix}{initialName}", out part)
                ? part.GetSettingName(specs.Section)
                : null;

        if (!betterName.HasValue())
            return new(initialName, journal);

        return new(betterName, journal.With($"updated config to '{initialName}'"));

    }

    /// <summary>
    /// Figure out which settings-name can be used (not empty, using "=", etc.).
    /// It will first try the preferred option, then the fallback, and if that doesn't exist, it will use the default name.
    /// </summary>
    /// <param name="preferred"></param>
    /// <param name="mainName"></param>
    /// <returns></returns>
    internal static DataWithJournal<string> PickBestSettingsName(string? preferred, string mainName)
    {
        var journal = new List<string> { $"Initial Settings Name: '{preferred}'" };

        preferred = preferred?.Trim();

        // Check if it's just a "=" symbol, which means "inherit"
        if (preferred == MagicConstants.InheritName)
        {
            preferred = mainName;
            journal.Add($"switched to inherit '{mainName}'");
        }

        // If we have a value, use that
        if (preferred.HasText())
            return new(preferred, new(journal, []));

        // If we don't have a preferred name, then don't use the main name (could be "Sidebar" or something) but instead use Default
        journal.Add($"Settings Name changed to '{MagicConstants.Default}'");
        return new(MagicConstants.Default, new(journal, []));
    }

}