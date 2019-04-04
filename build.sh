#!/bin/sh

if [ -z "$1" ] 
then
  echo No version specified
  exit 1
else
  echo version $1
fi

echo Prepare build environment
if [ -d "publish" ] 
then
  rm -Rf publish; 
fi

echo Restore solution
dotnet restore src/docson.csproj

echo Building solution
dotnet publish src/docson.csproj -c Release -o ./../publish/

echo Building docker image tomware/docson:$1
#echo $PWD
docker build --build-arg source=publish -t tomware/docson:$1 .

#echo Cleaning up
#if [ -d "publish" ] 
#then
#  rm -Rf publish; 
#fi

echo Done
docker images