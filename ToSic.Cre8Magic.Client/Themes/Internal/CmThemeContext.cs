﻿using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Internal.Journal;

namespace ToSic.Cre8magic.Themes.Internal;

/// <summary>
/// Lightweight context, mainly used for retrieving settings parts.
/// </summary>
public record CmThemeContext
{
    [field: AllowNull, MaybeNull]
    internal ThemePartNameResolver NameResolver => field ??= new(Name, ThemeSettings.GetStable().Parts);

    /// <summary></summary>
    public required string Name { get; init; }

    /// <summary></summary>
    public required MagicThemeSettings ThemeSettings { get; init; }

    /// <summary></summary>
    public required Journal Journal { get; init; }
}