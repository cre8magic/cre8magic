using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsNamedTests;

internal record TestDataAbleToMerge : TestDataNoMerge, ICanClone<TestDataAbleToMerge>
{
    public TestDataAbleToMerge() { }

    public TestDataAbleToMerge(TestDataAbleToMerge? priority, TestDataAbleToMerge? fallback = default)
    {
        Name = priority?.Name ?? fallback?.Name!;
        Id = priority?.Id ?? fallback?.Id;
        Description = priority?.Description ?? fallback?.Description;
    }

    public TestDataAbleToMerge CloneUnder(TestDataAbleToMerge? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);
}