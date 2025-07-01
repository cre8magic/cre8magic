---
uid: OqtaneThemes.Recommendations.Index
---

# Oqtane Theme Recommendations and Best Practices

These are recommendations base on the cre8magic team's experience with Oqtane themes.

We see these things as best practices, but they are not required,
or may also be different from the official Oqtane Best Practices.





## Components should Inherit from ComponentBase, Not ThemeControlBase

This applies to the many components you create in your theme, eg. for showing an icon, a menu, a breadcrumb, etc.

It does not apply to the Theme itself, which should inherit from `ThemeBase`,
or to Modules, which should inherit from `ModuleBase`.

### Do: Inherit from the standard ComponentBase

This is in the `Microsoft.AspNetCore.Components` namespace.

Your Component will usually require the Page-State, so you should add:

```razor
@code
{
  [CascadingParameter]
  public required PageState PageState { get; set; }
}

```

In some cases you may also need the Site-State or the Module-State.
If you do need them, do this:

```razor
@code
{
  [Inject]
  public required SiteState SiteState { get; set; }

  [CascadingParameter]
  public required Module ModuleState { get; set; }
}
```


### Don't: Inherit from ThemeControlBase

The official way to create Components in Oqtane
is to call them `Controls` and inherit from `ThemeControlBase`.

### Our Reasoning

The `ThemeControlBase` is a custom class that is part of the Oqtane framework.
This in turn inherits from `ThemeBase` which (IMHO) has various issues such as:

- It uses an Inheritance strategy instead of Composition, which we think is wrong.
- It mixes a lot of responsibilities
- It's huge and has a lot of APIs which we believe should not be used.

In 99% of all cases all you need is the `PageState`, which you can get without this additional baggage.
Doing this will also keep your code clean and more maintainable.

