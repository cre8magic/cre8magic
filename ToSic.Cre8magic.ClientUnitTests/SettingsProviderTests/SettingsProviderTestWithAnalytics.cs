using Microsoft.Extensions.DependencyInjection;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Internal.Startup;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Providers;
using ToSic.Cre8magic.Settings.Internal.Sources;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsProviderTests;

public class SettingsProviderTestWithAnalytics
{
    private const string DataValueOfOriginal = "test-inherits";

    /// <summary>
    /// Prepare a settings service and add a default value. 
    /// </summary>
    /// <returns></returns>
    private static (IMagicSettingsService settingsSvc, IMagicSettingsProvider SettingsProvider, MagicAnalyticsSettings DefaultSettings) PrepareSettings()
    {
        var di = SetupServices.Start().AddCre8magic().AddLogging().Finish();
        var settingsProvider = di.GetRequiredService<IMagicSettingsProvider>();
        var settingsSvc = di.GetRequiredService<IMagicSettingsService>();
        var original = new MagicAnalyticsSettings { GtmId = DataValueOfOriginal };
        settingsProvider.Analytics.SetDefault(original);
        return (settingsSvc, settingsProvider, original);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("name-not-found")]
    public void ValueOnlyNotNamed(string? name)
    {
        var (settingsSvc, settingsProvider, original) = PrepareSettings();
        var retrieved2 = settingsSvc.GetBestSettings(
            null,
            new MagicAnalyticsSettings { SettingsName = name },
            settingsSvc.Analytics,
            ContainerPrefix + "-",
            "Container"
        );
        Assert.Equal(original.GtmId, retrieved2.Data.GtmId);
    }

    private const string ContainerPrefix = "container";

    [Fact]
    public void FromDictionaryNoPrefixInData() =>
        FromDictionaryTests(null, true);

    private void FromDictionaryTests(string? prefixInData, bool shouldBeEqual, string addDicName = "admin", string searchName = "admin")
    {
        var addName = prefixInData + (string.IsNullOrEmpty(prefixInData) ? "" : "-") + addDicName;

        var (settingsSvc, settingsProvider, defaultSettings) = PrepareSettings();

        // Add a named setting which can be identified when found
        var namedSettings = new MagicAnalyticsSettings { GtmId = DataValueOfOriginal + "-named" };
        settingsProvider.Analytics.Provide(addName, namedSettings);

        var retrieved = settingsSvc.GetBestSettings(
            null,
            new MagicAnalyticsSettings { SettingsName = searchName },
            settingsSvc.Analytics,
            ContainerPrefix + "-",
            "Container"
        );

        Assert.Equal(shouldBeEqual, namedSettings.GtmId == retrieved.Data.GtmId);
        Assert.Equal(!shouldBeEqual, defaultSettings.GtmId == retrieved.Data.GtmId);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ", false)]
    //[InlineData(ContainerPrefix)] // this shouldn't work, as the prefix is really only used in parts-lookup, not settings-name
    [InlineData("some-other-prefix", false)]
    public void FromDictionaryWithPrefixInData(string? prefix, bool shouldBeEqual = true) =>
        FromDictionaryTests(prefix, shouldBeEqual);


    [Theory]
    [InlineData("admin", "admin")]
    [InlineData("default", "admin")]
    [InlineData("default", "whatever")]
    [InlineData("admin", "whatever", false)]
    [InlineData("admin", "default", false)]
    public void FromDictionaryFallbackDefault(string dicName, string searchName, bool shouldBeEqual = true) =>
        FromDictionaryTests(null, shouldBeEqual, dicName, searchName);

    [Fact]
    public void BothInterfacesOnServiceProviderGiveSameObject()
    {
        var serviceProvider = SetupServices.Start().AddCre8magic().AddStandardLogging().Finish();
        var original = serviceProvider.GetRequiredService<MagicSettingsProvider>();
        var settingsProvider = serviceProvider.GetRequiredService<IMagicSettingsProvider>();

        Assert.Equal(original, settingsProvider);

        var allSettingsProviders = serviceProvider.GetRequiredService<IEnumerable<IMagicSettingsSource>>();
        Assert.Contains(original, allSettingsProviders);
    }

}