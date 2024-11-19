set CodeBinPrefix=bin\Debug\net8.0\ToSic.Theme.Cre8magicTests
set OqtanePath=..\..\..\oqtane.framework\Oqtane.Server
set CodeBinTarget=%OqtanePath%\bin\Debug\net9.0\

XCOPY "..\Client\%CodeBinPrefix%.Client.Oqtane.dll" "%CodeBinTarget%" /Y
XCOPY "..\Client\%CodeBinPrefix%.Client.Oqtane.pdb" "%CodeBinTarget%" /Y
XCOPY "..\Client\bin\Debug\net8.0\ToSic.Cre8magic.Client.Oqtane.dll" "%CodeBinTarget%" /Y
XCOPY "..\Client\bin\Debug\net8.0\ToSic.Cre8magic.Client.Oqtane.pdb" "%CodeBinTarget%" /Y
XCOPY "..\Client\wwwroot\*" "%OqtanePath%\wwwroot\" /Y /S /I
