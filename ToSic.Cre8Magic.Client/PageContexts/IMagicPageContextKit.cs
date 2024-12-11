namespace ToSic.Cre8magic.PageContexts;

public interface IMagicPageContextKit
{
    bool UseBodyTag { get; }
    string? TagId { get; }
    string? Classes { get; }
    MagicPageContextSpell Spell { get; }
    Task UpdateBodyTag();
}