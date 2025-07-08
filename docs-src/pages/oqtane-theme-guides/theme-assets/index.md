---
uid: OqtaneThemes.ThemeAssets.Index
---

# Oqtane Blazor Theme Assets

Every theme needs assets, such as images, stylesheets, and scripts.
This guide will help you understand how to manage and use assets in your Oqtane themes effectively.

> [!TIP]
> A core aspect of modern css and javascript is the use of SASS and TypeScript.
> This guide will help you understand how to use these technologies in your Oqtane themes.

## All About Theme Assets

Each theme in Oqtane can define its own set of assets such as stylesheets (CSS/SASS), images, and JavaScript files. These assets are essential for customizing the look and feel of a theme, as well as for adding interactive behavior through JavaScript or TypeScript.

Organizing theme assets properly ensures a modular and maintainable theme structure that is easy to develop, test, and update.

## Stylesheets using CSS / SASS

To customize the appearance of your theme, you can use traditional CSS or the more powerful and maintainable SASS preprocessor. SASS allows you to use variables, nested rules, mixins, and more—making your styles more modular and easier to manage.

For more details on working with SASS in Oqtane themes, see the following guide:  
[More About Styles](xref:OqtaneThemes.ThemeAssets.Styles.Index)

## JavaScript using TypeScript

If your theme requires dynamic behavior or interaction, you can include JavaScript.  
[More About TypeScript](xref:OqtaneThemes.ThemeAssets.Typescript.Index)

## Running Vite to Compile Styles and Scripts

Vite is a fast modern build tool used in Oqtane themes to compile SASS (into CSS) and TypeScript (into JavaScript). It provides a smooth development experience with instant hot module replacement, and a fast production build.  
[More About Vite](xref:OqtaneThemes.ThemeAssets.Vite.Index)