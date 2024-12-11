using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Spells;
using ToSic.Cre8magic.Spells.Internal;

namespace ToSic.Cre8magic.UserLogins;

public record MagicUserLoginSpell : MagicSpellBase, ICanClone<MagicUserLoginSpell>
{
    /// <summary>
    /// Dummy constructor so better find cases where it's created
    /// Note it must be without parameters for json deserialization
    /// </summary>
    [PrivateApi]
    public MagicUserLoginSpell() {}

    private MagicUserLoginSpell(MagicUserLoginSpell? priority, MagicUserLoginSpell? fallback = default)
        : base(priority, fallback)
    {
        //DesignSettings = priority?.DesignSettings ?? fallback?.DesignSettings;
    }

    MagicUserLoginSpell ICanClone<MagicUserLoginSpell>.CloneUnder(MagicUserLoginSpell? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);


    //[JsonIgnore]
    //public MagicLanguageDesignSettings? DesignSettings { get; init; }

}