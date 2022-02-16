@echo off

pushd .\UzairAli.MobileApp.HttpClientServices\bin\Release\
FOR /F "tokens=*" %%I in ('DIR *.nupkg /B /OD') DO SET NewestFile=%%I
echo Pushing "%NewestFile%" to NuGet

dotnet nuget push "%NewestFile%" --api-key oy2ht7lpt3grjn3noqkqxo4drqcekupebkk642ge3fd33q --source https://api.nuget.org/v3/index.json