using ToSic.Cre8magic.Breadcrumb;

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
    public void CloneWithNull() => VerifySameAsOriginal(Original().CloneWith(null));

    [Fact]
    public void CloneWithNull2() => VerifySameAsOriginal(Original().CloneWith(null, true));

    [Fact]
    public void CloneWithHalf() => VerifySameAsMix(Original().CloneWith(Half()));

    private static MagicBreadcrumbDesignSettings Original() =>
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
    private static MagicBreadcrumbDesignSettings Half() =>
        new()
        {
            HasChildren = new()
            {
                On = "half-on",
                Off = "half-off"
            }
        };

    private static void VerifyIsEmpty(MagicBreadcrumbDesignSettings x)
    {
        Assert.Null(x.HasChildren);
        Assert.Null(x.IsDisabled);
    }

    private static void VerifySameAsOriginal(MagicBreadcrumbDesignSettings y)
    {
        Assert.Equal("on", y.HasChildren.On);
        Assert.Equal("off", y.HasChildren.Off);
        Assert.Equal("on", y.IsDisabled.On);
        Assert.Equal("off", y.IsDisabled.Off);
    }

    private static void VerifySameAsHalf(MagicBreadcrumbDesignSettings y)
    {
        Assert.Equal("half-on", y.HasChildren.On);
        Assert.Equal("half-off", y.HasChildren.Off);
        Assert.Null(y.IsDisabled);
    }

    private static void VerifySameAsMix(MagicBreadcrumbDesignSettings y)
    {
        Assert.Equal("half-on", y.HasChildren.On);
        Assert.Equal("half-off", y.HasChildren.Off);
        Assert.Equal("on", y.IsDisabled.On);
        Assert.Equal("off", y.IsDisabled.Off);
    }
}