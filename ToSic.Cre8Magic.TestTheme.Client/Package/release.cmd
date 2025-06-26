@echo off
set TargetFramework=%1
set ProjectName=%2

call npm run build

cd Package
del "*.nupkg"
"..\..\..\oqtane.framework\oqtane.package\nuget.exe" pack %ProjectName%.nuspec -Properties targetframework=%TargetFramework%;projectname=%ProjectName%
XCOPY "*.nupkg" "..\..\..\oqtane.framework\Oqtane.Server\wwwroot\Packages\" /Y
cd ..\
