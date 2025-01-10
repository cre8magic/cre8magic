using Microsoft.Extensions.DependencyInjection;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Internal.Startup;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Sources;
using ToSic.Cre8magic.Settings.Providers;
using ToSic.Cre8magic.Settings.Providers.Internal;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsProviderTests;

public class SettingsProviderTestWithAnalytics
{
    private const string GtmOfOriginal = "test-inherits";
    private const string GtmOfAdded = "test-added";

    /// <summary>
    /// Prepare a settings service and add a default value. 
    /// </summary>
    /// <returns></returns>
    private static (IMagicSettingsService settingsSvc, IMagicSettingsProvider SettingsProvider, MagicAnalyticsSettings DefaultSettings) PrepareSettings()
    {
        var di = SetupServices.Start().AddCre8magic().AddLogging().Finish();
        var settingsProvider = di.GetRequiredService<IMagicSettingsProvider>();
        var original = new MagicAnalyticsSettings
        {
            GtmId = GtmOfOriginal
        };
        settingsProvider.Add(new()
        {
            Analytics = original,
        });

        var settingsSvc = di.GetRequiredService<IMagicSettingsService>();
        return (settingsSvc, settingsProvider, original);
    }

    /// <summary>
    /// Use settings which have a name, and if the name is empty, null or "default" it should merge
    /// the default settings (as they don't have a name either).
    /// </summary>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("default")]
    [InlineData(" ", true)]
    [InlineData("name-not-found", true)]
    public void ValueOnlyNotNamed(string? currentName, bool expectNoMerge = false)
    {
        var (settingsSvc, _, original) = PrepareSettings();
        // Settings, without GtmId, so that it would receive it from original when merging
        var analytics = new MagicAnalyticsSettings { Name = currentName };
        var retrieved2 = new GetSettings(settingsSvc, null, analytics.Name)
            .GetBest(analytics, settingsSvc.Analytics);
        Assert.Equal(expectNoMerge ? analytics.GtmId : original.GtmId, retrieved2.Data.GtmId);
    }

    /// <summary>
    /// Add a named setting to the source and try to find it with the same or another name.
    /// Basically as soon as the name differs, it won't find the setting anymore.
    /// </summary>
    [Theory]
    [InlineData(GtmOfAdded, "admin", "admin")]
    [InlineData(null, "default", "admin")]
    [InlineData(null, "default", "whatever")]
    [InlineData(null, "admin", "whatever")]
    [InlineData(GtmOfOriginal, "admin", "default")]
    public void FromDictionaryFallbackDefault(string? expected, string provideName, string searchName)
    {
        var (settingsSvc, settingsProvider, _) = PrepareSettings();

        // Add a named setting which can be identified when found
        //settingsProvider.Analytics.Provide(provideName, new()
        //{
        //    GtmId = GtmOfAdded,
        //});
        settingsProvider.Add(new(provideName)
        {
            Analytics = new() { GtmId = GtmOfAdded }
        });

        var retrieved = new GetSettings(settingsSvc, null, searchName)
            .GetBest(new MagicAnalyticsSettings { Name = searchName }, settingsSvc.Analytics);

        Assert.Equal(expected, retrieved.Data.GtmId);
    }

    [Fact]
    public void BothInterfacesOnServiceProviderGiveSameObject()
    {
        var serviceProvider = SetupServices.Start().AddCre8magic().AddStandardLogging().Finish();
        var original = serviceProvider.GetRequiredService<MagicSettingsProvider>();
        var settingsProvider = serviceProvider.GetRequiredService<IMagicSettingsProvider>();

        Assert.Equal(original, settingsProvider);

        var allSettingsProviders = serviceProvider.GetRequiredService<IEnumerable<IMagicBooksSource>>();
        Assert.Contains(original, allSettingsProviders);
    }

}