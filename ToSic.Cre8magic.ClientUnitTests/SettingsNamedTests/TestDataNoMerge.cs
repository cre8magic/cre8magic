using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsNamedTests;

internal record TestDataNoMerge: ICanClone<TestDataNoMerge>
{

    public string Name { get; init; }

    public int? Id { get; init; }

    public string? Description { get; init; }

    /// <summary>
    /// FAKE can clone, to simulate scenario where cloning doesn't work.
    /// </summary>
    public TestDataNoMerge CloneUnder(TestDataNoMerge? priority, bool forceCopy = false)
    {
        return priority;
    }
}