param (
    [string]$Version = $(throw "-Version is required."),
    [string]$Configuration = "Release",
    [string]$Path = ".\dist\NuGets"
)

Write-Host Path: $Path
Write-Host Configuration: $Configuration
Write-Host Version: $Version

if (Test-Path $Path)
{
    Write-Output "Clear folder '$Path'"
    Remove-Item -Path $Path\ -Recurse
}

dotnet restore ./src/Carneiro.Core.sln --nologo 3>&1 2>&1 $null
dotnet build ./src/Carneiro.Core.sln -c $Configuration -p:Version=$Version --nologo 3>&1 2>&1 $null
# entities
dotnet pack ./src/Carneiro.Core.Entities/Carneiro.Core.Entities.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null
dotnet pack ./src/Carneiro.Core.Entities.Abstractions/Carneiro.Core.Entities.Abstractions.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null

# generic
dotnet pack ./src/Carneiro.Core.Exceptions/Carneiro.Core.Exceptions.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null
dotnet pack ./src/Carneiro.Core.Extensions/Carneiro.Core.Extensions.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null

# more generic
dotnet pack ./src/Carneiro.Core.Health/Carneiro.Core.Health.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null
dotnet pack ./src/Carneiro.Core.Host/Carneiro.Core.Host.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null

# json
dotnet pack ./src/Carneiro.Core.Json/Carneiro.Core.Json.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null

# logging
dotnet pack ./src/Carneiro.Core.Logging/Carneiro.Core.Logging.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null

# data access
dotnet pack ./src/Carneiro.Core.Repository.Abstractions/Carneiro.Core.Repository.Abstractions.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null
dotnet pack ./src/Carneiro.Core.Repository/Carneiro.Core.Repository.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null

# tests
dotnet pack ./src/Carneiro.Core.Tests.Abstractions/Carneiro.Core.Tests.Abstractions.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null
dotnet pack ./src/Carneiro.Core.Tests/Carneiro.Core.Tests.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null

# utils
dotnet pack ./src/Carneiro.Core.Utils.Abstractions/Carneiro.Core.Utils.Abstractions.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null
dotnet pack ./src/Carneiro.Core.Utils/Carneiro.Core.Utils.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null

# web
dotnet pack ./src/Carneiro.Core.Web/Carneiro.Core.Web.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null
dotnet pack ./src/Carneiro.Core.Web.Abstractions/Carneiro.Core.Web.Abstractions.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null
dotnet pack ./src/Carneiro.Core.Web.Tests/Carneiro.Core.Web.Tests.csproj -c $Configuration -p:Version=$Version --no-restore --nologo -o $Path -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg 3>&1 2>&1 $null
