namespace ToSic.Cre8magic.Languages.Settings;

public record MagicLanguagesSettings : SettingsWithInherit, IHasDebugSettings
{
    /// <summary>
    /// Dummy constructor so better find cases where it's created
    /// Note it must be without parameters for json deserialization
    /// </summary>
    public MagicLanguagesSettings() {}

    /// <summary>
    /// If true, will only show the languages which are explicitly configured.
    /// If false, will first show the configured languages, then the rest. 
    /// </summary>
    public bool HideOthers { get; init; } = false;

    /// <inheritdoc />
    public MagicDebugSettings? Debug { get; init; }

    /// <summary>
    /// List of languages
    /// </summary>
    public NamedSettings<MagicLanguage>? Languages
    {
        get => _languages;
        init => _languages = InitList(value);
    }
    private readonly NamedSettings<MagicLanguage>? _languages;

    private NamedSettings<MagicLanguage>? InitList(NamedSettings<MagicLanguage>? dic)
    {
        if (dic == null) return null;
        // Ensure each config knows what culture it's for, as 
        foreach (var set in dic)
            set.Value.Culture ??= set.Key;
        return dic;
    }

    internal static Defaults<MagicLanguagesSettings> Defaults = new()
    {
        Fallback = new()
        {
            HideOthers = false,
            Languages = new()
            {
                { "en", new() { Culture = "en", Description = "English" } }
            },
        },
        Foundation = new()
        {
            HideOthers = false,
            Languages = new(),
        }
    };
}