del "*.nupkg"
"..\..\oqtane.framework\oqtane.package\nuget.exe" pack ToSic.Module.Cre8magicTests.nuspec 
XCOPY "*.nupkg" "..\..\oqtane.framework\Oqtane.Server\Packages\" /Y

