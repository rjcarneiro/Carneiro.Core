param (
    [string]$ApiKey = $(throw "-Key is required."),
    [string]$Path = ".\dist\NuGets",
    [string]$Source = "https://api.nuget.org/v3/index.json"
)

$NuGetFiles = Get-ChildItem -Path $Path -Filter *.nupkg -Name

Write-Host "-----------------"
Write-Host "About to push $(($NuGetFiles | Measure-Object).Count) NuGet packages into $Source"
Write-Host "-----------------"

foreach ($NuGet in $NuGetFiles)
{ 
    Write-Host "   => Pushing $NuGet";
    nuget push $Path/$NuGet -ApiKey $ApiKey -Source $Source
}
