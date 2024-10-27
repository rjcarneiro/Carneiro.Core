namespace Carneiro.Core.Tests.Scenarios;

/// <summary>
/// Record that represents a json file as settings.
/// </summary>
/// <param name="File"></param>
/// <param name="Optional"></param>
public record JsonSettingsItem(string File, bool Optional);