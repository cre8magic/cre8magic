# cre8magic - JavaScript Resources Explained

> [!TIP]
> This explains how JavaScript are Bundled and Distributed for use in NuGet,
> and how they are used in Themes or in the internal cre8magic ♾️ APIs.
>
> This document is meant for the people developing cre8magic.
> It is not meant for theme developers.

Themes which use cre8magic must also include the necessary javascript resources.
This is quite difficult to achieve, since the JS is in the NuGet package of the library.
Making sure that they are included when your theme is distributed requires quite a bit of work.

## Final Result at Runtime

At runtime, all the files must be located in `/wwwroot/_content/ToSic.Cre8magic.Oqtane/`.
As of now, there are two files:

* `interop.js` - this will be called by the theme for various operations such as adjusting classes on the body
* `ambient.js` - this will be run by the browser to be available all the time (ambient)

Commands on these files are called from Oqtane using the [](xref:ToSic.Cre8magic.Themes.IMagicThemeJsService).

## Final Scenario

> [!NOTE]
> In the end, a theme developer should be able to just use cre8magic,
> and the necessary JS should be included in the theme.
>
> In addition, the makers of cre8magic should be able to work on the JS files
> in an easy, efficient manner.

So we have three goals:

1. the final theme must include the necessary JS files in the `wwwroot` folder of the theme, specifically in the folder `wwwroot/_content/ToSic.Cre8magic.Oqtane/`
1. during development of cre8magic, the JS files must be deployed to the same folder as the development Oqtane
1. while working on the JS files, every change should be auto-updated to the dev Oqtane without a restart of Oqtane itself

## How this Works

TODO: @STV pls explain

* how the sources project works, etc.
* how changes to the source files go to the dev Oqtane
* how the dist files are picked up by the packager and placed in the NuGet package
* how the `_content` is a magic folder and what it's for (pls also include links to the MS docs)
