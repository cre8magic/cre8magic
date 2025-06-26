del "*.nupkg"
"..\..\..\oqtane.framework\oqtane.package\nuget.exe" pack ToSic.Module.PageTemplates.nuspec 
XCOPY "*.nupkg" "..\..\..\oqtane.framework\Oqtane.Server\Packages\" /Y

