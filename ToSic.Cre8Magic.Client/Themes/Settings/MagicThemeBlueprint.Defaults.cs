using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Spells.Internal;

namespace ToSic.Cre8magic.Themes.Settings;

public partial record MagicThemeBlueprint
{
    internal const string MainPrefix = "theme";
    private const string PanePrefix = "pane";
    internal const string SettingFromDefaults = $"{MainPrefix}-warning-this-is-from-defaults-you-should-set-your-own-value";

    private const string ContainerIdDefault = "module-[Module.Id]";
    internal const string ModulePrefixDefault = "module";


    internal static Defaults<MagicThemeBlueprint> Defaults = new()
    {
        Fallback = new()
        {
            PaneIsEmpty = new($"{PanePrefix}-is-empty"),
            Parts = new()
            {
                {
                    "container", new()
                    {
                        Classes = $"{MainPrefix}-page-language {SettingFromDefaults}",
                        IsPublished = new(null,
                            $"{ModulePrefixDefault}-unpublished  {SettingFromDefaults}"),
                        IsAdmin = new($"{MainPrefix}-admin-container  {SettingFromDefaults}"),
                        Id = ContainerIdDefault,
                    }
                },
            },
        },
        Foundation = new()
        {
            PaneIsEmpty = new(),
            Parts = new()
            {
                { "container", new() { Id = ContainerIdDefault } },
            }
        },
    };
}