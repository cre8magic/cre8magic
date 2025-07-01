---
uid: OqtaneThemes.Blazor.DependencyInjection.Index
---

# Oqtane Blazor Theme Creation - Dependency Injection Guide

Modern best practices in Blazor development encourage the use of Dependency Injection (DI) to manage dependencies and pass parameters between components.
This guide will help you understand how to effectively use DI in your Oqtane themes.

## What is Dependency Injection?

Dependency Injection is a design pattern that allows you to inject dependencies into a class rather than hard-coding them within the class.
This promotes loose coupling and makes your code more testable and maintainable.
In Blazor, you can use DI to pass services and parameters to your components, making them more flexible and reusable.

## How to Use Dependency Injection in Oqtane Themes

### Step 1: Create a Service

First you need to create a service that you want to inject into your components. This service can be anything from a simple data provider to a more complex business logic handler.

```csharp
public class MyService
{
    public string GetData()
    {
        return "Hello from MyService!";
    }
}
```

### Step 2: Register Services

In Oqtane, you can register services in the `ThemeStartup.cs` file of your theme project.

```csharp
using Microsoft.Extensions.DependencyInjection;

namespace ToSic.Cre8magic.Theme.Basic;

public class ThemeStartup : Oqtane.Services.IClientStartup
{
    /// <summary>
    /// Register Services which this theme needs.
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        // Register the MySettings helper
        services.AddTransient<MyService>();
    }
}
```

### Step 3: Inject Services into Components

You can inject the service into your Blazor components using the `[Inject]` attribute or the `@inject` directive.

```razor
@inject MyService MyService

<h3>Data from MyService:</h3>
<p>@MyService.GetData()</p>
```
