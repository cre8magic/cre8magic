using ToSic.Cre8magic.Settings.Internal.Docs;

namespace ToSic.Cre8magic.Languages;

public record MagicLanguageSettingsPubWip: MagicLanguageSettings, ISettingsForCodeUse
{
    public MagicLanguageSettingsPubWip() { }

    /// <summary>
    /// Constructor to re-hydrate from object of base class.
    /// </summary>
    internal MagicLanguageSettingsPubWip(MagicLanguageSettings ancestor, MagicLanguageSettingsPubWip? original) : base(ancestor)
    {
        if (original == null)
            return;
        
        PartName = original.PartName;
        SettingsName = original.SettingsName;
        DesignName = original.DesignName;
    }
    
    /// <inheritdoc/>
    public string? PartName { get; init; }

    /// <inheritdoc/>
    public string? SettingsName { get; init; }

    public string? DesignName { get; init; }
}