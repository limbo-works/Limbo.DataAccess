@echo off
dotnet build src/Limbo.EntityFramework --configuration Debug /t:rebuild /t:pack -p:BuildTools=1 -p:PackageOutputPath=../../debug/nuget