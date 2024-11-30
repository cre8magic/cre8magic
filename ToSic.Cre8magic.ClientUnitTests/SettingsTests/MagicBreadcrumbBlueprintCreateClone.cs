using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsTests;

public class MagicBreadcrumbBlueprintCreateClone
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
    public void CloneWithNull() => VerifySameAsOriginal(((ICanClone<MagicBreadcrumbBlueprintPart>)Original()).CloneUnder(null));

    [Fact]
    public void CloneWithNull2() => VerifySameAsOriginal(((ICanClone<MagicBreadcrumbBlueprintPart>)Original()).CloneUnder(null, true));

    [Fact]
    public void CloneWithHalf() => VerifySameAsMix(((ICanClone<MagicBreadcrumbBlueprintPart>)Original()).CloneUnder(Half()));

    private static MagicBreadcrumbBlueprintPart Original() =>
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
    private static MagicBreadcrumbBlueprintPart Half() =>
        new()
        {
            HasChildren = new()
            {
                On = "half-on",
                Off = "half-off"
            }
        };

    private static void VerifyIsEmpty(MagicBreadcrumbBlueprintPart x)
    {
        Assert.Null(x.HasChildren);
        Assert.Null(x.IsDisabled);
    }

    private static void VerifySameAsOriginal(MagicBreadcrumbBlueprintPart y)
    {
        Assert.Equal("on", y.HasChildren.On);
        Assert.Equal("off", y.HasChildren.Off);
        Assert.Equal("on", y.IsDisabled.On);
        Assert.Equal("off", y.IsDisabled.Off);
    }

    private static void VerifySameAsHalf(MagicBreadcrumbBlueprintPart y)
    {
        Assert.Equal("half-on", y.HasChildren.On);
        Assert.Equal("half-off", y.HasChildren.Off);
        Assert.Null(y.IsDisabled);
    }

    private static void VerifySameAsMix(MagicBreadcrumbBlueprintPart y)
    {
        Assert.Equal("half-on", y.HasChildren.On);
        Assert.Equal("half-off", y.HasChildren.Off);
        Assert.Equal("on", y.IsDisabled.On);
        Assert.Equal("off", y.IsDisabled.Off);
    }
}