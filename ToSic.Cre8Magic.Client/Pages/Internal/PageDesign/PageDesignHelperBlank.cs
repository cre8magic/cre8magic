using ToSic.Cre8magic.Tailors;

namespace ToSic.Cre8magic.Pages.Internal.PageDesign;

internal class PageDesignHelperBlank: IMagicTailor
{
    public string? Classes(string tag) => null;
    public string? Value(string key) => null;
    public string? Id(string target) => null;
}