namespace ToSic.Cre8magic.PageContexts;

public interface IMagicPageContextKit
{
    bool UseBodyTag { get; init; }
    string? TagId { get; init; }
    string? Classes { get; init; }
    MagicPageContextSettings? Settings { get; init; }
    Task UpdateBodyTag();
}