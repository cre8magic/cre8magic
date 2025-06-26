---
uid: Cre8magic.Roadmap.History
---

# Roadmap


## Working on v0.02 2024-11

Here we want to make it more architecturally sound, and more modular.

Goal is that some developers may only want to use certain services to make their life easier.
These people may not care about the settings system, projections or even styling system.

But those automations should still exist for most projects that just want to get things done.

So the goal is kind of the following - based on an example of the menu system:

1. Services
    1. We probably need some primary service which will do most of the work,
       To ensure that our startup logic is able to retrieve other services as well...
1. Factories
    1. Have a service - probably called `ToSic.Cre8Magic.Pages.MagicPagesService` - which is used to prepare the data for various use cases such as:
        1. Menu
        1. Breadcrumbs
        1. Sitemap
        1. Single pages which are smarter
        1. etc.
    1. These services should have a simple API but be able to do "everything"
        1. This should either be a named-params system
        2. Or a fluent API (TBD)
    1. Samples which just use the service, and custom Razor to do everything else.

### More Thoughts

* Probably remove the "Client" part in the namespace
* Probably make a topic based namespace system, not a model/services/controls system ? or the other way around?
* Unit tests?
* Possibly make smaller DLLs?, like a Core, A pages, etc.



## Version 0

### 2022-10-07 v0.0.2

1. Made menu design settings support inherit
1. Added json schema and published to https://2sic.github.io/cre8magic/schemas/2022-10/theme.json

### First Beta v0.01 2022-10-05

This was the first internal release of the project.
It was made to test various concepts such as:

1. Initial release for use on blazor-cms.org
1. Named settings for layouts, breadcrumbs, menus
1. Named settings for the design of layouts, breadcrumbs, menus
1. A bunch of base classes for Razor
1. A bunch of services to make it happen
1. MagicContext, MagicSettings, MagicConfigurations etc.

Basically...

1. Simple Bootstrap 5 Themes which can be easily customized without recompiling.
2. Settings-System which projects the configuration(s) to the various themes and components.
3. Creating various "standard" components that just do everything right.

This system had some important shortcomings, such as:

1. Black box - things magically worked - or didn't
2. We tried to compensate a lot of this with logging, but that only helped if you already knew a lot of the systems internals.
