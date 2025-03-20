@echo off
set TargetFramework=%1
set ProjectName=%2

XCOPY "bin\Debug\%TargetFramework%\%ProjectName%.Client.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\%TargetFramework%\" /Y
XCOPY "bin\Debug\%TargetFramework%\%ProjectName%.Client.Oqtane.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\%TargetFramework%\" /Y
XCOPY "bin\Debug\%TargetFramework%\ToSic.Cre8Magic.OqtaneBs5.Client.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\%TargetFramework%\" /Y
XCOPY "bin\Debug\%TargetFramework%\ToSic.Cre8Magic.OqtaneBs5.Client.Oqtane.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\%TargetFramework%\" /Y
XCOPY "..\ToSic.Cre8magic.Client\wwwroot\*" "..\..\oqtane.framework\Oqtane.Server\wwwroot\" /Y /S /I