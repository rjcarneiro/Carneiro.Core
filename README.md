# Carneiro.Core

NuGet package that contains default entity behaviour.

## Entities

### Entities Interfaces

- `IBaseEntity`

### Entities Implementations

- `BaseEntity : IBaseEntity`

## Host

### Host Interfaces

n/a

### Host Implementations

#### RzBackgroundService

Default background service.

#### PeriodicBackgroundService

Inherits from `RzBackgroundService`. Options (in seconds):

```json
{
    "min" : 1,
    "max" : 5
}
```

#### RzHostBuilder

- Default host builder

Usage

```csharp
RzHostBuilder.UseDefaultBuilder();
RzHostBuilder.UseDefaultBuilder(string[] args);

var hostBuilder = new RzHostBuilder(string[] args);
```

## Logging

### Logging Interfaces

n/a

### Logging Implementations

- `IWebHostBuilder UseLogging(this IWebHostBuilder builder)`
- `IWebHostBuilder UseLogging(this IWebHostBuilder builder, Action<LoggingOptions> action)`
- `IHostBuilder UseLogging(this IHostBuilder builder)`
- `IHostBuilder UseLogging(this IHostBuilder builder, Action<LoggingOptions> action)`

#### Logging Options

```json
{
    "UseDebug": false,
    "UseConsole": true,
    "Template" : "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] ({SourceContext}) {Message:j}{NewLine}{Exception}",
    "Path" : "logs/log_{Date}.log"
}
```

## Exceptions

## Exceptions Implementations

- `RzException` - Base default controlled exception;
- `HttpConnectivityRzException` - Exception when some http connectivity issue happens;

## Extensions

### DateTime

- `string GetHumanReadableDateDifference(this DateTime dateTime)`

### String

- `string RemoveDuplicatedWhiteSpaces(this string str)`
- `string GenerateSlug(this string phrase)`

### JsonHelper

- `JsonSerializerSettings JsonSettings`
- `string Serialize<T>(T input)`
- `string Serialize<T>(T input, Action<JsonSerializerSettings> action)`
- `string Serialize<T>(T input, JsonSerializerSettings settings)`

### TrimmingConverter : JsonConverter

### PaginatedListExtensions

- `PaginatedList<T> Convert<Y, T>(this PaginatedList<Y> paginatedList, IEnumerable<T> items)`

Example

```csharp
PaginatedList<RequestItemModel> files = await Get();
PaginatedList<RequestItemViewModel> items = files.Convert(files.Select(t => new RequestItemViewModel()));
```

## Repository

## Repository Interfaces

- `IUnitOfWork`

## Repository Implementations

- `UnitOfWork<TEntity> : IUnitOfWork`
- RzIdentityDbContext
- RzDbContext
- DbSettings

## NuGet Package

- [Carneiro.Core](http://nuget.ricardocarneiro.pt/?q=Carneiro.Core)
