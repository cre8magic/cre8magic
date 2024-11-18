using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Languages;

public record MagicLanguageSettings : SettingsWithInherit, IHasDebugSettings, ICanClone<MagicLanguageSettings>
{
    /// <summary>
    /// Dummy constructor so better find cases where it's created
    /// Note it must be without parameters for json deserialization
    /// </summary>
    public MagicLanguageSettings() {}

    public MagicLanguageSettings(MagicLanguageSettings? priority, MagicLanguageSettings? fallback = default)
        : base(priority, fallback)
    {
        HideOthers = priority?.HideOthers ?? fallback?.HideOthers ?? Defaults.Fallback.HideOthers;
        Debug = priority?.Debug ?? fallback?.Debug;
        Languages = priority?.Languages ?? fallback?.Languages;
    }

    public MagicLanguageSettings CloneWith(MagicLanguageSettings? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

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
        if (dic == null)
            return null;

        // Ensure each config knows what culture it's for, as
        var extended = dic.ToDictionary(
            pair => pair.Key,
            pair => pair.Value with { Culture = pair.Key }
        );

        return new(extended);
    }

    internal static Defaults<MagicLanguageSettings> Defaults = new()
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