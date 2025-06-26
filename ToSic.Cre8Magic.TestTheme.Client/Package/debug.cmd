@echo off
set TargetFramework=%1
set ProjectName=%2

call npm run dev

XCOPY "bin\Debug\%TargetFramework%\%ProjectName%.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\%TargetFramework%\" /Y
XCOPY "bin\Debug\%TargetFramework%\%ProjectName%.Oqtane.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\%TargetFramework%\" /Y
XCOPY "bin\Debug\%TargetFramework%\ToSic.Cre8magic.Client.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\%TargetFramework%\" /Y
XCOPY "bin\Debug\%TargetFramework%\ToSic.Cre8magic.Client.Oqtane.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\%TargetFramework%\" /Y
XCOPY "bin\Debug\%TargetFramework%\ToSic.Cre8Magic.OqtaneBs5.Client.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\%TargetFramework%\" /Y
XCOPY "bin\Debug\%TargetFramework%\ToSic.Cre8Magic.OqtaneBs5.Client.Oqtane.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\%TargetFramework%\" /Y
XCOPY "wwwroot\*" "..\..\oqtane.framework\Oqtane.Server\wwwroot\" /Y /S /I
