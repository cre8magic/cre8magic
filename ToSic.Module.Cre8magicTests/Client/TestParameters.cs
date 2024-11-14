using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToSic.Theme.Cre8magicTests.Client;

/// <summary>
/// Class containing switches, values etc. for the tests to run.
///
/// A special property is that certain IDs for many tests
/// are basically hard-wired and differ from system to system.
/// </summary>
internal class TestParameters
{
    public const string EnvTonci = "Tonci";
    public const string EnvIJungleboy = "iJungleboy";

    public const string EnvCurrent = EnvIJungleboy;

    /// <summary>
    /// TODO: MAYBE AUTO DETECT THE ENVIRONMENT - NOT IMPLEMENTED YET
    /// </summary>
    public static Dictionary<string, string> Environments = new()
    {
        {EnvTonci, "https://tonci.tosic.com"},
        {EnvIJungleboy, "https://ijungleboy.tosic.com"}
    };

    /// <summary>
    /// A special page which has sub-pages, for testing various menus
    /// </summary>
    public static Dictionary<string, string> TopLevelWithSubpagesIdDic = new()
    {
        { EnvTonci, "97" },
        { EnvIJungleboy, "29" }
    };

    public static Dictionary<string, int> PageWithDeepBreadcrumbIdDic = new()
    {
        { EnvTonci, 308 },
        { EnvIJungleboy, 35 } // Sub-Under-Third
    };
}