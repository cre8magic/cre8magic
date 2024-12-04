---
uid: Cre8magic.MagicSettings.Index
---

# cre8magic - Magic Settings Overview

**Magic Settings** are a core building block of cre8magic.
Basically anything you do, will start of with some settings - or some invisible defaults.

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

## Settings vs. DesignSettings

TODO: explain the difference between settings and design settings
