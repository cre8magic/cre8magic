using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.ClientUnitTests.AnalyticsTests;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsTests;

public class MagicAnalyticsCreateClone
{
    [Fact]
    public void Empty() => VerifyIsEmpty(new());


    [Fact]
    public void Create() => VerifySameAsOriginal(Original());


    [Fact]
    public void ConstructorClone() => VerifySameAsOriginal(new(Original()));

    [Fact]
    public void ConstructorClone2() => VerifySameAsOriginal(new(null, Original()));

    [Fact]
    public void CloneWithNull() => VerifySameAsOriginal(Original().CloneUnderTac(null));

    [Fact]
    public void CloneWithEmpty() => VerifySameAsOriginal(Original().CloneUnderTac(new()));

    [Fact]
    public void CloneWithDifferent()
    {
        var x = Original().CloneUnderTac(Replacement());
        VerifySameAsReplacement(x, skipGtm: true);
        Assert.Equal(Original().GtmId, x.GtmId);
    }

    private static void VerifyIsEmpty(MagicAnalyticsSpell x)
    {
        Assert.Null(x.GtmId);
        Assert.Null(x.PageViewTrack);
        Assert.Null(x.PageViewTrackFirst);
        Assert.Null(x.PageViewJs);
        Assert.Null(x.PageViewEvent);
    }

    private static MagicAnalyticsSpell Original() =>
        new()
        {
            GtmId = "123",
            PageViewTrack = true,
            PageViewTrackFirst = false,
            PageViewJs = "", // empty
            PageViewEvent = null
        };

    private void VerifySameAsOriginal(MagicAnalyticsSpell y)
    {
        Assert.Equal("123", y.GtmId);
        Assert.True(y.PageViewTrack);
        Assert.False(y.PageViewTrackFirst);
        Assert.Equal("", y.PageViewJs);
        Assert.Null(y.PageViewEvent);
    }

    private static MagicAnalyticsSpell Replacement() =>
        new()
        {
            GtmId = null,
            PageViewTrack = true,
            PageViewTrackFirst = true,
            PageViewJs = "some-js", // empty
            PageViewEvent = "some text"
        };

    private static void VerifySameAsReplacement(MagicAnalyticsSpell y, bool skipGtm = false)
    {
        if (!skipGtm)
            Assert.Null(y.GtmId);
        Assert.True(y.PageViewTrack);
        Assert.True(y.PageViewTrackFirst);
        Assert.Equal("some-js", y.PageViewJs);
        Assert.Equal("some text", y.PageViewEvent);
    }
}