namespace ToSic.Cre8magic.PageContext;

public record MagicPageContextSettings
{
    public bool? UseBodyTag { get; init; }

    public string? Classes { get; init; }

    public string? TagId { get; init; }
}