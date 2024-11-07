namespace ToSic.Cre8magic.Client.Tokens;

public class ThemeTokens(MagicPackageSettings themeSettings) : ITokenReplace
{
    public const string NameIdConstant = nameof(ThemeTokens);

    internal MagicPackageSettings PackageSettings { get; } = themeSettings;

    public string NameId => NameIdConstant;

    public virtual string Parse(string value) =>
        value.Replace(MagicTokens.ThemeUrl, PackageSettings.Url);
}