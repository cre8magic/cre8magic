# Contributing Code

> [!TIP]
> This is still a work in progress.

## Setup

The Solution is a Visual Studio 2022 solution.

It has a few special things to know:

1. The core project is `ToSic.Cre8magic.Client`
1. Most UI tests are found in the Module `ToSic.Module.Cre8magicTests`.


## Testing in the Browser

Place the module on a page and select various tests.

If you are logged in, you can also save the configuration so the same test will be shown when you revisit the page.

The reason we're doing this is because various tests will behave differently,
depending on the theme around it.

So this allows us to place the module on pages with different themes and see how it behaves.

## Important Conventions

### Group by Topic

1. Each topic should have its own folder. eg. `Menus`, `Breadcrumbs` etc.

### Internal as Much as Possible

Anything that is really not needed for the public use should be as internal as possible.

1. Anything internal that can be internal should be marked as `internal`  
    ...to really protect the public API surface.

1. Anything internal should be in a folder `Internal` in the root or any topic based folder.  
    ...this will be filtered out by the docs. (TODO)

1. Really internal objects should not be named `Magic...`
    * it's shorter
    * and we can spot internal stuff + spot leaks in the docs.
    * Note that this doesn't apply to internal implementations of interfaces, which should be named after the interface.

