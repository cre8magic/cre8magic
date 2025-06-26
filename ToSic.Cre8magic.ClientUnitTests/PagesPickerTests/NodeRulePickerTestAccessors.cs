using ToSic.Cre8magic.Menus.Internal.PagePicker;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.ClientUnitTests.PagesPickerTests;

internal static class NodeRulePickerTestAccessors
{
    public static List<IMagicPage> FindInitialPagesRawTac(this PagePicker picker, PagesPickRule pickRule) =>
        picker.FindInitialPagesRaw(pickRule);

    public static List<IMagicPage> FindStartPagesOfOneRuleTac(this PagePicker picker, PagesPickRule pickRule)
        => picker.FindStartPagesOfOneRule(pickRule);
}