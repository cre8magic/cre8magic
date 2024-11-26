---
uid: Cre8magic.MagicSettings.Index
---

# cre8magic - Magic Settings and Prepared Settings

**Magic Settings** are a core building block of cre8magic.
There are two basic ways to work with Magic Settings:

1. With **Direct Settings**, you will specify the settings in your code to get some specific data from a service,
2. or you can have **Prepared/Provided Settings** in a central location, and just use the name of the settings.

Before we explain the details, let's look at some examples.

## Example Direct Settings vs. Prepared Settings

### 1. Direct Settings

This example assumes you want to create a Breadcrumb, with all the code directly in your Blazor component.

1. going from the home page to the current page
1. you want the home page to be shown as the first node
1. and you want the current page to be shown as the last node, but not as a link

_Note: this sample uses a lengthy syntax just for better clarity._

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

### 2. Loaded Settings

This example assumes that settings have been prepare for this control to use.
So the code is simply:

```csharp
@* Breadcrumb using the settings from the catalog *@
@{
    breadcrumbKit = MagicHat.BreadcrumbKit(PageState);
    // Note: equivalent to ... = MagicHat.BreadcrumbKit(PageState, new() { SettingsName = "default" });
}
<ol class="breadcrumb">
    @foreach (var item in breadcrumbKit.Pages)
    {
        @* ... *@
    }
</ol>
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

## Preparing / Providing Settings

The settings are prepared in a central location.

> [!TIP]
> Since each Theme can have its own settings,
> the central location to prepare settings is in the Theme code.

There are different ways to prepare settings:

1. The theme code could have code directly to prepare settings