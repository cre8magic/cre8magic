using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.Tokens;

internal class ThemeTokens(MagicPackageSettings themeSettings) : ITokenReplace
{
    public const string NameIdConstant = nameof(ThemeTokens);

    internal MagicPackageSettings PackageSettings { get; } = themeSettings;

    public string NameId => NameIdConstant;

    public virtual string? Parse(string? value) =>
        value?.Replace(MagicTokens.ThemeUrl, PackageSettings.Url);
}