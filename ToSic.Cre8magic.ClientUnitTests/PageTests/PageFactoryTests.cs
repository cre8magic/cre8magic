using ToSic.Cre8magic.ClientUnitTests.PagesData;
using ToSic.Cre8magic.Pages.Internal;

namespace ToSic.Cre8magic.ClientUnitTests.PageTests;

public class PageFactoryTests
{
    [Fact]
    public void Current()
    {
        var page = PageFactoryMinimal().Current;
        Assert.NotNull(page);
        Assert.Equal(42, page.Id);
        Assert.Equal("Current", page.Name);
        Assert.Equal("current", page.Path);
    }

    [Fact]
    public void All1()
    {
        var pages = PageFactoryMinimal().PagesAll();
        Assert.Single(pages);
        var page = pages.Single();
        Assert.NotNull(page);
        Assert.Equal(42, page.Id);
    }

    private static MagicPageFactory PageFactoryMinimal()
    {
        var currentPage = PageTestData.Page(42, "Current");
        var pageState = PageTestData.PageState(currentPage, [currentPage]);
        var pageFactory = new MagicPageFactory(pageState);
        return pageFactory;
    }
}