---
uid: OqtaneThemes.Blazor.Parameters.Index
---

# Oqtane Blazor Theme Creation - Component Parameters Guide

Blazor has a few secret tricks up its sleeve that can make your life easier when creating themes.
A core challenge is passing parameters and settings around between components.

## Basics: 3 Ways to Pass Parameters

There are 3 ways to pass parameters between components in Blazor:

1. [On a Component Attribute](#pass-parameters-on-a-component-attribute) - the simplest way which you'll use a lot
2. [Cascade Parameters from Theme to any Control](#cascade-parameters-from-theme-to-any-control) - Oqtane uses this a lot
3. Using a Service

## Pass Parameters on a Component Attribute

The simplest way to pass a parameter.
Just add it to the attribute of the component.

```html
@* Example with a direct value *@
<MyComponent MyParameter="Hello World" FavoriteNumber="42" />

@* Example with a variable *@
@{
  string myHello = "Hello World";
  int myFavoriteNumber = 42;
}
<MyComponent MyParameter="@myHello" FavoriteNumber="myFavoriteNumber" />
```

Your component would then need to look a bit like this:

```razor
@code
{
  [Parameter]
  public string MyParameter { get; set; }

  [Parameter]
  public int FavoriteNumber { get; set; }
}
```

You can also tell the UI that this parameter is required,
so that Visual Studio and the compiler will tell the developer that they need to provide it.

```razor
@code
{
  [Parameter, EditorRequired]
  public required string MyParameter { get; set; }
}
```

> [!TIP]
> It's not always clear (especially on strings)
> if the parameter should be the word `myVariable` or the contents of the variable `myVariable`.
> In most cases this is automatically deduced, but in cases of strings it can go wrong.
> When in doubt, use the `@` symbol to tell Blazor to use the contents of the variable.

You can also pass in larger objects, like a class or a list:

```razor
@{
  var myObject = new MyObject { Name = "John", Age = 42 };
  var myListOfObjects = new List<MyObject> { myObject, myObject };
}
<MyComponent MyObject="myObject" MyListOfObjects="myListOfObjects" />
```

The component would then look like this:

```razor
@code
{
  [Parameter]
  public MyObject MyObject { get; set; }

  [Parameter]
  public List<MyObject> MyListOfObjects { get; set; }
}
```

### How Component Parameters are Used in Oqtane

The Oqtane Framework won't give your components any data directly,
so it won't pass in any parameters to your components like this.

So this mechanism is mostly used for passing parameters between your components in your theme.

### Timing Issues with Component Parameters

When you pass in a parameter to a component, it's not always available right away.

This is because the component is initialized before the parameter is set,
and sometimes the parent component is not yet ready to pass in the parameter.

Some tips:

1. always assume that the parameters could contain an empty / default value at first
1. use the `OnParametersSet` or `OnParametersSetAsync` lifecycle methods to initialize other objects
1. ... and to react to parameter changes
1. when something else is initialized with this parameter, also assume that it could be empty at first


## Cascade Parameters from Theme to any Control

Cascading parameters are a way to pass parameters down the component tree.
This is useful when you have a lot of nested components and you don't want to pass the parameter through all of them.

Imagine the following component tree:

* Theme
  * Menu Component
    * Menu Item Component
      * Menu Icon Component
    * Menu Item
    * Menu Item

This is what the Theme would look like:

```html
@{
  var myObject = new IconSettings { Library = "FontAwesome" };
}
<CascadingValue Value="myObject">
  <MyMenu />
</CascadingValue>
```

And the MenuIcon Component would then look like this:

```razor
@code
{
  [CascadingParameter]
  public IconSettings MyIconSettings { get; set; }
}
```

This way you can pass the `IconSettings` object from the Theme to the MenuIcon Component without having to pass it through all the other components.

> [!TIP]
> Cascading parameters are mapped using the `Type` of the object.
> This is why in the previous example we used a `IconSettings` object,
> even though we're just passing in as `string` value.

### How Cascading Parameters are Used in Oqtane

Oqtane uses cascading parameters a lot.
You will typically get the following parameters from the Theme:

```razor
@code
{
  /// <summary>
  /// Get the PageState from the CascadingParameter
  /// </summary>
  [CascadingParameter]
  public required PageState PageState { get; set; }
}
```

If your control is inside a module (eg. part of a Container)
you can also get the ModuleState like this:

```razor
@code
{
  /// <summary>
  /// Get the ModuleState from the CascadingParameter
  /// </summary>
  [CascadingParameter]
  public required Module ModuleState { get; set; }
}
```

### Timing Issues with Cascading Parameters

Cascading parameters are set before the component is initialized,
so you can be sure that they are available when the component is created.
This is why you can use them to pass parameters to components that are not directly related to each other.

### Empty Values in Cascading Parameters

Note that cascading parameters can be empty, because the parent component did not set them,
or they were set to `null` or `default` by the parent component.

So if you create your own cascading parameters,
either mark them as `required` to get compiler errors, or do null/default checks in your component.

## Using a Service

The last way to pass parameters is to use a service.
This is a bit more advanced and not used as often as the other two ways.

Services allow you to:

1. pass parameters "downwards" in the component tree (like cascading parameters)
1. pass parameters "upwards" in the component tree
1. pass parameters "sideways" between components that are not directly related

For this to work, a service needs to be scoped correctly,
meaning that it should provide the same service instance for every component that needs it.

This is done by registering the service as a scoped service in the `Startup.cs` file:

```csharp
namespace ToSic.Cre8magic;

public class Startup : Oqtane.Services.IClientStartup
{
  /// <summary>
  /// Register Services
  /// </summary>
  /// <param name="services"></param>
  public void ConfigureServices(IServiceCollection services)
  {
    // All these Settings etc. should be scoped, so they don't have to reload for each click
    services.TryAddScoped<IMagicSettingsService, MagicSettingsService>();
  }
}
```

> [!TIP]
> **Scoped** services have different behaviors depending on the RenderMode.
> For now, just understand that they will always give the same instance
> for everything that is rendered in the same page.

### Simple Example (Oqtane 5+)

In Oqtane 5+ you can use the `[Inject]` attribute or `@inject` to inject services into your components.

> [!TIP]
> This is the classic way of doing things, but has a few issues related to timing.
> Read on...

Then you can use the service in your components like this:

```razor
@code
{
  [Inject]
  public IMagicSettingsService MagicSettingsService { get; set; }
}
```

or like this:

```razor
@code
@inject IMagicSettingsService MagicSettingsService
```

In .net 9 you can also use constructor injection (new in Oqtane 6)
which has some benefits:

```razor
@code
{
  private readonly IMagicSettingsService _magicSettingsService;

  public MyComponent(IMagicSettingsService magicSettingsService)
  {
    _magicSettingsService = magicSettingsService;
  }
}
```

### Timing Issues with `@inject` and `[Inject]` Services

Services injected with `@inject` or `[Inject]` are handled the same way,
and they are _not_ available when the component is constructed.
Instead, they are available in the `OnInitialized` and `OnInitializedAsync` lifecycle method.

This is important, because it's counterintuitive and different from the normal
constructor injection that you might know from normal C# services.

Either make sure your code expects this - or use constructor injection if you're on Oqtane 6.

### Constructor Injection (Oqtane 6+)

In Oqtane 6+ you can use constructor injection to inject services into your components.

> [!IMPORTANT]
> Using Constructor Injection is a great way to avoid timing issues with services.
> It's also nicer but it has some downsides:
>
> 1. You must separate your razor file into 2 files `.razor` and `.razor.cs` (with code behind) which is possibly not desired
> 1. You can't easily inherit the component anymore (`error CS7036: There is no argument given that corresponds to the required parameter...`)

So you can still inherit the component, but any descendant component must also use
2 razor files with code-behind, which is kind of viral...

> [!TIP]
> Because of this tradeoff, we currently recommend against using constructor injection in Oqtane Components.


### How Service Injections are Used for Parameters in Oqtane

_This is about parameters in Oqtane, not about general Service Injection._

In Oqtane, certain environment parameters are provided as a service instead of Cascading Parameters.

Specifically, the SiteState is provided as a service:

```razor
  [Inject]
  protected SiteState SiteState { get; set; }
```

This is because the SiteState will not change through the lifecycle of the page.


