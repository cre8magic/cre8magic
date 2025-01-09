using Microsoft.Extensions.DependencyInjection;
using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Internal.Startup;
using ToSic.Cre8magic.Spells;
using ToSic.Cre8magic.Spells.Internal;
using ToSic.Cre8magic.Spells.Internal.Providers;
using ToSic.Cre8magic.Spells.Internal.Sources;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsProviderTests;

public class SettingsProviderTestWithAnalytics
{
    private const string GtmOfOriginal = "test-inherits";
    private const string GtmOfAdded = "test-added";

    /// <summary>
    /// Prepare a settings service and add a default value. 
    /// </summary>
    /// <returns></returns>
    private static (IMagicSpellsService settingsSvc, IMagicSpellsProvider SettingsProvider, MagicAnalyticsSpell DefaultSettings) PrepareSettings()
    {
        var di = SetupServices.Start().AddCre8magic().AddLogging().Finish();
        var settingsProvider = di.GetRequiredService<IMagicSpellsProvider>();
        var settingsSvc = di.GetRequiredService<IMagicSpellsService>();
        var original = new MagicAnalyticsSpell
        {
            GtmId = GtmOfOriginal
        };
        settingsProvider.Analytics.SetDefault(original);
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
        var analytics = new MagicAnalyticsSpell { SettingsName = currentName };
        var retrieved2 = settingsSvc.GetBestSpell(
            null,
            analytics,
            settingsSvc.Analytics,
            "Container"
        );
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
        settingsProvider.Analytics.Provide(provideName, new()
        {
            GtmId = GtmOfAdded,
        });

        var retrieved = settingsSvc.GetBestSpell(
            null,
            new MagicAnalyticsSpell { SettingsName = searchName },
            settingsSvc.Analytics,
            "Container"
        );

        Assert.Equal(expected, retrieved.Data.GtmId);
    }

    [Fact]
    public void BothInterfacesOnServiceProviderGiveSameObject()
    {
        var serviceProvider = SetupServices.Start().AddCre8magic().AddStandardLogging().Finish();
        var original = serviceProvider.GetRequiredService<MagicSpellsProvider>();
        var settingsProvider = serviceProvider.GetRequiredService<IMagicSpellsProvider>();

        Assert.Equal(original, settingsProvider);

        var allSettingsProviders = serviceProvider.GetRequiredService<IEnumerable<IMagicSpellsBooksSource>>();
        Assert.Contains(original, allSettingsProviders);
    }

}