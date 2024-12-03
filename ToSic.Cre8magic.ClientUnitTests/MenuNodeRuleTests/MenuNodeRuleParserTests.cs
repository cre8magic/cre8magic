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

    [Theory]
    [InlineData("/")]
    [InlineData("//")]
    [InlineData("//1")]
    public void RuleRoot(string raw) => AssertRule(new()
    {
        Raw = raw,
        Id = 0,
        Depth = 1,
        Force = false,
        Level = 1,
        ShowChildren = false,
        ModeInfo = StartMode.Root,
    });


    [Theory]
    [InlineData("/+", 2)]
    [InlineData("/++", 3)]
    public void RuleRootPlus1(string raw, int depth) => AssertRule(new()
    {
        Raw = raw,
        Id = 0,
        Depth = depth,
        Force = false,
        Level = 1,
        ShowChildren = false,
        ModeInfo = StartMode.Root,
    });

    [Fact]
    public void RuleRootLevel2() => AssertRule(new()
    {
        Raw = "//2",
        Id = 0,
        Force = false,
        Level = 2,
        ShowChildren = false,
        ModeInfo = StartMode.Root,
    });

    [Fact]
    public void RuleCurrent() => AssertRule(new()
    {
        Raw = ".",
        Id = 0,
        Force = false,
        Level = 0,
        ShowChildren = false,
        ModeInfo = StartMode.Current,
    });

    [Fact]
    public void RuleChildren() => AssertRule(new()
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
    public void RulePageId() => AssertRule(new()
    {
        Raw = "42",
        Id = 42,
        Force = false,
        Level = 1,
        ShowChildren = false,
        ModeInfo = StartMode.PageId,
    });

    [Fact]
    public void RulePageIdForce() => AssertRule(new()
    {
        Raw = "42!",
        Id = 42,
        Force = true,
        Level = 1,
        ShowChildren = false,
        ModeInfo = StartMode.PageId,
    });


    [Fact]
    public void RulePageIdChildren() => AssertRule(new()
    {
        Raw = "42/",
        Id = 42,
        Force = false,
        Level = 1,
        ShowChildren = true,
        ModeInfo = StartMode.PageId,
    });

    [Fact]
    public void RuleParent() => AssertRule(new()
    {
        Raw = "..",
        Id = 0,
        Force = false,
        Level = -1,
        ShowChildren = false,
        ModeInfo = StartMode.Current,
    });

    [Fact]
    public void RuleSiblings() => AssertRule(new()
    {
        Raw = "../",
        Id = 0,
        Force = false,
        Level = -1,
        ShowChildren = true,
        ModeInfo = StartMode.Current,
    });

}