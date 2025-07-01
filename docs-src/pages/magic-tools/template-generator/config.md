---
uid: Cre8magic.MagicTools.TemplateGenerator.Configuration
---

<img src="./assets/logo-400.webp" class="float-right" />

# cre8magic ♾️ Template Generator - Configuration File

This explains the configuration file for the **cre8magic ♾️ Template Generator**.

The file itself is a JSON file named `template-generator.config.json`
It contains various sections to define how the template generator should behave.

## The Template Generator Configuration File

The file is called: `template-generator.config.json`

It should be located in the root of the theme **solution** folder (not the theme folder).
This is a typical repository structure:

* `C:\Projects\cre8magic\theme-basic\cre8magic-oqtane-theme-basic` (root / git-repo)
  * `Client` (the code for the theme)
    * `Containers`
    * `Resources`
    * `Themes`
    * `wwwroot` (the static files)
    * ...
  * `Package`
  * `LICENSE.md`
  * `README.md`
  * `template-generator.config.json` (this is the file)
  * `ToSic.Cre8magic.Theme.Basic.sln` (solution file)

## Example Configuration File

> [!TIP]
> The file support CJSON, so you can use comments to explain the configuration.

```json
{
  "destinationPath": "..\\..\\2Templates",
  "template": "cre8magic",
  "source": [
    {
      "include": [
        "**/*.*"
      ],
      "exclude": [
        ".git/",
        ".github/",
        ".vs/",
        "Client/",
        "Package/",
        ".gitignore",
        "**/*.DotSettings",
        "template-generator.config.json"
      ]
    },
    {
      "include": [
        "Client/**/*.*"
      ],
      "exclude": [
        "**/bin/",
        "**/node_modules/",
        "**/obj/",
        "**/package-lock.json",
        "**/*.DotSettings"
      ]
    },
    {
      "include": [
        "Package/**/*.*"
      ],
      "exclude": [
        "**/bin/",
        "**/obj/",
        "**/*.nupkg"
      ]
    }
  ],
  "rename": [
    {
      "include": [
        "**/*.*"
      ],
      "replace": {
        "ToSic.Cre8magic.Theme.Basic": "[Owner].Theme.[Theme]"
      }
    }
  ],
  "process": [
    {
      "include": [
        "**/*.sln",
        "**/*.csproj",
        "**/*.cs",
        "**/*.cshtml",
        "**/*.razor",
        "**/*.resx",
        "**/*.config",
        "**/*.xml",
        "**/*.json",
        "**/*.js",
        "**/*.map",
        "**/*.ts",
        "**/*.css",
        "**/*.sass",
        "**/*.scss",
        "**/*.html",
        "**/*.md",
        "**/*.txt",
        "**/*.nuspec",
        "**/*.bat",
        "**/*.cmd",
        "**/*.ps1",
        "**/*.sh"
      ],
      "exclude": [
        "**/styles/readme.md"
      ],
      "replace": {
        "ToSic.Cre8magic.Theme.Basic": "[Owner].Theme.[Theme]",
        "ToSic": "[Owner]",
        "Basic": "[Theme]",
        "6.1.2": "[FrameworkVersion]",
        "6.1.3": "[FrameworkVersion]"
      }
    }
  ]
}
```

## Configuration Sections Explained TODO: @STV