---
uid: OqtaneThemes.Templates.Index
---

# Oqtane Theme Templates Guide

> [!TIP]
> Theme templates are a bunch of files with placeholders, which Oqtane can use to generate a new theme.

They are located in the `wwwroot/Themes/Templates` folder of the Oqtane installation.

## How do Theme Templates Work?

Oqtane provides functionality to generate a source code solution for a new custom theme based on the `Owner Name`, `Theme Name` input by the developer, and the selected theme template from `wwwroot/Themes/Templates/`.

Once the theme solution is generated, the developer can open it in Visual Studio. The solution includes:

- A fully functional source code for the Oqtane theme in the `Client` project.
- A `Package` project to deploy the theme in `Oqtane.Server` or package it into a `.nupkg` file. This `.nupkg` file can be used to deploy the theme to other Oqtane installations or distribute it on the Oqtane Market.

For an example of theme templates, see [`Oqtane.Server/wwwroot/Themes/Templates/External`](https://github.com/oqtane/oqtane.framework/tree/dev/Oqtane.Server/wwwroot/Themes/Templates/External). Note that this example exists only in the `oqtane.framework` source code and is not included in the Oqtane release package.

➡️ For more details, refer to the [Oqtane Themes Documentation](https://www.oqtane.org/documentation/themes).

## How to Use Them

TODO: @2dg walkthrough with images, short description

## How to Create a New Theme Template

You can create a new theme template in two ways:

1. **Using the cre8magic Transformer**: This is a tool that helps you convert an existing theme into a template.
    It allows you to specify which files to include, how to rename them, and how to replace placeholders with actual values.  
    _This is the recommended way, since it's easier, faster, more reliable and repeatable._  
    ➡️ See [](xref:Cre8magic.MagicTools.Transformer.Index)

2. **Manually**: You can create a new theme template by copying an existing one and modifying it.
    This involves creating a new folder in the `wwwroot/templates/themes` directory, copying the files from an existing template, and then modifying the files to suit your needs.

## The `template.json` File

An example of the `template.json` file can be found in the [`External`](https://github.com/oqtane/oqtane.framework/blob/dev/Oqtane.Server/wwwroot/Themes/Templates/External/template.json) theme template provided in the Oqtane source code.

```json
{
  "Title": "Default Theme Template",
  "Type": "External",
  "Version": "5.2.0",
  "Namespace": "[Owner].Theme.[Theme]"
}
```

The `template.json` file contains metadata that is used by Oqtane during runtime to display theme templates in the `Theme Management > Create Theme` screen. It is also utilized when generating the theme's source code. Each theme template must include this file.

### Properties

- **`Title`**: The name displayed in the theme template dropdown list, allowing users to select it to generate a new theme from the template.
- **`Type`**: Always set to `External`. In the past, there was also an `Internal` type, but this is no longer used. The `External` type generates a standalone source code solution outside of the `oqtane.framework` source code structure.
- **`Version`**: Specifies the required Oqtane.Framework version that the new theme depends on.
- **`Namespace`**: Provides a tokenized pattern used to generate the root namespace for the theme in the source code.

This file is essential for ensuring that the theme template integrates seamlessly with Oqtane's theme creation functionality. Themes generated from a template will include a `template.json` file in their source code.

Although the `template.json` file is not required for an Oqtane theme to function properly, developers should avoid deleting it. If the file is missing, it will cause issues later if the theme is used as a source to generate a new theme template. To address this, the [cre8magic ♾️ Transformer](xref:Cre8magic.MagicTools.Transformer.Index) tool automatically creates a `template.json` file if it is missing in the theme source.
