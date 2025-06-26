using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// Extensions for working with MagicSettings,
/// to make the API more fluent and easier to use.
/// </summary>
/// <remarks>
/// </remarks>
public static class MagicSettingsExtensions
{
    /// <summary>
    /// Fill in the PageState if not yet set.
    /// </summary>
    /// <remarks>
    /// This is to set the value if it's missing, but preserve any existing one.
    /// Alternative is <see cref="With{TSettings}"/>
    /// </remarks>
    /// <typeparam name="TSettings">The settings-type we're updating.</typeparam>
    /// <param name="settings">The initial settings object - can be null (in which case a fresh one is created)</param>
    /// <param name="pageState">The PageState to back-fill</param>
    /// <returns></returns>
    public static TSettings Refill<TSettings>(this TSettings? settings, PageState pageState)
        where TSettings : MagicSettings, new()
    {
        settings ??= new();
        return settings.PageState != null ? settings : settings with { PageState = pageState };
    }

    /// <summary>
    /// Fill in missing data of a specific type if the underlying value is not set.
    /// </summary>
    /// <remarks>
    /// This is to set the value if it's missing, but preserve any existing one.
    /// Alternative is <see cref="With{TSettings}"/>
    /// 
    /// This is used for various kinds of data which expects refilling.
    /// The target Settings must expect this kind of refill for the API to work.
    /// Otherwise, you would get a compile-time error.
    /// </remarks>
    /// <typeparam name="TSettings"></typeparam>
    /// <typeparam name="TWith"></typeparam>
    /// <param name="settings"></param>
    /// <param name="addition"></param>
    /// <returns></returns>
    public static TSettings Refill<TSettings, TWith>(this TSettings? settings, TWith? addition)
        where TSettings : MagicSettings, IWith<TWith?>, new()
        where TWith : class
    {
        settings ??= new();
        return settings.WithData != null
            ? settings
            : settings with { WithData = addition };
    }


    /// <summary>
    /// Add a PageState to a settings object.
    /// </summary>
    /// <remarks>
    /// This uses normal record `with` manipulations, so it creates a new object but preserves all other settings.
    /// Alternative when only filling empty <see cref="Refill{TSettings}"/>
    /// </remarks>
    /// <typeparam name="TSettings">The settings we're updating.</typeparam>
    /// <param name="settings">The initial settings object - can be null (in which case a fresh one is created)</param>
    /// <param name="pageState">The PageState</param>
    /// <param name="name">The name of default settings to load</param>
    /// <returns></returns>
    public static TSettings With<TSettings>(this TSettings? settings, PageState pageState, string? name = default)
        where TSettings : MagicSettings, new() =>
        settings != null
            ? settings with
            {
                PageState = pageState,
                Name = name ?? settings.Name,
            }
            : new()
            {
                PageState = pageState,
                Name = name,
            };




    public static TSettings With<TSettings, TWith>(this TSettings? settings, TWith? addition)
        where TSettings : MagicSettings, IWith<TWith?>, new()
        where TWith : class =>
        settings != null
            ? settings with { WithData = addition }
            : new() { WithData = addition };

}