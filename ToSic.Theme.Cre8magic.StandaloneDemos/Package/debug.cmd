@echo off
set TargetFramework=%1
set ProjectName=%2

set CodeBinPrefix=bin\Debug\%TargetFramework%\%ProjectName%
set OqtanePath=..\..\..\oqtane.framework\Oqtane.Server
set CodeBinTarget=%OqtanePath%\bin\Debug\%TargetFramework%\

XCOPY "..\Client\%CodeBinPrefix%.Client.Oqtane.dll" "%CodeBinTarget%" /Y
XCOPY "..\Client\%CodeBinPrefix%.Client.Oqtane.pdb" "%CodeBinTarget%" /Y
XCOPY "..\Client\wwwroot\*" "%OqtanePath%\wwwroot\" /Y /S /I