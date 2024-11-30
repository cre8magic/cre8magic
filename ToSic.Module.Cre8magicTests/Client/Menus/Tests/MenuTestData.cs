using System.Collections.Generic;
using ToSic.Theme.Cre8magicTests.Client;

namespace ToSic.Module.Cre8MagicTests.Client.Menus.Tests;

internal class MenuTestData
{
    public static Dictionary<string, List<int>> DicTopLevelThreePage = new()
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

    public static Dictionary<string, List<int>> DicRestrictPages = new()
    {
        { TestParameters.EnvTonci, MenuTestData.DicTopLevelThreePage[TestParameters.EnvTonci] },
        {
            TestParameters.EnvIJungleboy, [
                ..MenuTestData.DicTopLevelThreePage[TestParameters.EnvIJungleboy],
                36, // "Page with Content" under "TopLevel"
            ]
        }
    };
}