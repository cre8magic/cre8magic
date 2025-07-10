---
 uid: OqtaneThemes.ThemeInfo.Index
---

# Theme Info Configuration Class for Themes

The `ThemeInfo.cs` file is a central C# class used to define and register a theme in Oqtane. It tells Oqtane:

- What the theme is called  
- What version it has  
- Which resources (CSS, JS) should be loaded  
- How theme and container settings should be handled  


## Code Example

```csharp
public class ThemeInfo : ITheme
{
    public Oqtane.Models.Theme Theme => new()
    {
        Name = "Cre8magic Basic",
        Version = "1.0.0",
        PackageName = "ToSic.Cre8magic.Theme.Basic",
        ThemeSettingsType = ThemeSettingsType,
        ContainerSettingsType = ContainerSettingsType,
        Resources =
        [
            new Stylesheet("~/Theme.css"),
            new Stylesheet("~/Oqtane.css"),
            new Stylesheet("~/styles.min.css"), // Sass build with vite
            new Script("~/bootstrap.bundle.min.js"), // Bootstrap build with vite
        ]
    };
}
```

### `ThemeInfo` Load

- On application startup, Oqtane scans for all classes that implement the `ITheme` interface.
- The `Theme` property of each class is automatically read.
- The provided information tells Oqtane how to render the theme and which resources to load.
- The defined resources (CSS, JS) are automatically injected into the `<head>` or before the `</body>` tag of each page.

> [!Tip]
> - **Path Reference:** Resource paths like `~/Theme.css` are relative to the root of the theme.
> - **Load Order Matters:** The order of resources can be important (e.g., load Bootstrap first, then custom styles).
> - **Versioning:** When making changes, be sure to update the `Version`—especially when deploying via NuGet.
