namespace ToSic.Cre8magic.Menus.Internal.Nodes;

internal record StartNodeRule
{
    public int Id { get; init; }

    public bool Force { get; init; } = false;


    public int Level { get; init; } = 0; // 0 meaning current, not top...// -1, -2, -3; 1, 2, 3

    public bool ShowChildren { get; init; } = false;

    public StartMode ModeInfo { get; init; }

    public required string Raw { get; init; }
}