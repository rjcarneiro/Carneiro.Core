param (
    [string[]]$Assemblies = $(throw "-Assemblies is required."),
    [string]$Version = $(throw "-Version is required."),
    [string]$Configuration = "Release",
    [string]$Path = ".\dist\Tests"
)

Write-Host Path: $Path
Write-Host Configuration: $Configuration
Write-Host Version: $Version
Write-Host AppSettings: $AppSettings

# publish assembly
foreach ($Assembly in $Assemblies)
{
    Write-Host Publishing $Assembly
    dotnet publish --no-build ./tests/$Assembly/$Assembly.csproj -c $Configuration -o $Path --nologo 3>&1 2>&1 $null
    if ($lastexitcode -ne 0) {
        exit $lastexitcode
    }
}

# clean up
Remove-Item $Path\appsettings* -Exclude appsettings.json -Recurse -Force -ErrorAction Ignore
Remove-Item $Path\*Development.json -Recurse -Force -ErrorAction Ignore
Remove-Item $Path\runtimes -Force -ErrorAction Ignore -Recurse
Copy-Item coverlet.runsettings -Destination $Path
