#!/bin/bash

TargetFramework=$1
ProjectName=$2

cp -f "bin/Debug/$TargetFramework/$ProjectName$.Oqtane.dll" "../../oqtane.framework/Oqtane.Server/bin/Debug/$TargetFramework/"
cp -f "bin/Debug/$TargetFramework/$ProjectName$.Oqtane.pdb" "../../oqtane.framework/Oqtane.Server/bin/Debug/$TargetFramework/"
cp -rf "wwwroot/"* "../../oqtane.framework/Oqtane.Server/wwwroot/"