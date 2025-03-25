# cre8magic - Local NuGet Package for Development

When developing a new version of cre8magic, you may need to test the result on a theme without deploying it to NuGet first.

### Step 1: Create a Local NuGet Feed

If you don't already have a local NuGet feed, in your `cre8magic` folder, follow these steps:

1. Create a folder named `InstallPackages` to store NuGet packages for the local feed.
1. Create a `nuget.config` file with the following configuration:

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

This configuration allows you to install local NuGet packages from the `InstallPackages` folder using the `LocalNuGetFeed` source in Visual Studio.

### Step 2: Build the NuGet Package

1. Build the `ToSic.Cre8magic.Package.csproj` project in `Release` configuration.
1. This generates a new `ToSic.Cre8magic.Oqtane` NuGet package in the `ToSic.Cre8magic.Package` folder.
1. The version of the `.nupkg` file is defined in the `ToSic.Cre8magic.Oqtane.nuspec` file. Ensure you increment the version number for each new package to avoid issues with NuGet package caching.
1. Copy the new package (e.g., `ToSic.Cre8magic.Oqtane.1.0.0.nupkg`) to the `InstallPackages` folder to make it available as a dependency for your custom Oqtane theme project.

### Step 3: Modify a Test Theme to Use the Local NuGet Package

To test the local NuGet package, you need a custom Oqtane theme. For example, let's say you have generated a new custom theme named `ToSic.Theme.Test1`. This results in a folder structure with `Client` and `Package` subfolders containing `ToSic.Theme.Test1.Client.csproj` and `ToSic.Theme.Test1.Package.csproj` projects, respectively.

1. **Update the Client Project**
   - Open the `ToSic.Theme.Test1.Client.csproj` file.
   - Add the following line to the end of the first `<PropertyGroup>`:
     ```xml
     <OqtaneStaticAssetsPath>../../oqtane.framework/Oqtane.Server/wwwroot/_content/ToSic.Cre8magic.Oqtane</OqtaneStaticAssetsPath>
     ```
   - Add the following `<PackageReference>` to the `<ItemGroup>` containing other packages:
     ```xml
     <PackageReference Include="ToSic.Cre8magic.Oqtane" Version="1.0.0" GeneratePathProperty="true" />
     ```
   - Add a build task to copy static web assets:
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

1. **Update the Debug Script**
   - Open the `Package/debug.cmd` file.
   - Add the following lines to copy the DLLs to the Oqtane development environment:
     ```cmd
     XCOPY "..\Client\bin\Debug\%TargetFramework%\ToSic.Cre8magic.Client.Oqtane.dll" "..\..\..\oqtane.framework\Oqtane.Server\bin\Debug\%TargetFramework%\" /Y
     XCOPY "..\Client\bin\Debug\%TargetFramework%\ToSic.Cre8Magic.OqtaneBs5.Client.Oqtane.dll" "..\..\..\oqtane.framework\Oqtane.Server\bin\Debug\%TargetFramework%\" /Y
     ```

1. **Update the NuSpec File**
   - Open the `Package/ToSic.Theme.Test1.nuspec` file.
   - Add the following lines to include the static web assets in the package:
     ```xml
     <file src="..\Client\bin\Release\$targetframework$\ToSic.Cre8magic.Client.Oqtane.dll" target="lib\$targetframework$" />
     <file src="..\Client\bin\Release\$targetframework$\ToSic.Cre8Magic.OqtaneBs5.Client.Oqtane.dll" target="lib\$targetframework$" />
     <file src="..\..\..\oqtane.framework\Oqtane.Server\wwwroot\_content\ToSic.Cre8magic.Oqtane\**\*.*" target="wwwroot\_content\ToSic.Cre8magic.Oqtane" />
     ```

With these changes, your custom theme will use the local NuGet package and include the necessary static web assets for testing.

