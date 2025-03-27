using System.Diagnostics.CodeAnalysis;
using Oqtane.UI;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Tokens;
using static ToSic.Cre8magic.MagicConstants;
// ReSharper disable RedundantAccessorBody

namespace ToSic.Cre8magic.Settings.Internal;

/// <summary>
/// Service which consolidates settings made in the UI, in the JSON and falls back to coded defaults.
/// </summary>
internal class MagicSettingsService(MagicLibraryLoader libraryLoader) : IMagicSettingsService, IHasMagicLibrary
{
    /// <inheritdoc />>
    public IMagicSettingsService Setup(MagicThemePackage themePackage)
    {
        ThemePackage = themePackage;
        ThemeTokens = null!;
        return this;
    }

    private string? Variant => ThemePackage.Name;

    public IMagicSettingsService UsePageState(PageState pageState)
    {
        PageState = pageState;
        return this;
    }

    /// <summary>
    /// PageState for this service, if shared/broadcast from the theme
    /// </summary>
    public PageState? PageState { get; private set; }

    public MagicDebugSettings.Stabilized GetDebug(PageState pageState) =>
        new MagicDebugSettings(((IMagicSettingsService)this).Debug, GetThemeContext(pageState).ThemeSettings.Debug).GetStable();

    [field: AllowNull, MaybeNull]
    MagicDebugSettings IMagicSettingsService.Debug => field
        ??= Library.FirstOrDefault(c => c.Data.Debug != null)?.Data?.Debug ?? new();

    [field: AllowNull, MaybeNull]
    public MagicThemePackage ThemePackage { get => field ??= MagicThemePackage.Fallback; private set => field = value; }

    /// <summary>
    /// Tokens engine for this specific PageState
    /// </summary>
    /// <param name="pageState"></param>
    /// <returns></returns>
    public TokenEngine PageTokenEngine(PageState pageState)
    {
        var pageFactory = new MagicPageFactory(pageState);
        var pageTokens = new PageTokens(pageFactory.Current, Variant);
        var tokens = new TokenEngine([pageTokens, ThemeTokens]);
        return tokens;
    }

    [field: AllowNull, MaybeNull]
    private ThemeTokens ThemeTokens { get => field ??= new(ThemePackage); set => field = value; }

    #region Theme Context

    /// <inheritdoc />
    public CmThemeContext GetThemeContext(PageState? pageStateForCachingOnly)
    {
        var nameForCache = (Variant ?? "prevent-error") + (pageStateForCachingOnly?.Page.PageId ?? -1);
        if (pageStateForCachingOnly != null && _themeCtxCache.TryGetValue(key: nameForCache, value: out var cached2))
            return cached2;

        // Figure out real config-name, and get the initial layout
        var (settingsName, nameJournal) = string.IsNullOrEmpty(Variant)
            ? (Default, new Journal(["no name, use default"], []))
            : (Variant, new Journal([$"Name: '{Variant}'"], []));

        var themeSettings = Themes.FindAndNeutralize(settingsName);

        var ctx = new CmThemeContext
        {
            Name = settingsName,
            ThemeSettings = themeSettings,
            Journal = nameJournal
        };
        if (pageStateForCachingOnly != null)
            _themeCtxCache[nameForCache] = ctx;
        return ctx;
    }
    private readonly Dictionary<string, CmThemeContext> _themeCtxCache = new(StringComparer.InvariantCultureIgnoreCase);

    public CmThemeContextFull GetThemeContextFull(PageState pageState)
    {
        var originalNameForCache = (Variant ?? "prevent-error") + pageState.Page.PageId;
        if (_themeCtxFullCache.TryGetValue(originalNameForCache, out var cached2))
            return cached2;

        var ctxLight = GetThemeContext(pageState);

        // Tokens engine for this specific PageState
        var pageTokens = PageTokenEngine(pageState);

        var blueprint = ThemeBlueprints.FindAndNeutralize(ctxLight.Name);
        var ctx = new CmThemeContextFull
        {
            Name = ctxLight.Name,
            ThemeSettings = ctxLight.ThemeSettings,
            Journal = ctxLight.Journal,

            PageState = pageState,
            PageTokens = pageTokens,
            ThemeBlueprint = blueprint,
        };
        _themeCtxFullCache[originalNameForCache] = ctx;
        return ctx;
    }

    private readonly Dictionary<string, CmThemeContextFull> _themeCtxFullCache = new(StringComparer.InvariantCultureIgnoreCase);

    #endregion


    /// <summary>
    /// actually internal
    /// </summary>
    public List<DataWithJournal<MagicBook>> Library =>
        libraryLoader.Books(ThemePackage, cache: false);

    #region Generic Readers

    /// <summary>
    /// Get a reader from a section of the book.
    /// </summary>
    /// <typeparam name="TSettings"></typeparam>
    /// <returns></returns>
    public SettingsReader<TSettings> GetReader<TSettings>()
        where TSettings : class, new() =>
        new(this, book => book.GetSection<TSettings>());

    #endregion

    #region Themes

    [field: AllowNull, MaybeNull]
    private SettingsReader<MagicThemeSettings> Themes => field
        ??= new(this, book => book.Themes);

    [field: AllowNull, MaybeNull]
    public SettingsReader<MagicThemeBlueprint> ThemeBlueprints => field
        ??= new(this, book => book.ThemeBlueprints);

    #endregion


}