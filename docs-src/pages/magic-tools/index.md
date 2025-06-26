---
uid: Cre8magic.MagicTools.Index
---

# Magic Tools

Magic Tools will be a set of tools to better create themes for Oqtane.

## cre8magic ♾️ Theme To Template Converter

To create theme templates, you need a set of files containing a lot of placeholders.
This is difficult to develop, since it will not compile with the placeholders.

> [!TIP]
> The cre8magic Theme To Template Converter will help you convert your existing themes into templates.

So what it does is it takes an existing theme, a `.json` configuration file,
and generates all the files you need to create a theme template.

The converter is being used extensively by the cre8magic team to create new themes.

We expect to release the first version of the converter in 2025-Q3.

## How to Use the Converter

1. Create a theme any way you want, test it, develop it, etc.
1. Add a special  `template.json` file to the theme folder.
1. Run the converter, which will generate a new folder with the template files.
1. Use the generated files to create an installable NuGet package.
1. Deploy directly or through the Oqtane Marketplace.

## 