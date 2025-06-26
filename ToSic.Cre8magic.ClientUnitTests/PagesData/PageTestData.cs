using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Pages.Internal;

namespace ToSic.Cre8magic.ClientUnitTests.PagesData;

internal class PageTestData
{
    public static Page Page(int id, string name, bool isNavigation = true, int level = 0, int parent = 0, string? path = default) =>
        new()
        {
            PageId = id,
            Name = name,
            Path = path ?? name.ToLowerInvariant().Replace(' ', '-'),
            IsNavigation = isNavigation,
            Level = level,
            ParentId = parent
        };
    public static Page Child(Page parent, int id, string name, bool isNavigation = true) =>
        new()
        {
            PageId = id,
            Name = name,
            Path = name.ToLowerInvariant().Replace(' ', '-'),
            IsNavigation = isNavigation,
            Level = parent.Level + 1,
            ParentId = parent.PageId
        };
    public static PageState PageState(Page current, IEnumerable<Page> pages) =>
        new()
        {
            Site = new() { Pages = pages.ToList() },
            Page = current,
        };

    public const int HomeId = 1;
    public const int ProductsId = 100;
    public const int ElectronicsId = ProductsId * 10;
    public const int PhonesId = ElectronicsId * 10;

    // Important: these numbers must all be different, to prevent accidental use of the wrong one
    public const int ExpectedTopLevel = 5;
    public const int ExpectedProducts = 4;
    public const int ExpectedElectronics = 6;
    public const int ExpectedPhones = 8;

    public static List<Page> BasicTree()
    {

        var products = Page(ProductsId, "Products");
        var electronics = Child(products, ElectronicsId, "Electronics");
        var phones = Child(electronics, PhonesId, "Smartphones");

        var id = 10;
        var tree = new List<Page>
        {
            Page(HomeId, "Home", path: ""),
            Page(id++, "About"),
            Page(id++, "Contact"),
            Page(id ++, "Services"),

            products,
            Child(products, id++, "Kitchenware"),
            electronics,
            Child(products, id++, "Clothing"),
            Child(products, id++, "Furniture"),

            Child(electronics, id++, "Appliances"),
            Child(electronics, id++, "Computers"),
            phones,
            Child(electronics, id++, "Tablets"),
            Child(electronics, id++, "Mice"),
            Child(electronics, id++, "Keyboards"),

            Child(phones, id++, "Samsung"),
            Child(phones, id++, "Apple"),
            Child(phones, id++, "Huawei"),
            Child(phones, id++, "Xiaomi"),
            Child(phones, id++, "Honor"),
            Child(phones, id++, "OnePlus"),
            Child(phones, id++, "Sony"),
            Child(phones, id++, "Nokia"),

        };

        return tree;
    }

    public static MagicPageFactory PageFactoryForPage(int currentId)
    {
        var all = BasicTree();
        var current = all.First(p => p.PageId == currentId);
        var pageState = PageState(current, all);

        var firstFactory = new MagicPageFactory(pageState, ignorePermissions: true);

        // Create a page factory which has a "custom" list of pages, so that it won't do any security checks
        var pageFactory = new MagicPageFactory(pageState, firstFactory.PagesAll(), ignorePermissions: true);
        return firstFactory;
    }

}