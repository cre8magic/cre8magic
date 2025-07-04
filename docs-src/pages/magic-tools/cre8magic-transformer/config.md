---
uid: Cre8magic.MagicTools.Transformer.Configuration
---

<img src="./assets/logo-400.webp" class="float-right" />

# cre8magic ♾️ Transformer - Configuration File

This explains the configuration file for the **cre8magic ♾️ Transformer**.

The file itself is a JSON file named `cre8magic-transformer.config.json`
It contains various sections to define how the transformer should behave.

## The Transformer Configuration File

The file is called: `cre8magic-transformer.config.json`

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
  * `cre8magic-transformer.config.json` (this is the file)
  * `template.json` (oqtane template metadata)
  * `ToSic.Cre8magic.Theme.Basic.sln` (solution file)

## Example Configuration File

> [!TIP]
> The file support JSONC, so you can use comments to explain the configuration.

```json
{
  "destinationPath": "..\\oqtane.framework\\Oqtane.Server\\wwwroot\\Themes\\Templates\\cre8magic",
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
        "cre8magic-transformer.config.json"
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
        "6.1.3": "[FrameworkVersion]"
      }
    }
  ]
}
```

## Configuration Sections Explained

### 1. `sourcePath`

* **Purpose:** Specifies where is theme solution source code.
* **Details:** Can be an absolute or relative path. If the `--source` is not provided as a `cre8magic ♾️ Transformer` CLI argument, it will use this as fallback value.

### 2. `destinationPath`

* **Purpose:** Specifies where the generated templates will be placed.
* **Details:** Can be an absolute or relative path. If the `--destination` is not provided as a `cre8magic ♾️ Transformer` CLI argument, it will use this as fallback value. If generating templates inside Oqtane.Server folders, ensure Oqtane is stopped to avoid conflicts.

### 3. `source`

* **Purpose:** Defines rules for selecting source files to include or exclude in the template generation.
* **Structure:** An array of rule objects, each with `include` and `exclude` file globbing patterns.

### 4. `rename`

* **Purpose:** Specifies rules for renaming files and folders during the template generation process.
* **Structure:** An array of rule objects with `include`, `exclude` file globbing patterns and a `replace` dictionary.

### 5. `process`

* **Purpose:** Defines how file contents are processed, including which files to process and what token replacements to perform.
* **Structure:** An array of rule objects with:
  * `include`: File patterns to process (e.g., `.cs`, `.csproj`, `.json`, etc.).
  * `exclude`: File patterns to skip (e.g., `readme.md`, `template.json`).
  * `replace`: Dictionary of tokens to replace with template variables (e.g., `ToSic` → `[Owner]`, `Basic` → `[Theme]`, `6.1.3` → `[FrameworkVersion]`).


**Note:**

* The configuration supports multiple rules for flexibility.
* Token replacement allows for dynamic template customization.
* Exclusion patterns help prevent unwanted modifications to certain files.


## File Globbing Pattern Formats

The patterns specified in the `include` and `exclude` arrays can use the following formats to match multiple files or directories.

### Exact Directory or File Name

**Examples:**

* `some-file.txt`
* `path/to/file.txt`

### Wildcards

Wildcards (`*`) in file and directory names represent zero to many characters, excluding separator characters.

**Examples:**

| Value         | Description                                          |
|---------------|------------------------------------------------------|
| `*.txt`       | All files with `.txt` file extension.                |
| `*.*`         | All files with an extension.                         |
| `*`           | All files in the top-level directory.                |
| `.*`          | File names beginning with `.`.                       |
| `*word*`      | All files with `word` in the filename.               |
| `readme.*`    | All files named `readme` with any file extension.    |
| `styles/*.css`| All files with extension `.css` in the directory `styles/`. |
| `scripts/*/*` | All files in `scripts/` or one level of subdirectory under `scripts/`. |
| `images*/*`   | All files in a folder with a name that is or begins with `images`. |

### Arbitrary Directory Depth (`/**/`)

**Examples:**

| Value         | Description                                           |
|---------------|-------------------------------------------------------|
| `**/*`        | All files in any subdirectory.                       |
| `dir/`        | All files in any subdirectory under `dir/`.          |
| `dir/**/*`    | All files in any subdirectory under `dir/`.          |


## Template variables

Property `replace` is dictionary of tokens to replace with `template variables`. Following `template variables` are recognized by oqtane [ThemeController](https://github.com/oqtane/oqtane.framework/blob/b24e3252d9e68071b6726344edd328c31b3d7932/Oqtane.Server/Controllers/ThemeController.cs#L254) template `ITokenReplace` engine:

* `[Owner]`
* `[Theme]`
* `[FrameworkVersion]`
* `[ClientReference]`
* `[SharedReference]`
* `[RootPath]`
* `[RootFolder]`
