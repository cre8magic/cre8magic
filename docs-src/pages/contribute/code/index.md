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
