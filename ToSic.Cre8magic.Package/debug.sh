#!/bin/bash

TargetFramework=$1
ProjectName=$2

cp -f "../ToSic.Cre8magic.Client/bin/Debug/$TargetFramework/$ProjectName$.Client.Oqtane.dll" "../../oqtane.framework/Oqtane.Server/bin/Debug/$TargetFramework/"
cp -f "../ToSic.Cre8magic.Client/bin/Debug/$TargetFramework/$ProjectName$.Client.Oqtane.pdb" "../../oqtane.framework/Oqtane.Server/bin/Debug/$TargetFramework/"
cp -rf "../ToSic.Cre8magic.Client/wwwroot/"* "../../oqtane.framework/Oqtane.Server/wwwroot/"