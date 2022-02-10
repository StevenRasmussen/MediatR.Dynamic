set version=10.0.0
dotnet build MediatR.Dynamic.csproj -c Release
nuget.exe pack MediatR.Dynamic.nuspec -OutputDirectory "..\..\..\..\LocalNugetFeed" -Version %version% -Properties Configuration=Release
