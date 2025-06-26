@echo off
set TargetFramework=%1
set ProjectName=%2

del "*.nupkg"
"..\..\..\oqtane.framework\oqtane.package\nuget.exe" pack %ProjectName%.nuspec -Properties targetframework=%TargetFramework%;projectname=%ProjectName%
XCOPY "*.nupkg" "..\..\..\oqtane.framework\Oqtane.Server\wwwroot\Packages\" /Y