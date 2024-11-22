# cre8magic Internal Architecture

This document should explain the internal structures of cr8magic, so that developers can understand how it works and how to extend it.

## Overview

For the architecture there are a few things to consider:

1. Stable external APIs
1. Consistent internal APIs to make this fairly complex thing easy to understand

## Main Paradigms For the Internal Architecture

1. **Composition over Inheritance**
   * We try to avoid inheritance as much as possible, and instead use composition.
   * This makes it easier to understand and extend the system.

1. **Plain Vanilla Blazor Components**
    * We use `ComponentBase` instead of `ThemeBase` etc.
    * This is a consequence of Composition over Inheritance.


1. **Read-Only Record Objects**
    * We use read-only `record` objects for state management.
    * For example, a `MagicSettings` object is a read-only record object.
    * This has many benefits, especially that it will be immutable and not cause side-effects.

1. **Interfaces instead of Objects**
    * We prefer to use interfaces instead of objects.
    * This makes it easier to understand and extend the system.

1. **Structure by Topic, not by Type**
    * We structure the code by topic, not by type.
    * This makes it easier to understand and extend the system.

1. **Autocaching in the Service**
    * We use autocaching in the service to make it faster and more efficient.

   
## Things to Consider

We want developers to be able to use the services without having to use the Components.

In addition, the settings mechanisms should be seamless, magical but still very easy to understand.

## Sample Best Practice Setup by Example of the Menu

1. **IMagicMenuKit** will be the object that contains everything to do with the menu.
1. **IMagicMenuService** will be the service that contains the logic to get the Kit.
1. **IMagicHat** is the main entry point which is usually used to call the service.
1. **MagicMenuSettingsData** (`record`) is the data object that is used for settings which can be stored.
    This object is usually not used the outside, as the outside will use the **MagicMenuSettings** (without the `Data`).
1. **MagicMenuSettings** (`record`) will be the settings object that is used to configure what the service should provide.
    1. It extends the `MagicMenuSettingsData` and adds some additional properties, like the `PartName` which would be used to retrieve named settings.
    1. This object will usually be provided to the `MagicHat` to get everything set up.
    1. After the setup it will also be found on the `.Settings` property of the `MagicMenuKit`.
    1. After the setup it will have more values filled (from the `MagicMenuSettingsData`).
1. **MagicMenuDesignSettings** ...
1. **MagicMenuDesigner** ...
1. The **/Menus/Components/MagicMenu** is a primary or sample component for using the menu.
    1. It will expect a `Settings` parameter which is the `MagicMenuSettings`.
    1. It will usually use the `MagicHat` to prepare the `MagicMenuKit`
    1. It will then use the `MagicMenuKit` to iterate the data, and render the menu.

## Settings Structures

1. **IMagicSettingsService** is the internal object which manages/provides the settings.
1. **IMagicSettingsSource** (of which there are many) is the object that _can_ provide settings.
    1. The **MagicSettingsSourceJson** is an example of this.
    1. Sources contain a `Catalog(...)` method to retrieve the **MagicSettingsCatalog**
1. **MagicSettingsCatalogsLoader** will collect all the _Sources_ and provide them to the **IMagicSettingsService**.
1. ...

## Settings Catalog and Structure

A Catalog contain all the settings which can be requested by any part of the system.

It follows two important conventions:

1. Flat Structure with Topic first (e.g. `Themes`, `ThemeDesigns` `Menus`, `MenuDesigns`, `Breadcrumbs`, ...)
1. Each of these Settings Groups will have a dictionary of settings, so each section can contain multiple named settings.
1. Everything is a `record` object, so it's immutable and can't cause side-effects.
1. Everything inside the catalog should support the `ICanClone<T>` interface, so these records can be duplicated & modified easily.

The catalog always follows the following conventions

1. A `ThemeSettings` (of which there can be many) is used
    1. to configure a theme
    1. and (for example) determine which parts should be shown and where they can find their settings.
1. `Menus` contains menu settings
1. `MenuDesigns` contains menu design settings

## Settings Json

...usually `theme.json`

Specials like

* `=`
* `inherits`

Internally 