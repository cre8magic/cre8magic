using ToSic.Cre8magic.Spells.Internal;
using ToSic.Cre8magic.Tailors;

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
    public void CloneWithNull() => VerifySameAsOriginal(((ICanClone<MagicBlueprintPart>)Original()).CloneUnder(null));

    [Fact]
    public void CloneWithNull2() => VerifySameAsOriginal(((ICanClone<MagicBlueprintPart>)Original()).CloneUnder(null, true));

    [Fact]
    public void CloneWithHalf() => VerifySameAsMix(((ICanClone<MagicBlueprintPart>)Original()).CloneUnder(Half()));

    private static MagicBlueprintPart Original() =>
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
    private static MagicBlueprintPart Half() =>
        new()
        {
            HasChildren = new()
            {
                On = "half-on",
                Off = "half-off"
            }
        };

    private static void VerifyIsEmpty(MagicBlueprintPart x)
    {
        Assert.Null(x.HasChildren);
        Assert.Null(x.IsDisabled);
    }

    private static void VerifySameAsOriginal(MagicBlueprintPart y)
    {
        Assert.Equal("on", y.HasChildren!.On);
        Assert.Equal("off", y.HasChildren.Off);
        Assert.Equal("on", y.IsDisabled!.On);
        Assert.Equal("off", y.IsDisabled.Off);
    }

    private static void VerifySameAsHalf(MagicBlueprintPart y)
    {
        Assert.Equal("half-on", y.HasChildren!.On);
        Assert.Equal("half-off", y.HasChildren.Off);
        Assert.Null(y.IsDisabled);
    }

    private static void VerifySameAsMix(MagicBlueprintPart y)
    {
        Assert.Equal("half-on", y.HasChildren!.On);
        Assert.Equal("half-off", y.HasChildren.Off);
        Assert.Equal("on", y.IsDisabled!.On);
        Assert.Equal("off", y.IsDisabled.Off);
    }
}