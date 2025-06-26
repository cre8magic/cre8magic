set CodeBinPrefix=bin\Debug\net9.0\ToSic.Cre8magic.Seo
set OqtanePath=..\..\oqtane.framework\Oqtane.Server
set CodeBinTarget=%OqtanePath%\bin\Debug\net9.0\

XCOPY "..\ToSic.Cre8magic.Seo.Client\%CodeBinPrefix%.Client.Oqtane.dll" "%CodeBinTarget%" /Y
XCOPY "..\ToSic.Cre8magic.Seo.Client\%CodeBinPrefix%.Client.Oqtane.pdb" "%CodeBinTarget%" /Y
XCOPY "..\ToSic.Cre8magic.Seo.Server\%CodeBinPrefix%.Server.Oqtane.dll" "%CodeBinTarget%" /Y
XCOPY "..\ToSic.Cre8magic.Seo.Server\%CodeBinPrefix%.Server.Oqtane.pdb" "%CodeBinTarget%" /Y
XCOPY "..\ToSic.Cre8magic.Seo.Shared\%CodeBinPrefix%.Shared.Oqtane.dll" "%CodeBinTarget%" /Y
XCOPY "..\ToSic.Cre8magic.Seo.Shared\%CodeBinPrefix%.Shared.Oqtane.pdb" "%CodeBinTarget%" /Y
XCOPY "..\ToSic.Cre8magic.Seo.Server\wwwroot\*" "%OqtanePath%\wwwroot\" /Y /S /I
