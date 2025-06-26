using System.Text.Json;
using ToSic.Cre8magic.ClientUnitTests.PagesData;
using ToSic.Cre8magic.ClientUnitTests.Utils;
using ToSic.Cre8magic.Internal.Logging;
using ToSic.Cre8magic.Menus.Internal.PagePicker;
using ToSic.Cre8magic.Pages;
using Xunit.Abstractions;
using PagesPickRuleParser = ToSic.Cre8magic.Menus.Internal.PagePicker.PagesPickRuleParser;

namespace ToSic.Cre8magic.ClientUnitTests.PagesPickerTests;

public class PagePickerTests(ITestOutputHelper output)
{
    private static PagesPickRuleParser Parser => new(new());
    private List<IMagicPage> GetPagesRaw(int id, string? rule) =>
        GetPagesRawWithRule(id, rule).List;

    private (List<IMagicPage> List, PagesPickRule NodeRule) GetPagesRawWithRule(int id, string? rule)
    {
        var pageFactory = PageTestData.PageFactoryForPage(id);
        var logRoot = new LogRoot();
        var picker = new PagePicker(pageFactory, pageFactory.Current, logRoot.GetLog("PageFactory"));

        rule ??= id.ToString();
        PagesPickRuleParser parser = new(logRoot);
        var nodeRule = parser.GetStartNodeRules(rule).Single();
        var list = picker.FindInitialPagesRawTac(nodeRule);
        logRoot.Dump(output);
        output.WriteLine("");
        return (list, nodeRule);
    }

    [Theory]
    [InlineData(PageTestData.HomeId)]
    public void Home(int homeId)
    {
        var list = GetPagesRaw(homeId, null);

        Assert.NotNull(list);
        Assert.Single(list);
        Assert.Equal(PageTestData.HomeId, list.Single().Id);
    }

    [Theory]
    [InlineData("/", PageTestData.ExpectedTopLevel)]
    [InlineData("//", PageTestData.ExpectedTopLevel)]
    [InlineData("//1", PageTestData.ExpectedTopLevel)]
    [InlineData("//2", PageTestData.ExpectedProducts)]
    [InlineData("//3", PageTestData.ExpectedElectronics)]
    [InlineData("//4", PageTestData.ExpectedPhones)]
    [InlineData("//-1", PageTestData.ExpectedTopLevel)]
    public void Levels(string rule, int count) =>
        Assert.Equal(count, GetPagesRaw(PageTestData.HomeId, rule).Count);

    [Theory]
    [InlineData(PageTestData.ProductsId, PageTestData.ExpectedProducts)]
    [InlineData(PageTestData.ElectronicsId, PageTestData.ExpectedElectronics)]
    [InlineData(PageTestData.PhonesId, PageTestData.ExpectedPhones)]
    public void ChildrenOf(int pageId, int expectedCount) =>
        Assert.Equal(expectedCount, GetPagesRaw(pageId, "./").Count);

    [Theory]
    [InlineData(PageTestData.ProductsId)]
    [InlineData(PageTestData.ElectronicsId)]
    [InlineData(PageTestData.PhonesId)]
    public void JustCurrent(int productsId)
    {
        var list = GetPagesRaw(productsId, ".");
        Assert.Single(list);
        Assert.Equal(productsId, list.Single().Id);
    }

    [Theory]
    [InlineData(PageTestData.ProductsId)]
    [InlineData(PageTestData.ElectronicsId)]
    [InlineData(PageTestData.PhonesId)]
    public void JustId(int productsId)
    {
        var list = GetPagesRaw(productsId, productsId + "");
        Assert.Single(list);
        Assert.Equal(productsId, list.Single().Id);
    }

    [Theory]
    [InlineData(PageTestData.ProductsId, "./", PageTestData.ExpectedProducts)]
    [InlineData(PageTestData.ProductsId, ".//", PageTestData.ExpectedTopLevel)]
    [InlineData(PageTestData.ProductsId, ".//1", PageTestData.ExpectedTopLevel)]
    [InlineData(PageTestData.ProductsId, ".//2", PageTestData.ExpectedProducts)]
    [InlineData(PageTestData.ProductsId, ".//3", 0)]
    [InlineData(PageTestData.ProductsId, "../", PageTestData.ExpectedTopLevel)]
    //[InlineData(PageTestData.ProductsId, ".//-1", PageTestData.ExpectedTopLevel)]
    public void ChildrenOfRootAncestor(int productId, string rule, int expectedCount)
    {
        var result = GetPagesRawWithRule(productId, rule);
        Assert.Equal(expectedCount, result.List.Count);
    }

    [Theory]
    [InlineData(PageTestData.ProductsId, ".", 1, "products: self")]
    [InlineData(PageTestData.ProductsId, "..", 0, "products: parent = root")]
    [InlineData(PageTestData.ProductsId, "../", PageTestData.ExpectedTopLevel, "products: parent/children = top-level")]
    [InlineData(PageTestData.ProductsId, ".//", PageTestData.ExpectedTopLevel, "products: top level, no -")]
    [InlineData(PageTestData.ProductsId, ".//-1", PageTestData.ExpectedTopLevel, "products: -1 = children-of-parent")]
    [InlineData(PageTestData.ProductsId, ".//-2", 0, "products: -2 = not found")]
    [InlineData(PageTestData.ElectronicsId, "../", PageTestData.ExpectedProducts, "electronics: parent-children = siblings")]
    [InlineData(PageTestData.ElectronicsId, ".//-1", PageTestData.ExpectedProducts, "electronics: parent")]
    [InlineData(PageTestData.ElectronicsId, ".//-2", PageTestData.ExpectedTopLevel)]
    [InlineData(PageTestData.PhonesId, "../", PageTestData.ExpectedElectronics)]
    [InlineData(PageTestData.PhonesId, ".//-1", PageTestData.ExpectedElectronics)]
    [InlineData(PageTestData.PhonesId, ".//-2", PageTestData.ExpectedProducts)]
    [InlineData(PageTestData.PhonesId, ".//-3", PageTestData.ExpectedTopLevel)]
    public void ChildrenOfAncestor(int productId, string rule, int expectedCount, string? note = default)
    {
        var (list, nodeRule) = GetPagesRawWithRule(productId, rule);
        output.WriteLine($"Rule: {nodeRule.Raw}; Mode: '{nodeRule.PickMode}' ({note})");
        output.WriteLine(JsonSerializer.Serialize(nodeRule));
        Assert.Equal(expectedCount, list.Count);
    }
}