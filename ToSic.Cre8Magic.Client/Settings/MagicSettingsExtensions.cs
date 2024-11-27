using Oqtane.UI;

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
    /// <returns></returns>
    public static TSettings With<TSettings>(this TSettings? settings, PageState pageState)
        where TSettings : MagicSettingsBase, new() =>
        (settings ?? new()) with
        {
            PageState = pageState
        };
}