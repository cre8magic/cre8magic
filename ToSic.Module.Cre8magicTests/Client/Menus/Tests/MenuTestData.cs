using System.Collections.Generic;
using ToSic.Theme.Cre8magicTests.Client;

namespace ToSic.Module.Cre8MagicTests.Client.Menus.Tests;

internal class MenuTestData
{
    public static Dictionary<string, List<int>> dicTopLevelThreePage = new Dictionary<string, List<int>>()
    {
        { TestParameters.EnvTonci, [97, 98, 99] },
        {
            TestParameters.EnvIJungleboy, [
                29, // Top level page (first with subitems)
                33, // Third under Top Level Page
                41, // Main Test Page with loads of pages
            ]
        }
    };

    public static Dictionary<string, List<int>> dicRestrictPages = new Dictionary<string, List<int>>()
    {
        { TestParameters.EnvTonci, MenuTestData.dicTopLevelThreePage[TestParameters.EnvTonci] },
        {
            TestParameters.EnvIJungleboy, [
                ..MenuTestData.dicTopLevelThreePage[TestParameters.EnvIJungleboy],
                36, // "Page with Content" under "TopLevel"
            ]
        }
    };
}