using Oqtane.UI;
using ToSic.Cre8magic.Spells.Internal;

namespace ToSic.Cre8magic.Spells;

/// <summary>
/// Extensions for working with MagicSettings,
/// to make the API more fluent and easier to use.
/// </summary>
/// <remarks>
/// Despite it being related to Settings (which would normally be in [](xref:ToSic.Cre8magic.Spells)),
/// it's in the Act namespace because it's usually used in the basic work which starts in this namespace.
///
/// This makes it easier for most components to just use this namespace and little else.
/// </remarks>
public static class MagicSpellExtensions
{
    /// <summary>
    /// Fill in the PageState if not yet set.
    /// This is to set the value if it's missing, but preserve any existing one.
    /// Alternative is <see cref="With{TSettings}"/>
    /// </summary>
    /// <typeparam name="TSpell">The settings-type we're updating.</typeparam>
    /// <param name="settings">The initial settings object - can be null (in which case a fresh one is created)</param>
    /// <param name="pageState">The PageState to back-fill</param>
    /// <returns></returns>
    public static TSpell Refill<TSpell>(this TSpell? settings, PageState pageState)
        where TSpell : MagicSpellBase, new()
    {
        settings ??= new();
        return settings.PageState != null ? settings : settings with { PageState = pageState };
    }


    /// <summary>
    /// Add a PageState to a settings object.
    /// This uses normal record `with` manipulations, so it creates a new object but preserves all other settings.
    /// Alternative when only filling empty <see cref="Refill{TSettings}"/>
    /// </summary>
    /// <typeparam name="TSpell">The settings-type we're updating.</typeparam>
    /// <param name="settings">The initial settings object - can be null (in which case a fresh one is created)</param>
    /// <param name="pageState">The PageState</param>
    /// <returns></returns>
    public static TSpell With<TSpell>(this TSpell? settings, PageState pageState)
        where TSpell : MagicSpellBase, new() =>
        settings != null
            ? settings with { PageState = pageState }
            : new() { PageState = pageState };



    public static TSpell Refill<TSpell, TWith>(this TSpell? settings, TWith? addition)
        where TSpell : MagicSpellBase, IWith<TWith?>, new()
        where TWith : class
    {
        settings ??= new();
        return settings.WithData != null ? settings : settings with { WithData = addition };
    }

    public static TSpell With<TSpell, TWith>(this TSpell? settings, TWith? addition)
        where TSpell : MagicSpellBase, IWith<TWith?>, new()
        where TWith : class =>
        settings != null
            ? settings with { WithData = addition }
            : new() { WithData = addition };

    public static TSpell RefillLookup<TSpell>(this TSpell? settings, MagicSpellLookup lookup)
        where TSpell : MagicSpellBase, new() =>
        (settings ??= new()) with
        {
            PartName = settings.PartName ?? lookup.PartName,
            SettingsName = settings.SettingsName ?? lookup.SpellName,
            DesignName = settings.DesignName ?? lookup.BlueprintName,
        };

    public static TSpell WithLookup<TSpell>(this TSpell? settings, MagicSpellLookup lookup)
        where TSpell : MagicSpellBase, new() =>
        (settings ??= new()) with
        {
            PartName = lookup.PartName ?? settings.PartName,
            SettingsName = lookup.SpellName ?? settings.SettingsName,
            DesignName = lookup.BlueprintName ?? settings.DesignName,
        };
}