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
namespace ToSic.Cre8magic.Theme.Basic;

public class ThemeInfo : ITheme
{
    /// <summary>
    /// The Theme will let Oqtane know what theme this is, and how it should be loaded / rendered etc.
    /// </summary>
    public Oqtane.Models.Theme Theme => new()
    {
        Name = "Cre8magic Basic",
        Version = "1.0.1",
        PackageName = "ToSic.Cre8magic.Theme.Basic",
        ThemeSettingsType = ThemeSettingsType,
        ContainerSettingsType = ContainerSettingsType,
        Resources =
        [
            new Stylesheet("~/Theme.css"),
            new Stylesheet("~/Oqtane.css"),
            new Stylesheet ("~/styles.min.css"), // Sass build with vite
            new Script("~/bootstrap.bundle.min.js"), // Bootstrap build with vite
        ]
    };

    #region Settings Type Names for Oqtane to Auto-Inject in the Theme/Module Settings Dialogs

    /// <summary>
    /// The assembly name, used to construct the full type names for settings.
    /// </summary>
    /// <remarks>
    /// The assembly name should not provide version etc. because it will be stored in the database, and future updates would result in a mismatch.
    /// </remarks>
    private static readonly string AssemblyName = typeof(ContainerSettings).Assembly.GetName().Name!;

    /// <summary>
    /// The Theme Settings Type will let Oqtane know what control to show in the page settings, and it will also be used to load language resources.
    /// </summary>
    public static readonly string ThemeSettingsType = $"{typeof(ThemeSettings).FullName}, {AssemblyName}";

    /// <summary>
    /// The Container Settings Type will let Oqtane know what control to show in the module settings, and it will also be used to load language resources.
    /// </summary>
    public static readonly string ContainerSettingsType = $"{typeof(ContainerSettings).FullName}, {AssemblyName}";

    #endregion

    /// <summary>
    /// Prefix used for all keys/settings in this theme.
    /// </summary>
    internal static string KeyPrefix = typeof(ThemeInfo).Namespace!;
}
```

### `ThemeInfo` Load

- On application startup, Oqtane scans for all classes that implement the `ITheme` interface.
- The `Theme` property of each class is automatically read.
- The provided information tells Oqtane how to render the theme and which resources to load.
- The defined resources (CSS, JS) are automatically injected into the `<head>` or before the `</body>` tag of each page.


> [!Tip]
> - **Path Reference:** Resource paths like `~/Theme.css` are relative to the `wwwroot` of the theme. TODO: Resources
> - **Load Order Matters:** The order of resources can be important (e.g., load Bootstrap first, then custom styles). TODO: Resources
> - **Versioning:** When making changes, be sure to update the `Version`—especially when deploying via NuGet. [Change Version](xref:OqtaneThemes.PublishTheme.Index)

> [!WARNING]
> The container must be in the same namespace as the theme.
>
> ```xml
> @namespace ToSic.Cre8magic.Theme.Basic
> ```

