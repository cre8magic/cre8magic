﻿using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Themes.Internal.Json;

namespace ToSic.Cre8magic.Themes.Settings;

[JsonConverter(typeof(ThemePartJsonConverter))]
public record MagicThemePartSettings: ICanClone<MagicThemePartSettings>
{
    [PrivateApi]
    public MagicThemePartSettings() {}

    private MagicThemePartSettings(MagicThemePartSettings? priority, MagicThemePartSettings? fallback = default)
    {
        Show = priority?.Show ?? fallback?.Show;
        Design = priority?.Design ?? fallback?.Design;
        Settings = priority?.Settings ?? fallback?.Settings;
    }

    MagicThemePartSettings ICanClone<MagicThemePartSettings>.CloneUnder(MagicThemePartSettings? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);



    public MagicThemePartSettings(bool show)
    {
        Show = show;
        Design = null;
        Settings = null;
    }

    public MagicThemePartSettings(string name)
    {
        Show = true;
        Design = name;
        Settings = name;
    }

    /// <summary>
    /// Determines if this part should be shown or not.
    ///
    /// This allows you to configure to show / not show certain bits like breadcrumbs in certain scenarios.
    /// </summary>
    public bool? Show { get; init; }

    /// <summary>
    /// Name of the design settings to look up.
    /// </summary>
    public string? Design { get; init; }

    /// <summary>
    /// Name of the settings to look up.
    /// </summary>
    public string? Settings { get; init; }

    internal string? GetSettingName(ThemePartSectionEnum partSection) =>
        partSection switch
        {
            ThemePartSectionEnum.Design => Design,
            ThemePartSectionEnum.Settings => Settings,
            _ => null
        };

    /// <summary>
    /// Special Class just for json serialization, which will have the values but not the converter
    /// </summary>
    internal record NoJsonConverter : MagicThemePartSettings
    {
        public NoJsonConverter() { }    // for Deserialization
        public NoJsonConverter(MagicThemePartSettings original) : base(original) { } // For Serialization; explicit constructor, so original doesn't become a property
    }

}