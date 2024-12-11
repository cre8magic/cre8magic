using ToSic.Cre8magic.Spells.Internal;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.ClientUnitTests.ThemePartTests;

public class ThemePartNameLookupTests
{
    private const string ThemeName = "sideways";
    private const string OtherSettingsName = "other";
    private const string MenuPrefix = "menu-";

    private const string Default = "default";
    private const string MenuNoRedirect = "menu-no-redirects";
    private const string MenuRedirect = "menu-redirect";
    private const string FallbackToMainName = "fallback-to-main";
    private const string NonExisting = "non-existing";

    [Theory]
    [InlineData(MenuNoRedirect, MenuNoRedirect, "no redirects")]
    [InlineData(OtherSettingsName, MenuRedirect, "should find other")]
    [InlineData(Default, Default, "defaults, nothing special")]
    [InlineData(NonExisting, NonExisting, "non existing should preserve name")]
    [InlineData(ThemeName, MagicConstants.InheritName, "inherit should just use main name")]
    [InlineData(ThemeName, FallbackToMainName, "= in value should do fallback")]
    [InlineData(Default, "", "empty should use default")]
    [InlineData(Default, null, "null should use default")]
    public void CheckNameRedirects(string expected, string? partName, string? testDescription = default)
    {
        testDescription ??= "add this to prevent warning of unused variable";

        var themeParts = new Dictionary<string, MagicThemePartSettings>
        {
            { Default, new() },
            { MenuNoRedirect, new() },
            { MenuRedirect, new() { Settings = OtherSettingsName } },
            { FallbackToMainName, new() { Settings = MagicConstants.InheritName } },
        };

        var nameResolver = new ThemePartNameResolver(ThemeName, themeParts);

        var findSpecs = new FindSettingsSpecs(null, null, partName, null, ThemePartSectionEnum.Settings, null);
        var result = nameResolver.FindBestNameAccordingToParts(findSpecs);

        Assert.Equal(expected, result.Data);
    }
}