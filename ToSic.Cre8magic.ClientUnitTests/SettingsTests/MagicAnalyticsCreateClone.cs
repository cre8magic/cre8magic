using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.ClientUnitTests.AnalyticsTests;
using static Xunit.Assert;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsTests;

public class MagicAnalyticsCreateClone
{
    [Fact]
    public void Empty() => VerifyIsEmpty(new());

    [Fact]
    public void EmptyFinalized()
    {
        var s = new MagicAnalyticsSettings().GetStable();
        Equal(MagicAnalyticsSettings.Stabilized.DefaultGtmId, s.GtmId);
        Equal(MagicAnalyticsSettings.Stabilized.DefaultPageViewEvent, s.PageViewEvent);
        Equal(MagicAnalyticsSettings.Stabilized.DefaultPageViewJs, s.PageViewJs);
        Equal(MagicAnalyticsSettings.Stabilized.DefaultPageViewTrack, s.PageViewTrack);
        Equal(MagicAnalyticsSettings.Stabilized.DefaultPageViewTrackFirst, s.PageViewTrackFirst);
    }

    [Fact]
    public void Create() => VerifySameAsOriginal(Original());

    [Fact]
    public void CreateFinalMatchesOriginal() => VerifyFinalSimilarToOriginal(Original().GetStable());

    [Fact]
    public void ConstructorClone() => VerifySameAsOriginal(new MagicAnalyticsSettings().CloneUnderTac(Original()));

    //[Fact]
    //public void ConstructorClone2() => VerifySameAsOriginal(new(null, Original()));

    [Fact]
    public void CloneWithNull() => VerifySameAsOriginal(Original().CloneUnderTac(null));

    [Fact]
    public void CloneWithEmpty() => VerifySameAsOriginal(Original().CloneUnderTac(new()));

    [Fact]
    public void CloneWithDifferent()
    {
        var x = Original().CloneUnderTac(Replacement());
        VerifySameAsReplacement(x, skipGtm: true);
        Equal(Original().GtmId, x.GtmId);
    }

    private static void VerifyIsEmpty(MagicAnalyticsSettings x)
    {
        Null(x.GtmId);
        Null(x.PageViewTrack);
        Null(x.PageViewTrackFirst);
        Null(x.PageViewJs);
        Null(x.PageViewEvent);
    }

    private static MagicAnalyticsSettings Original() =>
        new()
        {
            GtmId = "123",
            PageViewTrack = true,
            PageViewTrackFirst = false,
            PageViewJs = "", // empty
            PageViewEvent = null
        };

    private void VerifySameAsOriginal(MagicAnalyticsSettings y)
    {
        Equal("123", y.GtmId);
        True(y.PageViewTrack);
        False(y.PageViewTrackFirst);
        Equal("", y.PageViewJs);
        Null(y.PageViewEvent);
    }
    private void VerifyFinalSimilarToOriginal(MagicAnalyticsSettings.Stabilized y)
    {
        Equal("123", y.GtmId);
        True(y.PageViewTrack);
        False(y.PageViewTrackFirst);
        Equal("", y.PageViewJs);
        //Null(y.PageViewEvent);
        Equal(MagicAnalyticsSettings.Stabilized.DefaultPageViewEvent, y.PageViewEvent);
    }

    private static MagicAnalyticsSettings Replacement() =>
        new()
        {
            GtmId = null,
            PageViewTrack = true,
            PageViewTrackFirst = true,
            PageViewJs = "some-js", // empty
            PageViewEvent = "some text"
        };

    private static void VerifySameAsReplacement(MagicAnalyticsSettings y, bool skipGtm = false)
    {
        if (!skipGtm)
            Null(y.GtmId);
        True(y.PageViewTrack);
        True(y.PageViewTrackFirst);
        Equal("some-js", y.PageViewJs);
        Equal("some text", y.PageViewEvent);
    }
}