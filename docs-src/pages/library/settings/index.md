---
uid: Cre8magic.Library.MagicSettings.Index
---

# cre8magic - Magic Settings Overview

**Magic Settings** are a core building block of cre8magic.
Basically anything you do, will start of with some settings - or some invisible defaults.

> [!TIP]
> **Magic Settings** allow you to move 95% of the theme code into some kind of configuration.

## Quick Introduction

Magic Settings are a way to provide settings to the various parts of cre8magic.
Let's say you want to create a footer-menu, which only shows **Home** and **Login**.

```html
<!-- This is what you would do -->
`<MagicMenu Settings='new () { Nodes = "/, 5" }' />`

<!-- Note that the previous is just a shorthand for -->
`<MagicMenu Settings='new MagicMenuSettings() { Nodes = "/, 5" }' />`
```

Settings can also be provided in a central location, and just use the name of the settings:

```html
<!-- Get menu-settings called "footer" -->
`<MagicMenu SettingsName="footer" />`

<!-- Get all settings for the part "footer-menu" -->
`<MagicMenu PartName="footer-menu" />`
```

Settings are also used to get **Kits** so you can write your own code to do things, like this:

```csharp
var menuKit = MagicAct.MenuKit(new () { Nodes = "/, 5" });
```

Settings are also used when you don't provide them, in which case some minimal defaults are used:

```csharp
var menuKit = MagicAct.MenuKit();
```

> [!NOTE]
> Settings are used to specify what you want to do, and how you want to do it.
> On the other hand, **Blueprints** are used to customize the output
> using **Tailors**.
> So you could think of **Blueprints** as _Design Settings_ but they are treated differently.

## Overview

These are the things you should probably know:

1. What settings can be used for each scenario
1. How to provide settings in a central location
1. How to provide settings by name or theme part name (TODO:)
1. How to provide design settings (TODO:)
1. How to provide PageState (TODO:)
1. How settings work internally

## Settings for each Scenario

Settings will differ depending on if you're creating a menu, a breadcrumb, a Magic Context, or something else.
So for this you should look at the respective documentation.

TODO: list all relevant parts

## Providing Settings

Settings can be used directly in the code, like:

```csharp
var breadcrumbKit = MagicAct.BreadcrumbKit(new MagicBreadcrumbSettings
{
  PageState = PageState,
  WithActive = true,
  WithHome = false,
});
```

Or you can have settings prepared in a central location, and just use the name of the settings:

```csharp
var breadcrumbKit = MagicAct.BreadcrumbKit(PageState);
```

The second way is especially useful when the _identical_ code and components should result in different outputs
depending on the scenario, since it allows you to change the settings in one place.

For example, you could define that certain menus don't appear or look different within certain parts of your site.

For this, see

* [Providing Settings](./provide-settings.md).
* Providing Named Settings TODO:
* Providing Settings by Theme Part Name TODO:

## How Settings Work Internally

TODO: explain how settings are used internally

Settings have a 2-level hierarchy, shown by the example of a breadcrumb:

`MagicBreadcrumbSettings` would be the main settings which are used in your code.
These settings have all the possible options, but also some properties which
can only be set by code.

For example, you can set a `Designer` which is a special object that can create some HTML.

The `MagicBreadcrumbSettingsData` is the underlying class behind the `MagicBreadcrumbSettings`.
It contains all the properties which can be stored externally, like in a JSON file or database.



## Levels of Indirection WIP

1. Theme Variant
1. Part in that Theme Variant
1. Settings
1. Design Settings
1. Inheritance on every level


## Overview - old, json file...

Basically the magic settings let you put a bunch of parameters in a JSON file.
This file is then used by your Theme and it's Controls to

* determine what Blazor files to use (like what Template should be used for the menu)
* what to do with `class="..."` or `id="..."` in the HTML
* and much more 😉

> This basic principle allows you to create and tweak amazing designs
> without ever recompiling the Theme.
>
> It also allows you to create variations of your theme with the same Blazor files.

## Example JSON

See [settings-json](xref:Cre8magic.Library.ThemeSettings.Index) to see an example file.

## The Configuration File

The system works by creating a json file such as `theme-settings.json`.

This is placed in your themes `wwwroot` folder like this:

`wwwroot/ToSic.Themes.BlazorCms/theme-settings.json`

_Note that we don't use `...Client` in the path, just the real namespace._

Which file to use can be configured in the theme.
Normally you would use the same file for all variations of your theme, but the important thing is that the theme
must give some initial configuration object to the cre8magic Services.

Here's how:

### Create the MagicPackageSettings

This could be done anywhere, but I would place the code in the `ThemeInfo.cs` file:

```c#
    /// <summary>
    /// Default settings used in this package.
    /// They are defined here and given as initial values to the ThemeSettingsService in the Default Razor file.
    /// </summary>
    public static MagicPackageSettings ThemePackageDefaults = new()
    {
        // The package name is important, as it's used to find assets etc.
        PackageName = new ThemeInfo().Theme.PackageName,

        // The json file in the theme folder folder containing all kinds of settings etc.
        SettingsJsonFile = "theme-settings.json",
    };
```

### Tell the Theme to Use these Settings

Then in the theme, you should inherit from the `MagicTheme` base class and set the ThemePackageSettings like this:

```c#
public override MagicPackageSettings ThemePackageSettings => ThemeInfo.ThemePackageDefaults;
```

This would usually look a bit like this:

```c#
public abstract class MyThemeBase : MagicTheme
{
    public override List<Resource> Resources => new()
    {
        new() { ResourceType = Stylesheet, Url = $"{ThemePath()}theme.min.css" },       // Bootstrap generated with Sass/Webpack
        new() { ResourceType = Script, Url = $"{ThemePath()}bootstrap.bundle.min.js" }, // Bootstrap JS
        new() { ResourceType = Script, Url = $"{ThemePath()}ambient.js", },             // Ambient JS for page Up-button etc.
    };

    /// <summary>
    /// The ThemePackageSettings must be set in this class, so the Settings initializer can pick it up.
    /// </summary>
    public override MagicPackageSettings ThemePackageSettings => ThemeInfo.ThemePackageDefaults;

    public override string Panes => string.Join(",", PaneNames.Default, PaneNameHeader);
}
```

that's it ✌🏽

## How the Settings Work

Internally the `MagicSettingsService` will be initialized automatically by the `MagicTheme` base class.
It will then go and pick up the JSON file, parse it, do a bunch of magic and come back with a final `MagicSettings` object.
This `MagicSettings` will then contain all the important settings for the current page/theme.
It will also keep a reference to other settings such as `Menus` for which many configurations can exist.

## How the Settings are Broadcast

A key feature of this system is that the settings are initially loaded in the theme,
and then broadcast to all controls used in that theme.

To make this happen, the theme must wrap everything in a `MagicContextAndSettings` tag:

```razor
<MagicContextAndSettings Settings="Settings">
  Content
</MagicContextAndSettings>
```

This will do a few things

1. Make sure that the inner content is only shown if Settings are loaded - otherwise show a `loading settings...` text
1. Broadcast the `MagicSettings` with the name `Settings` to all child controls using `CascadingValue`.
1. It will also ensure that the [Magic Page Context](xref:Cre8magic.Library.MagicPageContext.Index) is set on the page

TODO:

1. EXPLAIN SETTINGS MORE

## Continue...

Then continue back to the 👉🏾 [Home](../index.md)

