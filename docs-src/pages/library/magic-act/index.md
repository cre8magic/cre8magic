---
uid: Cre8magic.Library.MagicAct.Index
---

# Magic Act for Oqtane Themes

_The cre8magic Act is the central coordinator for everything in Oqtane Themes._

> [!NOTE]
> If you're just getting started with **cre8magic ♾️**
> you don't need this information yet.
>
> Come back here once you've played around and want to any of the following:
>
> 1. Understand how it works under the hood
> 1. Create your own components
> 1. Broadcast centralized settings from the theme or from a JSON file.

## Magic Act TL;DR

Having a great theme is like having a great **Magic Act 🎭**.

So the **Magic Act 🎭** is mainly used for 2 things

1. It is used by **Components** to get everything they need
1. It is used by **Theme** to "broadcast" settings to the components

This is the typical process of creating output in Oqtane with **cre8magic ♾️**:

[!include [](../shared/_magic-process-in-a-nutshell.md)]

**cre8magic ♾️** allows you to use the components or create your own, and things will just work.
But once you really start using it, you will want to centralize settings and make things more flexible.
This is where the **Magic Act 🎭** comes in.

The first thing the **Magic Act 🎭** does is to take any settings and prepare a **Kit 🧰**
so that the components can easily create the desired output.
By default, there are no additional settings, so the **Magic Act 🎭** will just use the defaults.

The second thing the **Magic Act 🎭** does is to broadcast settings.
So once you have settings to broadcast (either from the Theme Code or from a JSON File),
you can use the **Magic Act 🎭** to set everything up.

## Use in Components

### [Pre-Built Components](#tab/pre-built-components)

When using the pre-built components, you don't need to worry about the **Magic Act 🎭**.
Code like this will just do everything for you:

```html
@using ToSic.Cre8magic.OqtaneBs5
<MagicMenu Settings='new() { Pick = "/" }'/>
```

Internally these components will use the **Magic Act 🎭** to get the settings and prepare the **Kit 🧰**,
but you don't need to know about this.

### [Custom Components](#tab/custom-components)

When you're creating your own components,
you will need to use the **Magic Act 🎭** to get the **Kits 🧰**
and start creating your output.
Here's an example:

```razor
@inherits ComponentBase
@using ToSic.Cre8magic.Act
@using ToSic.Cre8magic.Pages
@* Inject the IMagicAct service *@
@inject IMagicAct MagicAct
@code{
  [CascadingParameter] public required PageState PageState { get; set; }

  RenderFragment RenderMenu(IMagicPage current) =>
    @<ul>
      // ...
    </ul>;
}

@{
  // Use the MagicAct to get the MenuKit with specified settings.
  var kit = MagicAct.MenuKit(new() {
    Pick = "/+",
    PageState = PageState
  });
}
@RenderMenu(kit.Root)
```

---

## Use in Themes

You can use the **Magic Act 🎭** in your theme to get a **ThemeKit** and work with it.

### [Basic Example](#tab/basic-example)

```razor
@using ToSic.Cre8magic.Act
@using ToSic.Cre8magic.Themes;
@inject IMagicAct MagicAct
@code {
  IMagicThemeKit ThemeKit =>
    field ??= MagicAct.ThemeKit(new() { PageState = PageState });
}
```

> [!NOTE]
> Doing this is possible, but that probably doesn't make much sense.
> Usually you want to broadcast settings to the components.
> For this, it's best to inherit the `MagicTheme` and take it from there.

---

### Inherit MagicTheme & Broadcast Settings

When creating a theme, you will typically

1. inherit from `MagicTheme`
1. initialize the `ThemePackage`

This will allow the **Magic Act 🎭** to pick up all relevant settings such as the path etc.
From now on, every component within this theme will have these settings.

#### [Basic Example](#tab/theme-single-package)

```c#
public class MyTheme : ToSic.Cre8magic.OqtaneBs5.MagicTheme
{
  /// <summary>
  /// Set the ThemePackage, so the system has all it needs.
  /// </summary>
  public override MagicThemePackage ThemePackage =>
    field ??= new(new ThemeInfo());

  // ...
}
```

#### [Theme with Variations](#tab/theme-with-variations)

If your theme has multiple configurations, you may also want to specify
a name so that the system will retrieve the correct settings.

```c#
public class MyDefaultTheme : ToSic.Cre8magic.OqtaneBs5.MagicTheme
{
  /// <summary>
  /// Set the ThemePackage, so the system has all it needs.
  /// </summary>
  public override MagicThemePackage ThemePackage =>
    field ??= new(new ThemeInfo(), "Default");

  // ...
}
```

```c#
public class MyGreenTheme : ToSic.Cre8magic.OqtaneBs5.MagicTheme
{
  /// <summary>
  /// Set the ThemePackage, so the system has all it needs.
  /// </summary>
  public override MagicThemePackage ThemePackage =>
    field ??= new(new ThemeInfo(), "Green");

  // ...
}
```




---

---

## Missing Features

1. TODO:

## History

1. Added in v0.05 2024-10
