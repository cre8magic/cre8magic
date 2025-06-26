﻿using ToSic.Cre8magic.ClientUnitTests.Utils;
using ToSic.Cre8magic.Internal.Logging;
using ToSic.Cre8magic.Menus.Internal.PagePicker;
using Xunit.Abstractions;
using PagesPickRuleParser = ToSic.Cre8magic.Menus.Internal.PagePicker.PagesPickRuleParser;

namespace ToSic.Cre8magic.ClientUnitTests.PagesPickerTests;

public class MenuNodeRuleParserTests(ITestOutputHelper output)
{
    private void AssertRule(PagesPickRule expected)
    {
        var logRoot = new LogRoot();
        PagesPickRuleParser parser = new(logRoot);
        var rules = parser.GetStartNodeRules(expected.Raw);
        logRoot.Dump(output);
        Assert.Single(rules);
        Assert.Equal(expected, rules[0]);
    }

    [Theory]
    [InlineData("/")]
    [InlineData("//")]
    [InlineData("//1")]
    [InlineData("//-1")]
    [InlineData("//-2")]
    [InlineData("0/")]
    [InlineData("0//")]
    public void RuleRoot(string raw) => AssertRule(new()
    {
        Raw = raw,
        Id = 0,
        Depth = 1,
        Force = false,
        Level = 1,
        ShowChildren = false,
        PickMode = PickMode.Root,
    });


    [Theory]
    [InlineData("/+", 2)]
    [InlineData("//+", 2)]
    [InlineData("/++", 3)]
    [InlineData("//++", 3)]
    public void RuleRootPlus1(string raw, int depth) => AssertRule(new()
    {
        Raw = raw,
        Id = 0,
        Depth = depth,
        Force = false,
        Level = 1,
        ShowChildren = false,
        PickMode = PickMode.Root,
    });

    [Theory]
    [InlineData("//2", 2)]
    [InlineData("//3", 3)]
    [InlineData("//-1", 1)]
    public void RuleRootLevel2(string raw, int level) => AssertRule(new()
    {
        Raw = raw,
        Id = 0,
        Force = false,
        Level = level,
        ShowChildren = false,
        PickMode = PickMode.Root,
    });

    [Theory]
    [InlineData(".")]
    public void RuleCurrent(string raw) => AssertRule(new()
    {
        Raw = raw,
        Id = 0,
        Force = false,
        Level = 0,
        ShowChildren = false,
        PickMode = PickMode.Current,
    });

    [Theory]
    [InlineData(".+")]
    public void RuleCurrentWithChildren(string raw) => AssertRule(new()
    {
        Raw = raw,
        Id = 0,
        Depth = 2,
        Force = false,
        Level = 0,
        ShowChildren = false,
        PickMode = PickMode.Current,
    });

    [Theory]
    [InlineData("./")]
    public void RuleChildren(string raw) => AssertRule(new()
    {
        Raw = raw,
        Id = 0,
        Force = false,
        Level = 0,
        ShowChildren = true,
        PickMode = PickMode.Current,
    });

    [Theory]
    [InlineData(".//")]
    public void RuleCurrentFromRoot(string raw) => AssertRule(new()
    {
        Raw = raw,
        Id = 0,
        Force = false,
        Level = 1,
        ShowChildren = false,
        PickMode = PickMode.Current,
    });

    [Theory]
    [InlineData(".//1", 1)]
    [InlineData(".//2", 2)]
    [InlineData(".//3", 3)]
    public void RuleCurrentFromRootLevel1(string raw, int level) => AssertRule(new()
    {
        Raw = raw,
        Id = 0,
        Force = false,
        Level = level,
        ShowChildren = false,
        PickMode = PickMode.Current,
    });

    [Theory]
    [InlineData("42")]
    public void RulePageId(string raw) => AssertRule(new()
    {
        Raw = raw,
        Id = 42,
        Force = false,
        Level = 1,
        ShowChildren = false,
        PickMode = PickMode.PageId,
    });

    [Theory]
    [InlineData("42!")]
    public void RulePageIdForce(string raw) => AssertRule(new()
    {
        Raw = raw,
        Id = 42,
        Force = true,
        Level = 1,
        ShowChildren = false,
        PickMode = PickMode.PageId,
    });


    [Theory]
    [InlineData("42/")]
    public void RulePageIdChildren(string raw) => AssertRule(new()
    {
        Raw = raw,
        Id = 42,
        Force = false,
        Level = 1,
        ShowChildren = true,
        PickMode = PickMode.PageId,
    });

    [Theory]
    [InlineData("42!/")]
    public void RulePageIdForceChildren(string raw) => AssertRule(new()
    {
        Raw = raw,
        Id = 42,
        Force = true,
        Level = 1,
        ShowChildren = true,
        PickMode = PickMode.PageId,
    });

    [Theory]
    [InlineData("..")]
    public void RuleParent(string raw) => AssertRule(new()
    {
        Raw = raw,
        Id = 0,
        Force = false,
        Level = -1,
        ShowChildren = false,
        PickMode = PickMode.Current,
    });

    [Theory]
    [InlineData("../")]
    [InlineData(".//-1")]
    [InlineData(".//-2", -2)]
    [InlineData(".//-3", -3)]
    public void RuleSiblings(string raw, int level = -1) => AssertRule(new()
    {
        Raw = raw,
        Id = 0,
        Force = false,
        Level = level,
        ShowChildren = true,
        PickMode = PickMode.Current,
    });

}