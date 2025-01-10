using ToSic.Cre8magic.Settings;

namespace ToSic.Cre8magic.TestTheme.Client;

internal class MyThemeSettings
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
                    { "logo", new() { Classes = "logo logo-interactive" } }
                },
            },
            Language = new()
            {
                Languages = new()
                {
                    { "en", new() { Label = "En Language", Description = "English from Code" } },
                    { "de", new() { Label = "DE Language", Description = "DE from Code" } },
                },
            }
        }
    };
}