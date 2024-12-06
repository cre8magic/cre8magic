---
uid: Cre8magic.Library.Guides.OqtaneCascadingParameters
---

# Oqtane Cascading Parameter and How to Use it

Blazor uses a concept called **Cascading Parameters** to pass data from a parent component to a child component.

Here you'll learn how this works, how Oqtane uses it, and how this affects your work with cre8magic.

## Cascading Parameters in Blazor

In short, a parent component can prepare an object and pass it down to a child component.
The parent would do something like this:

```csharp
// ParentComponent.razor
@code {
  private ThemeInfo theme = new() { ButtonClass = "btn-success" };

  private void ChangeToDarkTheme()
  {
      theme = new() { ButtonClass = "btn-secondary" };
  }
}
<CascadingValue Value="theme">
    <ChildComponent />
</CascadingValue>
```

And the child component would receive it like this:

```csharp
// ChildComponent.razor
@code {
    [CascadingParameter] ThemeInfo Theme { get; set; }
}
```

➡️ For in-depth understanding, read the official docs on [Cascading Parameters](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/cascading-values-and-parameters).


## Oqtane and Cascading Parameters

Oqtane uses this specifically to tell components about the current page, site, module and other context information.
Specifically

1. The `PageState` object is passed down to all components in the page
1. The `ModuleState` object is passed down to all things module related - like the container or the module itself

Note that `SiteState` is not passed down, as it's provided as a service using dependency injection.

So any component which wants to know the PageState _must_ do this:

```csharp
@code {
    [CascadingParameter] PageState PageState { get; set; }
}
```

> [!TIP]
> It's important to be aware that this value can change,
> and that it's not available before `OnParametersSet` is called.

## Oqtane and Inheritance over Composition

Oqtane started with a design where components would inherit from a base class.
So if you inherit from a typical Oqtane component, you'll get the `PageState` (and sometimes `ModuleState`) properties for free.
This is the case for components which inherit from:

1. `ThemeBase` and all derived components
1. `ModuleBase` and all derived components
1. `ContainerBase` and derived components such as `ModuleTitle`, `ModuleActions`, `DefaultContainer` etc.
1. `ThemeControlBase` (inherits `ThemeBase`) and derived components such as `ControlPanel`, `Login`, `MenuBase` etc.

## Get PageState when Inheriting from ComponentBase

[!include[](../shared/do-inherit-from-componentbase.md)]

If you inherit from `ComponentBase` your code will look like this:

```csharp
// Note: this next line is not necessary, but we recommend it for clarity
@inherits ComponentBase
@code {
    [CascadingParameter] PageState PageState { get; set; }
}
```

---

Updated 2024-11-29 / Oqtane 6.0
