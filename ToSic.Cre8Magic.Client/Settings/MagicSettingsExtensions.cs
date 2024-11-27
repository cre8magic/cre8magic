using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Settings;

public static class MagicSettingsExtensions
{
    /// <summary>
    /// Add a PageState to a settings object.
    /// This uses normal record `with` manipulations, so it creates a new object but preserves all other settings.
    /// </summary>
    /// <typeparam name="TSettings">The type of settings we're expanding with this PageState data.</typeparam>
    /// <param name="settings">The initial settings object - can be null (in which case a fresh one is created)</param>
    /// <param name="pageState">The PageState</param>
    /// <param name="overwrite">Determines if an existing value should be overwritten. If `false` (default) any original value is preserved.</param>
    /// <returns></returns>
    public static TSettings With<TSettings>(this TSettings? settings, PageState pageState, bool overwrite = false)
        where TSettings : MagicSettingsBase, new()
    {
        settings ??= new();
        return !overwrite && settings.PageState != null
            ? settings
            : settings with { PageState = pageState };
    }

    public static TSettings With<TSettings, TWith>(this TSettings? settings, TWith? addition, bool overwrite = false)
        where TSettings : MagicSettingsBase, IWith<TWith>, new()
        where TWith : class
    {
        settings ??= new();
        return !overwrite && settings.WithData != null
            ? settings
            : settings with { WithData = addition };
    }

}