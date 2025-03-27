﻿using System.Text.Json.Serialization;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Internal.Json;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.PageContexts;
using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Settings.Debug.Internal;
using ToSic.Cre8magic.Themes.Settings;
using static System.StringComparer;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// This is a Book; a catalog of all kinds of settings.
/// It serves as a kind of database to manage all settings, which will usually be retrieved using a name. 
/// </summary>
public record MagicBook: IHasDebugSettings
{
    /// <summary>
    /// Empty Constructor so it can be created in code or Json-Deserialized
    /// </summary>
    public MagicBook() { }

    public MagicChapter Chapter { init => LoadChapter(value); }

    public IEnumerable<MagicChapter> Chapters { init => value?.ToList().ForEach(LoadChapter); }

    private void LoadChapter(MagicChapter? chapter)
    {
        if (chapter == null) return;

        var name = string.IsNullOrWhiteSpace(chapter.Name) ? "default" : chapter.Name;
        if (chapter.Theme != null) Themes[name] = chapter.Theme;
        if (chapter.ThemeBlueprint != null) ThemeBlueprints[name] = chapter.ThemeBlueprint;
        if (chapter.Analytics != null) Analytics[name] = chapter.Analytics;
        if (chapter.Breadcrumb != null) Breadcrumbs[name] = chapter.Breadcrumb;
        if (chapter.BreadcrumbBlueprint != null) BreadcrumbBlueprints[name] = chapter.BreadcrumbBlueprint;
        if (chapter.Container != null) Containers[name] = chapter.Container;
        if (chapter.ContainerBlueprint != null) ContainerBlueprints[name] = chapter.ContainerBlueprint;
        if (chapter.Language != null) Languages[name] = chapter.Language;
        if (chapter.LanguageBlueprint != null) LanguageBlueprints[name] = chapter.LanguageBlueprint;
        if (chapter.Menu != null) Menus[name] = chapter.Menu;
        if (chapter.MenuBlueprint != null) MenuBlueprints[name] = chapter.MenuBlueprint;
        if (chapter.PageContext != null) PageContexts[name] = chapter.PageContext;
    }

    internal const string SourceDefault = "Unknown";
    /// <summary>
    /// Version number when loading from JSON to verify it's what we expect
    /// </summary>
    public float Version { get; init; }

    /// <summary>
    /// Master debug settings - would override other debugs
    /// </summary>
    public MagicDebugSettings? Debug { get; init; } = new();

    /// <summary>
    /// Source of these settings / where they came from, to ensure that we can see in debug where a value was picked up from
    /// </summary>
    public string Source { get; set; } = SourceDefault;

    /// <summary>
    /// List of Themes and mainly what parts they want to explicitly configure (e.g. determine Show, and there these parts find their settings)
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicThemeSettings>))]
    public Dictionary<string, MagicThemeSettings> Themes { get; init; } = new(InvariantCultureIgnoreCase);

    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicThemeBlueprint>))]
    public Dictionary<string, MagicThemeBlueprint> ThemeBlueprints { get; init; } = new(InvariantCultureIgnoreCase);


    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicAnalyticsSettings>))]
    public Dictionary<string, MagicAnalyticsSettings> Analytics { get; init; } = new(InvariantCultureIgnoreCase);



    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicBreadcrumbSettings>))]
    public Dictionary<string, MagicBreadcrumbSettings> Breadcrumbs { get; init; } = new(InvariantCultureIgnoreCase);

    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicBreadcrumbBlueprint>))]
    public Dictionary<string, MagicBreadcrumbBlueprint> BreadcrumbBlueprints { get; init; } = new(InvariantCultureIgnoreCase);



    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicContainerSettings>))]
    public Dictionary<string, MagicContainerSettings> Containers { get; init; } = new(InvariantCultureIgnoreCase);

    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicContainerBlueprint>))]
    public Dictionary<string, MagicContainerBlueprint> ContainerBlueprints { get; init; } = new(InvariantCultureIgnoreCase);

    /// <summary>
    /// Language Settings
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicLanguageSettings>))]
    public Dictionary<string, MagicLanguageSettings> Languages { get; init; } = new(InvariantCultureIgnoreCase);

    /// <summary>
    /// Design definitions of languages
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicLanguageBlueprint>))]
    public Dictionary<string, MagicLanguageBlueprint> LanguageBlueprints { get; init; } = new(InvariantCultureIgnoreCase);


    /// <summary>
    /// The menu definitions
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicMenuSettings>))]
    public Dictionary<string, MagicMenuSettings> Menus { get; init; } = new(InvariantCultureIgnoreCase);

    /// <summary>
    /// Design definitions of the menu
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicMenuBlueprint>))]
    public Dictionary<string, MagicMenuBlueprint> MenuBlueprints { get; init; } = new(InvariantCultureIgnoreCase);

    /// <summary>
    /// The menu definitions
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicPageContextSettings>))]
    public Dictionary<string, MagicPageContextSettings> PageContexts { get; init; } = new(InvariantCultureIgnoreCase);

    internal static MagicBook Fallback = new()
    {
        Version = -1.0f,
        Source = "Fallback",
    };
}