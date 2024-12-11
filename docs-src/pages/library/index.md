---
uid: Cre8magic.Library.Index
---

# cre8magic – Oqtane Theme System

_services, components, and utilities for Oqtane Themes_

## Introduction

Creating awesome themes for any platform is a lot of work, and needs a lot of skill.
There's Bootstrap to master, as well as HTML, CSS and JavaScript to conjure up.
And then there's the platform-specific stuff to learn,
such as Blazor and the intricacies of Oqtane itself.

> The cre8magic library is a collection of services, components, utilities
> and documentations that help you create themes for Oqtane with less effort and more fun.

[2sic](https://www.2sic.com), the inventors of cre8magic, have been creating themes
for more than 1'000 websites since 1999.
That's a lot of experience packed together.
Our goal was to ensure that every theme we create is as good as it can be,
and that we can create it as fast as possible.

Let's take a simple example: **System-Menus**.
They usually show a bunch of links, some within the normal structure of the site,
and some taken out of the normal navigation structure just for the footer.
Typically these pages are officially "hidden" in the navigation, since we don't want them in the main menu,
but still need to be accessible.
This is how we would do it in cre8magic using the `MagicMenu` _Oqtane Bootstrap5_ component:

```html
<!-- This example assumes a separate configuration which is called "System" -->
<MagicMenu PartName="System" />

<!-- This example places all the settings directly inside the tag -->
<!-- In this case, 5 is a hidden page in the navigation, and we want to show it's children -->
<MagicMenu Settings='new() { Start = "5!/" }' />
```



explain

## Planned structure

1. Introduction (maybe inside overview? or before)
1. Overview, explaining what's in the box - basically what things it can solve
1. Get Started - step-by-step guide to get started and simple use case

1. **Acts**
1. Common Setup
1. Magic Breadcrumbs
1. Magic Languages
1. Magic Links ?
1. Magic Logos
1. Magic Menus
1. Magic Page Context
1. Magic Pages ?
1. Magic To-Top

1. **SEO & Analytics**
1. Magic Analytics
1. Magic Sitemap

1. **Razor Components**
1. Magic Themes
1. Magic "Buttons" - needs better name
1. Magic OqtaneBs5 Razor Components

1. **Foundation**
1. Magic Act
1. Magic Blueprints
1. Magic Tailors
1. Magic Spells Book
1. Magic Meta-Settings - needs better name / Magic Spells


## History

1. Added in v0.0.1 2022-10 with 80% coverage of what DDR Menu had in DNN
