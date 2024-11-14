using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.ClientUnitTests.SettingsNamedTests;

internal record DataForTest
{

    public string Name { get; init; }

    public int? Id { get; init; }

    public string? Description { get; init; }
}

internal record DataForTestCanClone : DataForTest, ICanClone<DataForTestCanClone>
{
    public DataForTestCanClone() { }

    public DataForTestCanClone(DataForTestCanClone? priority, DataForTestCanClone? fallback = default)
    {
        Name = priority?.Name ?? fallback?.Name;
        Id = priority?.Id ?? fallback?.Id;
        Description = priority?.Description ?? fallback?.Description;
    }

    public DataForTestCanClone CloneWith(DataForTestCanClone? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);
}