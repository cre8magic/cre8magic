using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using Oqtane.Models;
using ToSic.Cre8magic.Act;
using ToSic.Cre8magic.Themes;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.OqtaneBs5;

// Note: this is basically duplicate code from the OqtaneBasic.MagicTheme
// but we want to be sure that all the properties are in the docs
// which can't be done with inheritance

/// <inheritdoc cref="OqtaneBasic.MagicTheme" />
public abstract class MagicTheme: Oqtane.Themes.ThemeBase
{
    /// <inheritdoc cref="OqtaneBasic.MagicTheme.ThemePackage" />
    public abstract MagicThemePackage ThemePackage { get; }


    /// <inheritdoc cref="OqtaneBasic.MagicTheme.ThemeKit" />
    public IMagicThemeKit ThemeKit => _themeKitCache.Get(PageState, () => MagicAct.ThemeKit(new() { PageState = PageState }));
    private readonly CacheByPage<IMagicThemeKit> _themeKitCache = new();

    /// <inheritdoc cref="OqtaneBasic.MagicTheme.MagicAct" />
    [Inject]
    public required IMagicAct MagicAct
    {
        get => _magicAct;
        [MemberNotNull(nameof(_magicAct))]
        set => _magicAct = value.UseThemePackage(ThemePackage);
    }
    private IMagicAct _magicAct;

    /// <inheritdoc cref="OqtaneBasic.MagicTheme.OnParametersSet" />
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        // Provide the latest PageState on every change
        MagicAct.UsePageState(PageState);
    }

    /// <inheritdoc cref="OqtaneBasic.MagicTheme.OnAfterRenderAsync" />
    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        await base.OnAfterRenderAsync(isFirstRender);
        // Provide the latest PageState on every change
        // but OnAfterRenderAsync could be executed without OnParametersSet
        MagicAct.UsePageState(PageState);
        await MagicAct.AnalyticsKit(new() { PageState = PageState }).TrackPage(isFirstRender);
    }
}