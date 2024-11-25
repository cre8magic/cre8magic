---
uid: Cre8magic.MagicSettings.Index
---

# cre8magic - Magic Settings

**Magic Settings** are a core building block of cre8magic.
There are two basic ways to work with Magic Settings:

1. You will either use them to directly get some specific data from a service,
2. or you can place the settings in a central location, and just use the name of the settings.

## Example

This example assumes you want to create a Breadcrumb, with all the code directly in your Blazor component.

1. going from the home page to the current page
1. you want the home page to be shown as the first node
1. and you want the current page to be shown as the last node, but not as a link

_Note: this sample skips adding Bootstrap classes, proper design other best-practices._
_It also uses a lengthy syntax just for better clarity._

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
        <li class='breadcrumb-item'>
            @if (item.IsActive)
            {
                <span aria-current="page">@item.Name</span>
            }
            else
            {
                <a href="@item.Link">@item.Name</a>
            }
        </li>
    }
</ol>
```