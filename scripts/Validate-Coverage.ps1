param (
    [string]$Summary = ".\TestResults\Summary.txt",
    [decimal]$Minimum = 0
)

$LineCoverage = select-string -path $Summary -pattern 'Line coverage:\s*\d+.\d+' -Raw

$CodeCoverageDecimal = 0

if ($LineCoverage.Length -gt 0)
{
    $CodeCoverage = $LineCoverage.Split(' ') | Select -Last 1
    $CodeCoverage = $CodeCoverage.Split('%') | Select -First 1
    $CodeCoverageDecimal = [decimal]$CodeCoverage.Replace(",", ".")
}
Write-Host "Code Coverage: $CodeCoverageDecimal%"
Write-Host "Minimum: $Minimum%"

if ($Minimum -gt 0 -And $CodeCoverageDecimal -lt $Minimum)
{
    throw "Code Coverage of '$CodeCoverageDecimal%' doesn't meet the minimum coverage of '$Minimum%'."
}
