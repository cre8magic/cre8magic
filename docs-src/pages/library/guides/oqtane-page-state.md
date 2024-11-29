---
uid: Library.Guides.OqtanePageState
---

# Oqtane PageState and How to Use it

The Oqtane **PageState** is an object which the platform Oqtane updates continuously
and passes on to all components on the page.

Here you'll learn what the PageState is, and how to use it in different scenarios.

## What is the Oqtane PageState

Internally Oqtane creates the `PageState` object and updates it whenever the page changes.
It contains various things such as:

1. current-page information such as the current page, list of modules on it etc.
1. list of all pages etc.
1. site information to access ID, URL, etc.
1. request information such as url parameters

This object is provided by Oqtane automatically as a [CascadingParameter](xref:Library.Guides.OqtaneCascadingParameters).

> [!TIP]
> Most operations in cre8magic will require the `PageState` object.
> There are a few ways to access it, explained below.

## Get the PageState

If you inherit from any standard Oqtane component (_not recommended_), you can access the `PageState` object directly.
This is because these components are already set up to receive the `PageState` object.

[!include[](../shared/do-inherit-from-componentbase.md)]

To access the `PageState` from any normal component, just add this section to your file which
inherits from `ComponentBase`:

```csharp
@inherits ComponentBase
@code {
    [CascadingParameter] PageState PageState { get; set; }
}
```

## Use the PageState to get a Kit from the MagicHat

Almost all operations in cre8magic require a `Kit` object.
This is created for you by the `IMagicHat` service,
which usually needs the `PageState` to figure out the context.

There are two ways that the `MagicHat` can get the `PageState`:

1. Directly in the request - there are a few ways to do this
1. Provide it in the theme, so that all components can magically have it (less code)

### Pass the PageState to the MagicHat in the Call

Since there will be various scenarios where you need to pass the `PageState` to the `MagicHat`,
we have created a few ways to do this.

The most simple one creates a new [](xref:ToSic.Cre8magic.Menus.MagicMenuSettings) and sets the `PageState` property:

```csharp
@{
  var menuKit = MagicHat.MenuKit(new() { PageState = PageState });
}
```

In other scenarios you may already have a [](xref:ToSic.Cre8magic.Menus.MagicMenuSettings) object,
but want to extend it with the `PageState`:

```csharp
@code {
  // The PageState provided by Oqtane
  [CascadingParameter] PageState PageState { get; set; }

  // The Settings handed into this component as a required parameter (so it's never null)
  [Parameter, EditorRequired] MagicMenuSettings Settings { get; set; }
}
@{
  // Extend the existing settings with the PageState using the records-with syntax
  var menuKit = MagicHat.MenuKit(Settings with { PageState = PageState });
}
```

> [!TIP]
> The snippet above uses the `with` statement to create a new object with the `PageState` property set.
> This is a core feature of the newer C# languages, and records are used extensively in cre8magic.

But the most common scenario is that you _might_ have a [](xref:ToSic.Cre8magic.Menus.MagicMenuSettings) object.
Since it could also be null, the `with` statement above would fail.
To make things easier, we created an extension method to allow this syntax:

```csharp
@code {
  // The PageState provided by Oqtane
  [CascadingParameter] PageState PageState { get; set; }

  // The Settings handed into this component; NOT required, so it could be null
  [Parameter] MagicMenuSettings? Settings { get; set; }
}
@{
  // Extend the existing settings with the PageState using the records-with syntax
  var menuKit = MagicHat.MenuKit(Settings.With(PageState));
}
```

Internally this does the same as the original `with` statement, but also creates a new object if it's null.

## Provide the PageState in the Theme (less code)

The above examples are the fool-proof way of doing things,
but it does add some plumbing which isn't exactly magical.

Since advanced setups will usually have a cre8magic-aware Theme,
which has some initialization code running, you can also provide the `PageState` there.

```csharp
// In your Theme.razor file
@code {
    /// <summary>
    /// Get the Magic Hat from the DI
    /// </summary>
    [Inject] public required IMagicHat MagicHat { get; set; }

    /// <summary>
    /// OnInitialized will run early (and once only).
    /// It also runs before OnInitializedAsync.
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        // Provide the first PageState as early as possible.
        MagicHat.UsePageState(PageState);
    }

    /// <summary>
    /// This will run whenever any parameter changes - such as PageState.
    /// It also runs before OnParametersSetAsync.
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        // Provide the latest PageState on every change
        MagicHat.UsePageState(PageState);
    }
}
```

The **MagicHat 🎩** will remember the `PageState` for you, so your components can now be simpler.
You can now write this:

```csharp
@{
  var menuKit = MagicHat.MenuKit(Settings);
}
```

...instead of the fairly lengthy:

```csharp
@code {
  // The PageState provided by Oqtane
  [CascadingParameter] PageState PageState { get; set; }

  // The Settings handed into this component; NOT required, so it could be null
  [Parameter] MagicMenuSettings? Settings { get; set; }
}
@{
  // Extend the existing settings with the PageState using the records-with syntax
  var menuKit = MagicHat.MenuKit(Settings.With(PageState));
}
```

> [!TIP]
> When creating a theme which fully uses cre8magic, we recommend this approach.
>
> But when creating reusable components for others, you cannot be sure that
> they have configured the theme to proved the `PageState`,
> so your standalone-components should always be able to handle the `PageState` themselves.
  

---

## History

1. ...