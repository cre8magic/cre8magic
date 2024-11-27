using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsTests;

public class MagicBreadcrumbDesignSettingsCreateClone
{
    [Fact]
    public void Empty() => VerifyIsEmpty(new());

    [Fact]
    public void CreateOriginal() => VerifySameAsOriginal(Original());

    [Fact]
    public void CreateHalf() => VerifySameAsHalf(Half());

    [Fact]
    public void ConstructorClone() => VerifySameAsOriginal(new(Original()));

    [Fact]
    public void ConstructorClone2() => VerifySameAsOriginal(new(null, Original()));

    [Fact]
    public void CloneWithNull() => VerifySameAsOriginal(((ICanClone<MagicBreadcrumbDesignSettingsPart>)Original()).CloneUnder(null));

    [Fact]
    public void CloneWithNull2() => VerifySameAsOriginal(((ICanClone<MagicBreadcrumbDesignSettingsPart>)Original()).CloneUnder(null, true));

    [Fact]
    public void CloneWithHalf() => VerifySameAsMix(((ICanClone<MagicBreadcrumbDesignSettingsPart>)Original()).CloneUnder(Half()));

    private static MagicBreadcrumbDesignSettingsPart Original() =>
        new()
        {
            HasChildren = new()
            {
                On = "on",
                Off = "off"
            },
            IsDisabled = new()
            {
                On = "on",
                Off = "off"
            }
        };
    private static MagicBreadcrumbDesignSettingsPart Half() =>
        new()
        {
            HasChildren = new()
            {
                On = "half-on",
                Off = "half-off"
            }
        };

    private static void VerifyIsEmpty(MagicBreadcrumbDesignSettingsPart x)
    {
        Assert.Null(x.HasChildren);
        Assert.Null(x.IsDisabled);
    }

    private static void VerifySameAsOriginal(MagicBreadcrumbDesignSettingsPart y)
    {
        Assert.Equal("on", y.HasChildren.On);
        Assert.Equal("off", y.HasChildren.Off);
        Assert.Equal("on", y.IsDisabled.On);
        Assert.Equal("off", y.IsDisabled.Off);
    }

    private static void VerifySameAsHalf(MagicBreadcrumbDesignSettingsPart y)
    {
        Assert.Equal("half-on", y.HasChildren.On);
        Assert.Equal("half-off", y.HasChildren.Off);
        Assert.Null(y.IsDisabled);
    }

    private static void VerifySameAsMix(MagicBreadcrumbDesignSettingsPart y)
    {
        Assert.Equal("half-on", y.HasChildren.On);
        Assert.Equal("half-off", y.HasChildren.Off);
        Assert.Equal("on", y.IsDisabled.On);
        Assert.Equal("off", y.IsDisabled.Off);
    }
}