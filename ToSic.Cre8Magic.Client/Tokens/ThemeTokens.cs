namespace ToSic.Cre8magic.Tokens;

internal class ThemeTokens(MagicThemePackage themePackage) : ITokenReplace
{
    public const string NameIdConstant = nameof(ThemeTokens);

    //internal MagicThemePackage ThemePackage { get; } = themePackage;

    public string NameId => NameIdConstant;

    public string? Parse(string? value) =>
        value?.Replace(MagicTokens.ThemeUrl, themePackage.Url);
}