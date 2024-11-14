using System;
using System.Collections.Generic;
using ToSic.Module.Cre8MagicTests.Client.Menus.Tests;
using ToSic.Module.Cre8MagicTests.Client.Tests.Breadcrumb;
using ToSic.Module.Cre8MagicTests.Client.Tests.PageService;
using ToSic.Theme.Cre8magicTests;

namespace ToSic.Module.Cre8MagicTests.Client.Tests;


/// <summary>
/// 
/// </summary>
/// <param name="Id">The ID which is also stored in the DB, so this should remain constant even if the controls change</param>
/// <param name="Name"></param>
/// <param name="Type"></param>
internal record TestDescription(string Id, string Name, Type Type);

internal class TestDatabase
{
    public static List<TestDescription> Tests =
    [
        // Page Service
        new("page-service-pages", "Page Service Pages", typeof(PageServicePages)),
        new("page-service-breadcrumb", "Page Service Breadcrumb", typeof(PageServiceBreadcrumb)),

        // Breadcrumb Controls
        new("breadcrumbs-basic", "Breadcrumb Basic", typeof(BreadcrumbsBasic)),

        // Menu Service
        new("menu-basic", "Menus Basic", typeof(MenuControlsBasic)),
        new("menu-functions", "Menus Functions", typeof(MenuControlsFunctions)),
    ];
}