using ToSic.Cre8magic.Menus.Internal.Nodes;

namespace ToSic.Cre8magic.ClientUnitTests.MenuNodeRuleTests;

public class MenuNodeRuleParserTests
{
    private static NodeRuleParser Parser => new(new());

    private void AssertRule(StartNodeRule expected)
    {
        var rules = Parser.GetStartNodeRules(expected.Raw);
        Assert.Single(rules);
        Assert.Equal(expected, rules[0]);
    }

    [Fact]
    public void RuleRoot() =>
        AssertRule(new()
        {
            Id = 0,
            Force = false,
            Level = 1,
            ShowChildren = false,
            ModeInfo = StartMode.Root,
            Raw = "/",
        });

    [Fact]
    public void RuleRootDoubleSlash() =>
        AssertRule(new()
        {
            Id = 0,
            Force = false,
            Level = 1,
            ShowChildren = false,
            ModeInfo = StartMode.Root,
            Raw = "//",
        });

    [Fact]
    public void RuleRootLevel1() =>
        AssertRule(new()
        {
            Id = 0,
            Force = false,
            Level = 1,
            ShowChildren = false,
            ModeInfo = StartMode.Root,
            Raw = "//1",
        });

    [Fact]
    public void RuleRootLevel2() =>
        AssertRule(new()
        {
            Id = 0,
            Force = false,
            Level = 2,
            ShowChildren = false,
            ModeInfo = StartMode.Root,
            Raw = "//2",
        });

    [Fact]
    public void RuleCurrent() =>
        AssertRule(new()
        {
            Id = 0,
            Force = false,
            Level = 0,
            ShowChildren = false,
            ModeInfo = StartMode.Current,
            Raw = ".",
        });

    [Fact]
    public void RuleChildren() =>
        AssertRule(new()
        {
            Raw = "./",
            Id = 0,
            Force = false,
            Level = 0,
            ShowChildren = true,
            ModeInfo = StartMode.Current,
        });

    [Fact]
    public void RuleCurrentFromRoot() => AssertRule(new()
    {
        Raw = ".//",
        Id = 0,
        Force = false,
        Level = 1,
        ShowChildren = false,
        ModeInfo = StartMode.Current,
    });

    [Fact]
    public void RuleCurrentFromRootLevel1() => AssertRule(new()
    {
        Raw = ".//1",
        Id = 0,
        Force = false,
        Level = 1,
        ShowChildren = false,
        ModeInfo = StartMode.Current,
    });
    [Fact]
    public void RuleCurrentFromRootLevel2() => AssertRule(new()
    {
        Raw = ".//2",
        Id = 0,
        Force = false,
        Level = 2,
        ShowChildren = false,
        ModeInfo = StartMode.Current,
    });

    [Fact]
    public void RulePageId() =>
        AssertRule(new()
        {
            Raw = "42",
            Id = 42,
            Force = false,
            Level = 1,
            ShowChildren = false,
            ModeInfo = StartMode.PageId,
        });

    [Fact]
    public void RulePageIdForce() =>
        AssertRule(new()
        {
            Raw = "42!",
            Id = 42,
            Force = true,
            Level = 1,
            ShowChildren = false,
            ModeInfo = StartMode.PageId,
        });


    [Fact]
    public void RulePageIdChildren() =>
        AssertRule(new()
        {
            Raw = "42/",
            Id = 42,
            Force = false,
            Level = 1,
            ShowChildren = true,
            ModeInfo = StartMode.PageId,
        });

    [Fact]
    public void RuleParent() =>
        AssertRule(new()
        {
            Raw = "..",
            Id = 0,
            Force = false,
            Level = -1,
            ShowChildren = false,
            ModeInfo = StartMode.Current,
        });

    [Fact]
    public void RuleSiblings() =>
        AssertRule(new()
        {
            Raw = "../",
            Id = 0,
            Force = false,
            Level = -1,
            ShowChildren = true,
            ModeInfo = StartMode.Current,
        });

}