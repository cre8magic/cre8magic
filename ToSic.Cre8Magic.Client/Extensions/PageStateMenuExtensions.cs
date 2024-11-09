using Oqtane.UI;

namespace ToSic.Cre8magic.Client;

public static class PageStateMenuExtensions
{

    internal static bool CurrentPageIsHome(this PageState pageState) => pageState?.Page.Path == "";

}