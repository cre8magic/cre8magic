# cre8magic - Local NuGet Package for Development

When developing a new version of cre8magic, you may need to test the result on a theme _without_ deploying it to NuGet first.
To implement this, we need these steps:

1. Create a local NuGet "feed" which Visual Studio will use
1. Build the NuGet package to test
1. Modify your test theme to use the local NuGet package

## Step 1: Create Local NuGet Feed

If you don't already have a local NuGet feed, in your `cre8magic` folder, follow these steps:

1. In your solution root (e.g., `c:\projects\cre8magic\`), create a folder named `InstallPackages` to store NuGet packages for the local feed.
1. Also, in your solution root, create a `nuget.config` file with the following configuration:  

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration xmlns="http://schemas.microsoft.com/nuget/config/Settings">
  <packageSources>
    <!-- Define the local package source with a relative path -->
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
  </config>
</configuration>
```

This configuration allows you to install local NuGet packages from the specified `InstallPackages` folder. Visual Studio will find them using the `LocalNuGetFeed` source.

> [!TIP]
> If you want to confirm/override a feed, you can still go into **Visual Studio**: `Tools > Options > NuGet Package Manager > Package Sources` and check if your local feed is listed, but by default, Visual Studio will automatically discover and honor the feeds defined in a nuget.config located in solution root.

## Step 2: Build the cre8magic NuGet Package

1. Ensure you increment the version number for each new package to avoid issues with NuGet package caching. The version of the `.nupkg` file is defined in the `ToSic.Cre8magic.Package/ToSic.Cre8magic.Oqtane.nuspec` file.
1. Build the `ToSic.Cre8magic.Package/ToSic.Cre8magic.Package.csproj` project in `Release` configuration.
1. This generates a new `ToSic.Cre8magic.Oqtane` NuGet package in the `ToSic.Cre8magic.Package` folder.
1. Copy the new package (e.g., `ToSic.Cre8magic.Oqtane.N.N.N.nupkg`) to the `InstallPackages` folder to make it available as a dependency for your custom Oqtane theme project.

> [!IMPORTANT]
> Always increment the package version number in the `.nuspec` file when making changes to your package. NuGet uses a complex caching mechanism, and if you rebuild a package with the same version number, Visual Studio might continue using the previously cached version rather than your new build.

## Step 3: Modify Test Theme to Use the Local NuGet Package

To test the local NuGet package, you need a custom Oqtane theme which uses it.

> [!TIP]
> The standard cre8magic solution has such a theme named `ToSic.Cre8magic.ThemeWithLocalNuGet`.
>
> So if you are just working in the cre8magic project, you can use that project to test everything.

Say you have generated a new custom theme named `ToSic.Theme.Test1`.
This results in a folder structure with the folders `Client` and `Package`, containing the `ToSic.Theme.Test1.Client.csproj` and `ToSic.Theme.Test1.Package.csproj` projects.

### 3.1. Update the Client Project

Open the `ToSic.Theme.Test1.Client.csproj` file to make some changes.

1. Add the following line to the end of the first `<PropertyGroup>`:

    ```xml
    <OqtaneStaticAssetsPath>../../../oqtane.framework/Oqtane.Server/wwwroot/_content/ToSic.Cre8magic.Oqtane</OqtaneStaticAssetsPath>
    ```

    > [!NOTE]
    > `OqtaneStaticAssetsPath` property specifies a relative folder path where cre8magic shared static web assets (like CSS, JavaScript, images, etc.) from the `ToSic.Cre8magic.Oqtane` package will be copied during the build process.
    
    > [!TIP]
    > In ASP.NET Core, the `wwwroot/_content/[packageid]` folder structure is a convention used to serve static assets from Razor Class Libraries. The `_content` folder is a special directory allowing components from different libraries to coexist without file path conflicts. 

1. Add the following `<PackageReference>` to the `<ItemGroup>` containing other packages, and replace `Version="N.N.N"` with latest version, eg. `Version="1.0.0"`:

    ```xml
    <PackageReference Include="ToSic.Cre8magic.Oqtane" Version="N.N.N" GeneratePathProperty="true" />
    ```
  
    > [!TIP]
    > Attribute `Version="N.N.N"` with the expected version of the `ToSic.Cre8magic.Oqtane` package (located in your local `InstallPackages` folder) is optional. If multiple versions of the same package exist in your local NuGet folder and you don't specify a version, NuGet will use the highest version by default.

    > [!TIP]
    > The `GeneratePathProperty="true"` attribute in the `PackageReference` element is a special MSBuild property that instructs the build system to create an MSBuild property containing the installation path of the referenced NuGet package, so it can be used later when necessary.

1. Add a build task to copy static web assets:

    ```xml
    <Target Name="CopyStaticWebAssets" AfterTargets="Build">
      <ItemGroup>
        <StaticAssets Include="$(PkgToSic_Cre8magic_Oqtane)\staticwebassets\**\*" />
      </ItemGroup>
      <MakeDir Directories="$(OqtaneStaticAssetsPath)" />
      <Copy SourceFiles="@(StaticAssets)"
            DestinationFiles="@(StaticAssets->'$(OqtaneStaticAssetsPath)/%(RecursiveDir)%(Filename)%(Extension)')"
            SkipUnchangedFiles="true" />
    </Target>
    ```

    > [!NOTE]
    > For Oqtane themes, this build task ensures that cre8magic shared static web assets (like CSS, JavaScript, images, etc.) from the `ToSic.Cre8magic.Oqtane` package are copied during the build process, which is essential for proper theme rendering.

### 3.2. Update the Debug Script

Open the `Package/debug.cmd` file and add the following lines to copy the DLLs to the Oqtane development environment:

```cmd
XCOPY "..\Client\bin\Debug\%TargetFramework%\ToSic.Cre8magic.Client.Oqtane.dll" "..\..\..\oqtane.framework\Oqtane.Server\bin\Debug\%TargetFramework%\" /Y
XCOPY "..\Client\bin\Debug\%TargetFramework%\ToSic.Cre8Magic.OqtaneBs5.Client.Oqtane.dll" "..\..\..\oqtane.framework\Oqtane.Server\bin\Debug\%TargetFramework%\" /Y
```

### 3.3. Update the NuSpec File

Open the `Package/ToSic.Theme.Test1.nuspec` file and add the following lines to include the static web assets in the package:

```xml
<file src="..\Client\bin\Release\$targetframework$\ToSic.Cre8magic.Client.Oqtane.dll" target="lib\$targetframework$" />
<file src="..\Client\bin\Release\$targetframework$\ToSic.Cre8Magic.OqtaneBs5.Client.Oqtane.dll" target="lib\$targetframework$" />
<file src="..\..\..\oqtane.framework\Oqtane.Server\wwwroot\_content\ToSic.Cre8magic.Oqtane\**\*.*" target="wwwroot\_content\ToSic.Cre8magic.Oqtane" />
```

> [!NOTE]
> With these changes, your custom theme will use the local NuGet package and include the necessary static web assets for testing.

### 3.4. Build and Verify that it Works in your Oqtane development environment

When you select **Debug** configuration and you build the *Package* project, it automatically deploys your Oqtane theme to your Oqtane Server development environment:
1. Select **Debug** configuration in Visual Studio.
1. Build the *Package* project (e.g., `ToSic.Theme.Test1.Package.csproj`).
1. The build process runs the `debug.cmd` script, which copies relevant files to your Oqtane development environment.
1. Theme-related DLL files, `ToSic.Cre8magic.Client.Oqtane.dll` and `ToSic.Cre8Magic.OqtaneBs5.Client.Oqtane.dll` will be placed in the `oqtane\oqtane.framework\Oqtane.Server\bin\Debug\net9.0` folder.
1. Theme static assets will be copied to the `oqtane\oqtane.framework\Oqtane.Server\wwwroot\Themes\ToSic.Theme.Test1` folder and `oqtane\oqtane.framework\Oqtane.Server\wwwroot\_content\ToSic.Cre8magic.Oqtane` folder.

To verify it works:
1. Run the Oqtane development environment (`Oqtane.Server` project) in **Debug** configuration in Visual Studio.
1. Oqtane will automatically detect and install your theme during startup.
1. Log in to your Oqtane admin account.
1. Navigate to any page and access its `Page Management` settings.
1. Go to the `Appearance` section.
1. Your theme should be available in the dropdown list of themes and containers.
1. Select your theme and verify it renders correctly.

> [!NOTE]
> If the theme doesn't appear in the list or doesn't render correctly, check the theme's *Package* project **Build** log in Visual Studio's **Output** window.

### 3.5. Test an Updated Version in your Oqtane development environment

After creating a new version of `ToSic.Cre8magic.Oqtane.M.M.M.nupkg` package you need to update your theme to use it:

1. Ensure you have the new version of `ToSic.Cre8magic.Oqtane.M.M.M.nupkg` package in `InstallPackages`.
1. Open the `ToSic.Theme.Test1.Client.csproj` file and update the version to the new one:
    ```xml
    <PackageReference Include="ToSic.Cre8magic.Oqtane" Version="M.M.M" GeneratePathProperty="true" />
    ```
1. Select **Debug** configuration in Visual Studio if it is not already selected.
1. **Rebuild** the *Package* project (e.g., `ToSic.Theme.Test1.Package.csproj`).
1. The updated files will automatically deploy to your Oqtane Server development environment.
1. Run the Oqtane development environment (`Oqtane.Server` project).
1. Navigate to a page using your theme and verify the changes are visible.

> [!NOTE]
> If your changes don't appear:
> - Make sure your browser isn't caching the page (try a hard refresh with Ctrl+F5)
> - Verify that you increased the version of `ToSic.Cre8magic.Oqtane.nupkg` and that the correct one is in the `InstallPackages` folder
> - Examine the theme's *Package* project **Build** log in Visual Studio's **Output** window for any errors

### 3.6. Build and Test in a Standalone Oqtane Installation

To create a deployable package for testing in a production-like environment:

1. Ensure you have new version of `ToSic.Cre8magic.Oqtane.M.M.M.nupkg` package in `InstallPackages`.
1. Open the `ToSic.Theme.Test1.Client.csproj` file and update version to new one:
    ```xml
    <PackageReference Include="ToSic.Cre8magic.Oqtane" Version="M.M.M" GeneratePathProperty="true" />
    ```
1. Select **Release** configuration in Visual Studio
1. Ensure you've incremented the version in `ToSic.Theme.Test1.nuspec` (in `Package` folder) if making changes
1. Build the *Package* project (e.g., `ToSic.Theme.Test1.Package.csproj`)
1. The build process runs `release.cmd`, which creates a NuGet package
1. The resulting theme package (e.g., `ToSic.Theme.Test1.N.N.N.nupkg`) will be in the `Package` folder

To install and test your theme in a standalone Oqtane installation:

1. Copy the `ToSic.Theme.Test1.N.N.N.nupkg` file to your Oqtane server's `Packages` folder, or use the **Admin Dashboard > Theme Management > Install Theme** option
1. In Oqtane, go to **Admin Dashboard > System Info** and click **Restart Application**
1. After restart, verify the theme was installed in **Admin Dashboard > Theme Management**
1. Navigate to any page and open its **Page Management** settings
1. In the **Appearance** section, your theme should be available in the dropdown list
1. Select your theme and verify it renders correctly

> [!NOTE]
> If your theme doesn't appear or doesn't render properly:
> - Clear browser cache (Ctrl+F5)
> - Check Oqtane logs in the `Packages` and `wwwroot/Logs` folders
> - Verify the package was properly created by examining it with a tool like NuGet Package Explorer
> - Ensure all required dependencies are available in the target environment
