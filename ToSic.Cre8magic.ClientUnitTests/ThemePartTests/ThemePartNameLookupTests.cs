using ToSic.Cre8magic.Client;
using ToSic.Cre8magic.Client.Themes.Settings;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

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
        var themeParts = new NamedSettings<MagicThemePartSettings>
        {
            { Default, new() },
            { MenuNoRedirect, new() },
            { MenuRedirect, new() { Configuration = OtherSettingsName } },
            { FallbackToMainName, new() { Configuration = MagicConstants.InheritName } },
        };

        var nameResolver = new ThemePartNameResolver(ThemeName, themeParts);

        var (bestName, messages) = nameResolver.GetMostRelevantSettingsName(partName, MenuPrefix);
        Assert.Equal(expected, bestName);
    }
}