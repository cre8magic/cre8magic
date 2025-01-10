using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.TestTheme.Client;

internal class ThemeSettingsForTests
{
    public static MagicBook Book => new()
    {
        Source = nameof(Book),
        Chapter = new()
        {
            Theme = new()
            {
                Logo = $"{MagicTokens.ThemeUrl}/assets/LOGO.svg",
            },
            ThemeBlueprint = new()
            {
                Parts = new()
                {
                    { "logo", new() { Classes = "logo logo-manual" } }
                },
            },
            Language = new()
            {
                Languages = new()
                {
                    { "en", new() { Label = $"En {nameof(Book)}", Description = "English from Code" } },
                    { "de", new() { Label = $"DE {nameof(Book)}", Description = "DE from Code" } },
                },
            }
        }
    };
}