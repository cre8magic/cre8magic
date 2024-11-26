using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Tokens;

internal class ThemeTokens(MagicThemePackage themePackage) : ITokenReplace
{
    public const string NameIdConstant = nameof(ThemeTokens);

    internal MagicThemePackage ThemePackage { get; } = themePackage;

    public string NameId => NameIdConstant;

    public virtual string? Parse(string? value) =>
        value?.Replace(MagicTokens.ThemeUrl, ThemePackage.Url);
}