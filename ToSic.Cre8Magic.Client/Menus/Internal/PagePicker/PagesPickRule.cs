namespace ToSic.Cre8magic.Menus.Internal.PagePicker;

/// <summary>
/// Rule / specs for picking / finding a page.
/// </summary>
internal record PagesPickRule
{
    public int Id { get; init; } = 0;

    public bool Force { get; init; } = false;

    public int Depth { get; init; } = 1;

    public int Level { get; init; } = 0; // 0 meaning current, not top...// -1, -2, -3; 1, 2, 3

    public bool ShowChildren { get; init; } = false;

    public PickMode PickMode { get; init; }

    public required string Raw { get; init; }
}