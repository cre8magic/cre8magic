using ToSic.Cre8magic.Menus.Internal.Nodes;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.ClientUnitTests.MenuNodeRuleTests;

internal static class NodeRulePickerTestAccessors
{
    public static List<IMagicPage> FindInitialPagesRawTac(this NodeRulePicker picker, StartNodeRule rule) =>
        picker.FindInitialPagesRaw(rule);

    public static List<IMagicPage> FindStartPagesOfOneRuleTac(this NodeRulePicker picker, StartNodeRule rule)
        => picker.FindStartPagesOfOneRule(rule);
}