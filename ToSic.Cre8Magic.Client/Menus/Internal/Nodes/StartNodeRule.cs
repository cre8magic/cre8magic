using System.Text.Json.Serialization;

namespace ToSic.Cre8magic.Menus.Internal.Nodes;

internal class StartNodeRule
{
    public int Id { get; set; }

    public bool Force { get; set; } = false;

    public string From { get; set; } = "*"; // "*", ".", "43"

    public int Level { get; set; } = 0; // 0 meaning current, not top...// -1, -2, -3; 1, 2, 3

    public bool ShowChildren { get; set; } = false;

    [JsonIgnore]
    internal StartMode ModeInfo => _mode != default
        ? _mode
        : _mode = Id != default
            ? StartMode.PageId
            : From == MagicMenuSettingsData.StartPageRoot
                ? StartMode.Root
                : From == MagicMenuSettingsData.StartPageCurrent
                    ? StartMode.Current
                    : StartMode.Unknown;

    private StartMode _mode;
}