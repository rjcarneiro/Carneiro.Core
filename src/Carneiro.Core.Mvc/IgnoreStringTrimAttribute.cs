namespace Carneiro.Core.Mvc;

/// <summary>
/// Attribute that allows ignore string trim validation on <see cref="StringTrimModelBinder"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public sealed class IgnoreStringTrimAttribute : Attribute
{
}