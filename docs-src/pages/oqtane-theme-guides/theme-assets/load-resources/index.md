---
uid: OqtaneThemes.ThemeAssets.LoadResources.Index
---
# Load Resources in the Browser

Resources can include **images**, **CSS files**, **JavaScript files**, **fonts**, and more.


> [!TIP]
> - **Path Reference:** Resource paths like `~/Theme.css` are relative to the `wwwroot` of the theme. TODO: Resources
> - **Load Order Matters:** The order of resources can be important (e.g., load Bootstrap first, then custom styles). TODO: Resources

---

## Global Resources – `ThemeInfo.cs`

Global resources can be defined in the `ThemeInfo.cs` file. These are **always**
loaded as soon as the theme is used – regardless of which variant is currently active.

[Show Example from ThemeInfo.cs](xref:OqtaneThemes.ThemeInfo.Index)


## Theme-Specific Resources

Alternatively, you can define resources directly in the Razor file of your theme (e.g., in `Fullscreen.razor` or `Theme.cshtml`). These resources are only loaded when the corresponding theme is active.

This helps reduce load times and is ideal when different theme variants require different styles or scripts.

Link to Code: https://github.com/cre8magic/cre8magic/blob/fa9922b81e8e0d97e428f272306010f335a16b3b/ToSic.Cre8Magic.Seo.Client/Modules/ToSic.Cre8magic.Seo/Index.razor#L22-L27

<!-- <div gallery="gallery01">
  <img src="./assets/theme-resources_2.webp" data-caption="Example Fullscreen Theme">
</div> -->


## How Resources Are Loaded

- Global resources (`ThemeInfo.cs`) are automatically loaded when a theme is loaded – regardless of the Razor component being used.

