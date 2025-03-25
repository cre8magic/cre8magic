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

The cre8magic JavaScript resources follow a structured development, build, and distribution workflow:

### JavaScript Source

* Source JavaScript files live in the `src/scripts` directory of the cre8magic project
* Source files are organized by feature/functionality (interop, ambient, shared)
* TypeScript is used for type safety during development
* Use ES modules (ESM) format
  * `src/scripts/ambient/index.ts` - file automatically imports all TypeScript modules in the ambient directory to bundle them together for `ambient.js` library, intentionally avoiding tree-shaking.
  * `src/scripts/interop/interops.ts` - re-exports all interop functions from their respective modules
  * `src/shared/...` - folder contains code that's reused across different parts of the cre8magic JavaScript implementation.
* We use a modern JS build system [Vite](https://vite.dev/) to bundle and optimize cre8magic JavaScript for development or production

### Development Workflow

* `ToSic.Cre8magic.StaticWebAssets.csproj` - project is configured to automatically install Node.js dependencies during the build process. This ensures that all required dependencies are installed, reducing the risk of build failures due to missing packages.
* The `package.json` file defines npm scripts to manage the JavaScript build and development workflow for cre8magic:
  * `clean`: Deletes the `dist` directory, ensuring a clean build environment.
  * `build`: Builds assets (bundled JS, CSS files) in the `dist` folder for production (also copies them to the Oqtane `wwwroot/_content` folder).
  * `serve`: Runs `vite build --watch` to continuously rebuild the JavaScript files when changes are detected, enabling a live development workflow.
  * `pack`: Executes the build script to prepare the JavaScript files and other assets for packaging into the NuGet package.
* To streamline the build and deployment process, **Visual Studio** project commands: **Clean**, **Build**, **Startup**, and **Publish** run appropriate npm scripts as defined within `ToSic.Cre8magic.StaticWebAssets.csproj`.
* During development, we use a file watcher (`npm run serve`) that:
  * Detects changes to source JavaScript files
  * Automatically rebuilds bundles in the `dist` folder
  * Copies the updated files to the development Oqtane instance's appropriate folder under `wwwroot/_content`
* Another option is to **Build** the `ToSic.Cre8magic.Package.csproj` project in `Debug` configuration, as this will build and deploy the entire cre8magic library to the Oqtane development environment.

### NuGet Package

* The purpose of the `ToSic.Cre8magic.Oqtane` NuGet package is to redistribute the cre8magic library so it can be reused in custom Oqtane themes.
* **Build** the `ToSic.Cre8magic.Package.csproj` project in `Release` configuration to build and package the cre8magic library into the `ToSic.Cre8magic.Oqtane` NuGet package. Static web assets are included as part of it.
* The build process runs a production JS build (`npm run build`) to generate optimized JS bundles in the `dist` directory.
* Then `release.cmd` is executed to handle generating the new NuGet package.
* JS bundles and other assets from the `dist` folder are included in the NuGet package in the special `staticwebassets` folder, as defined in the `ToSic.Cre8magic.Oqtane.nuspec` configuration file.
* The new `ToSic.Cre8magic.Oqtane` NuGet package file is generated in the `ToSic.Cre8magic.Package` folder, ready to be published to nuget.org (or copied to a local NuGet feed).

### The `_content` Folder

* The `_content` folder is a special convention in ASP.NET Core for static web assets.
* When a NuGet package is referenced, its static assets are automatically made available at `wwwroot/_content/{PackageId}/`.
* This is part of ASP.NET Core's static web assets feature, so at runtime, ASP.NET Core middleware serves these files from the correct location. This ensures developers don't need to manually copy JS files, as they're automatically available when referencing the cre8magic NuGet package. See more:
  * [Create an RCL with static assets in the wwwroot folder](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/class-libraries?view=aspnetcore-9.0&tabs=visual-studio#create-an-rcl-with-static-assets-in-the-wwwroot-folder)
  * [Consume content from a referenced RCL](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/ui-class?view=aspnetcore-9.0&tabs=visual-studio#consume-content-from-a-referenced-rcl)
* Unfortunately, this does not work as expected in Oqtane, since Oqtane has special handling of NuGet packages. Therefore, we need to manually add MSBuild tasks to achieve the same functionality.




