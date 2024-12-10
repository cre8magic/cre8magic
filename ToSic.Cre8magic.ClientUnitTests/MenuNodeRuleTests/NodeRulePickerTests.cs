using ToSic.Cre8magic.ClientUnitTests.PagesData;
using ToSic.Cre8magic.Menus.Internal.Nodes;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.ClientUnitTests.MenuNodeRuleTests;

public class NodeRulePickerTests
{
    private static NodeRuleParser Parser => new(new());
    private static List<IMagicPage> GetPagesRaw(int id, string? rule) =>
        GetPagesRawWithRule(id, rule).List;

    private static (List<IMagicPage> List, StartNodeRule NodeRule) GetPagesRawWithRule(int id, string? rule)
    {
        var pageFactory = PageTestData.PageFactoryForPage(id);
        var picker = new NodeRulePicker(pageFactory, pageFactory.Current, new LogRoot().GetLog("dummy"));

        rule ??= id.ToString();
        var nodeRule = Parser.GetStartNodeRules(rule).Single();
        var list = picker.FindInitialPagesRawTac(nodeRule);
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
    public void ChildrenOfAncestor(int productId, string rule, int expectedCount)
    {
        var result = GetPagesRawWithRule(productId, rule);
        Assert.Equal(expectedCount, result.List.Count);
    }
}