using Microsoft.Extensions.DependencyInjection;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Internal.Startup;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Providers;
using ToSic.Cre8magic.Settings.Internal.Sources;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsProviderTests;

public class ContainerSettingsTests
{
    private const string DataValueOfOriginal = "test-inherits";
    /// <summary>
    /// Prepare a settings service and add a default value. 
    /// </summary>
    /// <returns></returns>
    private static (IMagicSettingsService settingsSvc, IMagicSettingsProvider SettingsProvider, MagicContainerSettings DefaultSettings) PrepareSettings()
    {
        var di = SetupServices.Start().AddCre8magic().AddLogging().Finish();
        var settingsProvider = di.GetRequiredService<IMagicSettingsProvider>();
        var settingsSvc = di.GetRequiredService<IMagicSettingsService>();
        var original = new MagicContainerSettings { TestData = DataValueOfOriginal };
        settingsProvider.Containers.SetDefault(original);
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
            new MagicContainerSettingsWip { SettingsName = name },
            settingsSvc.Containers,
            ContainerPrefix + "-",
            "Container"
        );
        Assert.Equal(original.TestData, retrieved2.Data.TestData);
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
        var namedSettings = new MagicContainerSettings { TestData = DataValueOfOriginal + "-named" };
        settingsProvider.Containers.Provide(addName, namedSettings);

        var retrieved = settingsSvc.GetBestSettings(
            null,
            new MagicContainerSettingsWip { SettingsName = searchName },
            settingsSvc.Containers,
            ContainerPrefix + "-",
            "Container"
        );

        Assert.Equal(shouldBeEqual, namedSettings.TestData == retrieved.Data.TestData);
        Assert.Equal(!shouldBeEqual, defaultSettings.TestData == retrieved.Data.TestData);
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