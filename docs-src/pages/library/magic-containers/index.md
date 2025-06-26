---
uid: Cre8magic.Library.MagicContainers.Index
title: Magic Containers for Oqtane
---

# cre8magic – Magic Containers

_The cre8magic Containers help you create best-practice, customizable Oqtane containers in themes._

> [!TIP]
> Add best practice containers in your theme like this:
>
> [!code-csharp[](../../../../ToSic.Theme.Cre8magic.StandaloneDemos/Client/Containers/Container.razor)]
>
> This will make sure that Oqtane can find _your_ container
> (as it must be in the same namespace as the theme) and use it as a container.

In most cases, this is all you'll need to do.
But if you want to customize the container, you can do so by inheriting from the `Container` class and overriding the methods you want to change.

TODO: more variants


---

## Pending Tasks 2025-03-28

1. Ability to show the title after all - but default to false
1. Correct default css classes for containers
1. Ability to create custom containers inheriting, but replacing the tailor / settings only
1. Ability to detect what pane we are on, to use as alternate configurations - eg. `pane-default` or `pane-admin` etc.
1. Ability to detect what module we are on, to use as alternate configurations - eg. `module-ToSic.Sxc.Oqtane`
1. Documentation
    1. Sample to just inherit as is - recommended for most scenarios

---

## History

1. created in v0.0.1 2022-10
