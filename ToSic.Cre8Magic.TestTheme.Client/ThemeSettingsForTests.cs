using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.TestTheme.Client;

internal class ThemeSettingsForTests
{
    public static MagicBook Book => new()
    {
        Source = nameof(Book),
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
                        { "en", new() { Label = $"En {nameof(Book)}", Description = "English from Code" } },
                        { "de", new() { Label = $"DE {nameof(Book)}", Description = "DE from Code" } },
                    },
                }
            },
        }
    };
}