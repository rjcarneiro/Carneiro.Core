# Carneiro.Core

This repository has a bunch of helpful libraries that does a lot of heavy lifting on my daily basis.

## How to publish

To publish a package, we first need to build and then publish the results:

```pwsh
.\scripts\Build-NuGets.ps1 -Version VERSION
```

And then, just publish

```pwsh
.\scripts\Push-NuGets.ps1 -ApiKey APIKEY
```

## NuGet Package

- [Carneiro.Core](https://www.nuget.org/packages?q=Carneiro.Core)
