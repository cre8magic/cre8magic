---
uid: OqtaneThemes.Templates.Index
---

# Oqtane Theme Templates Guide

Theme templates are a bunch of files with placeholders, which Oqtane can use to generate a new theme.

They are located in the `wwwroot/templates/themes` folder of the Oqtane installation.

## How do Theme Templates Work?

TODO: @STV short explanation of how theme templates work, with a link to 

## How to Use Them

TODO: @2dg walkthrough with images, short description

## How to Create a New Theme Template

You can create a new theme template in two ways:

1. **Using the Oqtane Template Generator**: This is a tool that helps you convert an existing theme into a template.
    It allows you to specify which files to include, how to rename them, and how to replace placeholders with actual values.  
    _This is the recommended way, since it's easier, faster, more reliable and repeatable._  
    ➡️ See [](xref:Cre8magic.MagicTools.TemplateGenerator.Index)

2. **Manually**: You can create a new theme template by copying an existing one and modifying it.
    This involves creating a new folder in the `wwwroot/templates/themes` directory, copying the files from an existing template, and then modifying the files to suit your needs.

## The `template.json` File

TODO: @STV explain the template.json file, which is used by Oqtane to describe the Template to the user.
