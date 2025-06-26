set CodeBinPrefix=bin\Debug\net9.0\ToSic.Theme.Cre8magicTests
set OqtanePath=..\..\..\oqtane.framework\Oqtane.Server
set CodeBinTarget=%OqtanePath%\bin\Debug\net9.0\

XCOPY "..\Client\%CodeBinPrefix%.Client.Oqtane.dll" "%CodeBinTarget%" /Y
XCOPY "..\Client\%CodeBinPrefix%.Client.Oqtane.pdb" "%CodeBinTarget%" /Y

set Cre8magicBinPrefix=..\Client\bin\Debug\net9.0\ToSic.Cre8magic.Client.Oqtane

XCOPY "%Cre8magicBinPrefix%.dll" "%CodeBinTarget%" /Y
XCOPY "%Cre8magicBinPrefix%.pdb" "%CodeBinTarget%" /Y
XCOPY "..\Client\wwwroot\*" "%OqtanePath%\wwwroot\" /Y /S /I
