using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.TestTheme.Client;

internal class ThemeSettingsForTests
{
    public static MagicSettingsCatalog Catalog1 => new()
    {
        Source = nameof(Catalog1),
        Themes = new()
        {
            {
                "default", new()
                {
                    Logo = "[Theme.Url]/assets/LOGO.svg",
                }
            },
        },
        Languages = new()
        {
            {
                "default", new()
                {
                    Languages = new()
                    {
                        { "en", new() { Label = $"En {nameof(Catalog1)}", Description = "English from Code" } },
                        { "de", new() { Label = $"DE {nameof(Catalog1)}", Description = "DE from Code" } },
                    },
                }
            },
        }
    };
}