using Microsoft.Extensions.DependencyInjection;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsProviderTests;

public class ContainerSettingsTests
{
    /// <summary>
    /// Prepare a settings service and add a defaut value. 
    /// </summary>
    /// <returns></returns>
    private static (IMagicSettingsProviders SettingsSvc, MagicContainerSettings DefaultSettings) PrepareSettings()
    {
        var settingsSvc = SetupServices.PrepareServices().GetRequiredService<IMagicSettingsProviders>();
        var original = new MagicContainerSettings();
        settingsSvc.ContainerSettings.Provide(original);
        return (settingsSvc, original);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("name-not-found")]
    public void ValueOnlyNotNamed(string? name)
    {
        var (settingsSvc, original) = PrepareSettings();
        var retrieved = settingsSvc.ContainerSettings.Find(new MagicSettingsContext { Name = name });
        Assert.Equal(original, retrieved);
    }

    private const string ContainerPrefix = "container";

    [Fact]
    public void FromDictionaryNoPrefixInData() =>
        FromDictionaryTests(null, true);

    private void FromDictionaryTests(string? prefixInData, bool shouldBeEqual, string addDicName = "admin", string searchName = "admin")
    {
        var addName = prefixInData + (string.IsNullOrEmpty(prefixInData) ? "" : "-") + addDicName;
        var (settingsSvc, defaultSettings) = PrepareSettings();
        var namedSettings = new MagicContainerSettings();
        settingsSvc.ContainerSettings.Provide(addName, namedSettings);

        var retrieved = settingsSvc.ContainerSettings.Find(new MagicSettingsContext { Name = searchName, Prefix = ContainerPrefix });
        Assert.Equal(shouldBeEqual, namedSettings == retrieved);
        Assert.Equal(!shouldBeEqual, defaultSettings == retrieved);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ", false)]
    [InlineData(ContainerPrefix)]
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

}