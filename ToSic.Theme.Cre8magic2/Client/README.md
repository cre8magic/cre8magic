# Cre8magic2 Theme for Oqtane

## Overview
Cre8magic2 is a modern, flexible theme for the Oqtane framework. It provides a clean and responsive design that can be customized to suit various website needs.

## Features
- Fully responsive layout
- Bootstrap 5 integration
- Customizable color schemes
- Multiple layout options
- Optimized for performance
- Easy customization through theme settings

## Development
This theme is based on the Cre8magic framework for Oqtane, providing additional functionality beyond standard Oqtane themes.

Key development notes:
- This is theme development version
- Depends on `ToSic.Cre8magic.Oqtane` nuget package that is usualy in local `InstallPackages` depends on relevant `nuget.config`
    ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <configuration xmlns="http://schemas.microsoft.com/nuget/config/Settings">
        <packageSources>
            <!-- Define the local package source with a relative path -->
            <!-- <add key="LocalNuGetFeed" value="ToSic.Cre8magic.StaticAssets/bin/Release" /> -->
            <add key="LocalNuGetFeed" value="InstallPackages" />
            <!-- Include other sources as needed -->
            <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
        </packageSources>
        <packageSourceMapping>
            <packageSource key="LocalNuGetFeed">
            <package pattern="ToSic.Cre8magic.*" />
            </packageSource>
            <packageSource key="nuget.org">
            <package pattern="*" />
            </packageSource>
        </packageSourceMapping>
        <packageRestore>
            <add key="enabled" value="True" />
            <add key="automatic" value="True" />
        </packageRestore>
        <config>
            <add key="IncludeSymbols" value="true" />
            <!-- <add key="SymbolPackageFormat" value="snupkg" /> -->
        </config>
    </configuration>
    ```
- Includes all symbols (*.pdb) in the NuGet package for easier debugging

## Setup Oqtane Dev Env
1. Clone this repo in the `cre8magic` subfolder.
1. Clone the Oqtane git repo in the `oqtane.framework` subfolder.
1. Build `Oqtane.sln` in **Debug** configuration.
1. Run `Oqtane.Server.csproj` to install and set up your local Oqtane development environment.

## Deploy during development
1. Ensure correct Oqtane.Server StaticWebAssets folder path in `ToSic.Theme.Cre8magic2.Package.csproj`
    ```xml
    <OqtaneStaticAssetsPath>../../oqtane.framework/Oqtane.Server/wwwroot/_content/ToSic.Cre8magic.Oqtane</OqtaneStaticAssetsPath>
    ```
1. Build `ToSic.Theme.Cre8magic2.Package.csproj` in **Debug** configuration.
1. Navigate to your Oqtane installation's Admin Themes Management interface and ensure that this theme is installed.
1. Apply to pages as necessary.

## Package for production
1. Build `ToSic.Theme.Cre8magic2.Package.csproj` in **Release** configuration.
1. Find the Oqtane Theme Package `ToSic.Theme.Cre8magic2.N.N.N.nupkg` in the `Package` folder.
1. Install the theme through the Oqtane Admin Themes Management interface.
1. For production use, we recommend using the Release configuration package.

## Theme installation in production
1. Requirements: Oqtane Framework 6.0.1 or higher.
1. Navigate to your Oqtane installation's Admin area.
1. Go to **Admin** > **Themes** > **Upload Theme**.
1. Upload the `ToSic.Theme.Cre8magic2.N.N.N.nupkg` package.
1. Restart Oqtane to install the theme.

## License
MIT License

## Version History
- **1.0.0**: Current development version

## Contact & Support
For questions, bug reports, or feature requests, please use the GitHub issue tracker.
