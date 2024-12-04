using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Languages;

/// <summary>
/// Language Design Settings
/// </summary>
public record MagicLanguageBlueprint : MagicBlueprints, ICanClone<MagicLanguageBlueprint>
{
    [PrivateApi]
    public MagicLanguageBlueprint() { }

    private MagicLanguageBlueprint(MagicLanguageBlueprint? priority, MagicLanguageBlueprint? fallback = default)
        : base(priority, fallback)
    { }

    MagicLanguageBlueprint ICanClone<MagicLanguageBlueprint>.CloneUnder(MagicLanguageBlueprint? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);


    internal static Defaults<MagicLanguageBlueprint> DesignDefaults = new()
    {
        Fallback = new()
        {
            Parts = new()
            {
                { "li", new() { IsActive = new() { On = "active" } } }
            },
        },
        Foundation = new()
    };
}