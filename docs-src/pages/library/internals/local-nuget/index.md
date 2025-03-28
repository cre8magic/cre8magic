# cre8magic - Local NuGet Package for Development

When developing a new version of cre8magic, you may need to test the result on a theme _without_ deploying it to NuGet first.
To implement this, we need these steps:

1. Create a local NuGet "feed" which Visual Studio will use
1. Build the NuGet package to test
1. Modify your test theme to use the local NuGet package

## Step 1: Create Local NuGet Feed

If you don't already have a local NuGet feed, in your `cre8magic` folder, follow these steps:

1. Create a folder named `InstallPackages` to store NuGet packages for the local feed.  
   TODO: @STV where?, maybe `c:\projects\cre8magic\InstallPackages` ???
1. Create a `nuget.config` file with the following configuration:  
   TODO: @STV where?

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

This configuration allows you to install local NuGet packages from the specified `InstallPackages` folder.
Visual Studio will find them using the `LocalNuGetFeed` source.

TODO: @STV - IS IT NOT NECESSARY TO CHANGE Visual Studio to know about this new feed?


## Step 2: Build the cre8magic NuGet Package

1. Build the `ToSic.Cre8magic.Package.csproj` project in `Release` configuration.
1. This generates a new `ToSic.Cre8magic.Oqtane` NuGet package in the `ToSic.Cre8magic.Package` folder.
1. The version of the `.nupkg` file is defined in the `ToSic.Cre8magic.Oqtane.nuspec` file.  
   Ensure you increment the version number for each new package to avoid issues with NuGet package caching.
1. Copy the new package (e.g., `ToSic.Cre8magic.Oqtane.1.0.0.nupkg`) to the `InstallPackages` folder to make it available as a dependency for your custom Oqtane theme project.



## Step 3: Modify Test Theme to Use the Local NuGet Package

To test the local NuGet package, you need a custom Oqtane theme which uses it.

> [!TIP]
> The standard cre8magic solution has such a theme named `ToSic.Cre8magic.ThemeWithLocalNuGet`.
>
> So if you are just working in the cre8magic project, you can use that project to test everything.

TODO: @STV please make sure we have this test-setup in the cre8magic solution.

Say you have generated a new custom theme named `ToSic.Theme.Test1`.
This results in a folder structure with the folders `Client` and `Package`, containing the `ToSic.Theme.Test1.Client.csproj` and `ToSic.Theme.Test1.Package.csproj` projects.

### 3.1. Update the Client Project

Open the `ToSic.Theme.Test1.Client.csproj` file to make some changes.

1. Add the following line to the end of the first `<PropertyGroup>` (TODO: @STV why, what does this do?):

  ```xml
  <OqtaneStaticAssetsPath>../../oqtane.framework/Oqtane.Server/wwwroot/_content/ToSic.Cre8magic.Oqtane</OqtaneStaticAssetsPath>
  ```

1. Add the following `<PackageReference>` to the `<ItemGroup>` containing other packages:

  ```xml
  <PackageReference Include="ToSic.Cre8magic.Oqtane" Version="1.0.0" GeneratePathProperty="true" />
  ```

1. Add a build task to copy static web assets:

  ```xml
  <Target Name="CopyStaticWebAssets" AfterTargets="Build">
    <ItemGroup>
      <StaticAssets Include="$(PkgToSic_Cre8magic_Oqtane)\staticwebassets\**\*" />
    </ItemGroup>
    <MakeDir Directories="../$(OqtaneStaticAssetsPath)" />
    <Copy SourceFiles="@(StaticAssets)"
          DestinationFiles="@(StaticAssets->'../$(OqtaneStaticAssetsPath)/%(RecursiveDir)%(Filename)%(Extension)')"
          SkipUnchangedFiles="true" />
  </Target>
  ```

### 3.2. Update the Debug Script

Open the `Package/debug.cmd` file and dd the following lines to copy the DLLs to the Oqtane development environment:

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

With these changes, your custom theme will use the local NuGet package and include the necessary static web assets for testing.

### 3.4. Build and Verify that it Works

TODO: @STV how can you tell that it worked?

### 3.5. Test an Updated Version

TODO: @STV what must we do when we want to rebuild and make sure it uses the newer version? I assume we need to bump the version and change the dependency in the client project?