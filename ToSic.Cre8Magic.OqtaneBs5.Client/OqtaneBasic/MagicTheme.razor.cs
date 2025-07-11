﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using ToSic.Cre8magic.Act;
using ToSic.Cre8magic.Themes;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.OqtaneBasic;

/// <summary>
/// Base class for our themes. It's responsible for
///
/// 1. Some basic properties such as Name, BodyClasses etc. which each theme can configure
/// 2. Adding special classes to the body tag so that the CSS can best optimize for each scenario
/// </summary>
/// <remarks>
/// - The base class must be abstract, so that Oqtane doesn't see it as a real them.
/// - The config-properties must be abstract, so the inheriting files are forced to set them. 
/// </remarks>
public abstract class MagicTheme : Oqtane.Themes.ThemeBase
{
    /// <inheritdoc cref="IMagicThemeDocs.ThemePackage" />
    public abstract MagicThemePackage ThemePackage { get; }

    /// <inheritdoc cref="IMagicThemeDocs.ThemeKit" />
    public IMagicThemeKit ThemeKit => _themeKit.Get(PageState, () => MagicAct.ThemeKit(new() { PageState = PageState }));
    private readonly CacheByPage<IMagicThemeKit> _themeKit = new();

    /// <inheritdoc cref="IMagicThemeDocs.MagicAct" />
    [Inject]
    [field: AllowNull, MaybeNull]
    public required IMagicAct MagicAct
    {
        get => field ?? throw new ArgumentException($"{nameof(MagicAct)} must be provided by DI.");
        set => field = value.UseThemePackage(ThemePackage);
    }

    /// <inheritdoc cref="IMagicThemeDocs.OnParametersSet" />
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        // Provide the latest PageState on every change
        MagicAct.UsePageState(PageState);
    }
}