using System;
using System.Collections.Generic;
using ToSic.Module.Cre8MagicTests.Client.Tests.Breadcrumb;

namespace ToSic.Module.Cre8MagicTests.Client.Tests;

internal record TestDescription(string Id, string Name, Type Type);

internal class TestDatabase
{
    public static List<TestDescription> Tests =
    [
        new TestDescription("breadcrumb-basic-1", "Breadcrumb Test 1", typeof(BreadcrumbTest1)),
        new TestDescription("breadcrumb-basic-2", "Breadcrumb Test 2", typeof(BreadcrumbTest2)),
    ];
}