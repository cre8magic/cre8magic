---
uid: Cre8magic.MagicSettings.Provide.Index
---

# cre8magic - Magic Settings and Prepared Settings

**Magic Settings** are a core building block of cre8magic.
There are two basic ways to work with Magic Settings:

1. With **Direct Settings**, you will specify the settings in your code to get some specific data from a service.
2. Or you can have **Prepared/Provided Settings** in a central location, and just use these.
3. Or you can have **Named Settings** in a central location, and just use the name of the settings.
4. Or you can have **Settings by Theme Part Name** in a central location, and just use the name of the part.

The scenarios above each add more levels of indirection.
This could be confusing to start with, but very powerful once you understand it.

Before we explain the details, let's look at some examples.

## Example Direct Settings vs. Prepared Settings

### 1. Direct Settings

This example assumes you want to create a Breadcrumb, with all the code directly in your Blazor component.

1. going from the home page to the current page
1. you want the home page to be shown as the first node
1. and you want the current page to be shown as the last node, but not as a link

```csharp
@{
    var breadcrumbKit = MagicHat.BreadcrumbKit(PageState, new MagicBreadcrumbSettings
    {
        WithActive = true,
        WithHome = false,
    });
}
<ol class="breadcrumb">
    @foreach (var item in breadcrumbKit.Pages)
    {
        @* ... *@
    }
</ol>
```

Or when using a MagicBreadcrumb component:

```csharp
<MagicBreadcrumb Settings="new MagicBreadcrumbSettings { WithActive = true, WithHome = false }" />
```

### 2. Loaded Settings

This example assumes that settings have been prepare for this control to use.
So the code is simply:

```csharp
@* Breadcrumb using the settings from the catalog *@
@{
    breadcrumbKit = MagicHat.BreadcrumbKit(PageState);
}
<ol class="breadcrumb">
    @foreach (var item in breadcrumbKit.Pages)
    {
        @* ... *@
    }
</ol>
```

Or when using a MagicBreadcrumb component:

```csharp
<MagicBreadcrumb />
```

### 3. Loaded Settings by Name

The previous example didn't specify anything when retrieving the data.
Now there are cases where the **Prepared Settings** use names.
This may not be common for breadcrumbs, but typical for menus where you would have:

* a `main` menu showing all items starting from the home page
* a `footer` menu showing only a few pre-specified items

In this case, you should know that these two things are equivalent:

```csharp
@{
    breadcrumbKit = MagicHat.BreadcrumbKit(PageState);
    breadcrumbKit = = MagicHat.BreadcrumbKit(PageState, new() { SettingsName = "default" });
}
```

Or when using a MagicBreadcrumb component:

```csharp
<MagicBreadcrumb />
<MagicBreadcrumb SettingsName="default" />
```

## Preparing / Providing Default Settings

The settings are prepared and provided in a central location.

> [!TIP]
> Since each Theme can have its own settings,
> the central location to prepare settings is in the Theme code.

There are different ways to provide settings:

1. You could just write your own code to keep all settings in one place.
1. The theme code can setup the [MagicHat](xref:ToSic.Cre8magic.IMagicHat) with the settings.
1. The theme code can load settings from a JSON file into the [MagicHat](xref:ToSic.Cre8magic.IMagicHat)
1. _The theme code could load settings from a database (not supported yet)._

### 1. Write your own Code

This variant is not recommended, but included just for completeness.
You could just have a class which provides the settings:

```csharp
namespace MyCompany.MyTheme.Ui

public class MySettingsProvider
{
    public static MagicBreadcrumbSettings BreadcrumbSettings => new()
    {
        WithActive = true,
        WithHome = false,
    };
}
```

And then use it like this:

```csharp
@{
    breadcrumbKit = MagicHat.BreadcrumbKit(PageState, MySettingsProvider.BreadcrumbSettings);
}
```

### 2. Theme Code

This is the first recommended way to provide settings.
The settings can be managed centrally, but are still part of the Theme.
In terms of Separation of Concerns, we still recommend placing the code which has the settings
in it's own class (similar to the previous example).

This is the code you would use in your theme:

```csharp
/// <summary>
/// OnInitialized, make ure that cre8magic knows what settings this theme wants.
/// </summary>
protected override void OnInitialized()
{
    base.OnInitialized();
    MagicHat.UseSettingsProvider(p => p
      .Breadcrumbs.SetDefault(MySettingsProvider.BreadcrumbSettings)
      .Menus.SetDefault(MySettingsProvider.MenuSettings)
      .UserLogin.SetDefault(MySettingsProvider.UserLoginSettings)
    );
}
```

And then use it like this:

```csharp
@{
    breadcrumbKit = MagicHat.BreadcrumbKit(PageState);
}
```

### 3. Load Settings from JSON

This is the most flexible way to provide settings.
It's quite nice, because you can change settings without recompiling your theme.
This provides for a much nicer developer / designer experience.

The setup requires these things:

1. A JSON file in your theme's `wwwroot` folder.
1. A [MagicThemePackage](xref:ToSic.Cre8magic.Themes.MagicThemePackage) object in your theme.
