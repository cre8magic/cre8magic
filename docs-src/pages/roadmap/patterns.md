# Patterns

cre8magic strives to follow the best practices of software development.
This includes the use of patterns and principles that have been proven to work well in the past.

> [!TIP]
> Some of these patterns are very different from the patterns used in Oqtane.
> There are good reasons for this, which are explained in the respective sections.

## Settings > Service > Kit > Work

...



## Composition over Inheritance

cre8magic prefers composition over inheritance.

> [!WARNING]
> **♾️ vs. 💧**
> This pattern used by cre8magic is seen as a best practice by the cre8magic team
> and by many experts in the community.
> Oqtane currently uses a different pattern, which is prioritizes inheritance.

## Plain Vanilla Blazor Components

...`ComponentBase` instead of `ThemeBase` etc.

...consequence of Composition over Inheritance.

## Read-Only Record Objects

cre8magic uses read-only `record` objects for state management.
For example, a `MagicSettings` object is a read-only record object.

This has many benefits, especially that it will be immutable and not cause side-effects.

> [!WARNING]
> **♾️ vs. 💧**
> This pattern used by cre8magic is seen as a best practice by the cre8magic team
> and by many experts in the community.
> Oqtane currently uses a different pattern, which has mutable classes.

Example of ♾️ vs. 💧

* Oqtane's `Page` `object` is mutable, and can be changed at any time.
    You should never do this, and it can have severe side-effects.
    But the API doesn't prevent you from doing it.
* cre8magic's `MagicPage` `record` is immutable, and can never be changed.
    You can create new copies of it, but the original will always stay the same.

## Interfaces instead of Objects


## Autocachnig in the service...