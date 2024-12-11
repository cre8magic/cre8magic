using ToSic.Cre8magic.Spells;

namespace ToSic.Cre8magic.TestTheme.Client;

internal class ThemeSettingsForTests
{
    public static MagicSpellsBook Book1 => new()
    {
        Source = nameof(Book1),
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
                        { "en", new() { Label = $"En {nameof(Book1)}", Description = "English from Code" } },
                        { "de", new() { Label = $"DE {nameof(Book1)}", Description = "DE from Code" } },
                    },
                }
            },
        }
    };
}