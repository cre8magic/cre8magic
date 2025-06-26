
set CodeBinPrefix=bin\Debug\net9.0\ToSic.Module.Cre8magicTests
set OqtanePath=..\..\..\oqtane.framework\Oqtane.Server
set CodeBinTarget=%OqtanePath%\bin\Debug\net9.0\

XCOPY "..\Client\%CodeBinPrefix%.Client.Oqtane.dll" "%CodeBinTarget%" /Y
XCOPY "..\Client\%CodeBinPrefix%.Client.Oqtane.pdb" "%CodeBinTarget%" /Y
XCOPY "..\Server\%CodeBinPrefix%.Server.Oqtane.dll" "%CodeBinTarget%" /Y
XCOPY "..\Server\%CodeBinPrefix%.Server.Oqtane.pdb" "%CodeBinTarget%" /Y
XCOPY "..\Shared\%CodeBinPrefix%.Shared.Oqtane.dll" "%CodeBinTarget%" /Y
XCOPY "..\Shared\%CodeBinPrefix%.Shared.Oqtane.pdb" "%CodeBinTarget%" /Y
XCOPY "..\Server\wwwroot\*" "%OqtanePath%\wwwroot\" /Y /S /I
