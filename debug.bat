@echo off
dotnet build src/Limbo.DataAccess --configuration Release /t:rebuild /t:pack -p:BuildTools=1 -p:PackageOutputPath=../../debug/nuget