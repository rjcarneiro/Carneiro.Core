using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;

namespace Carneiro.Core.Tests.Core;

/// <summary>
/// Adds supports for auto moq containers
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class AutoMoqWithInlineAutoData : InlineAutoDataAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AutoMoqWithInlineAutoData"/> class.
    /// </summary>
    /// <param name="values">The values.</param>
    public AutoMoqWithInlineAutoData(params object[] values)
        : base(CreateFixture, values)
    {
    }

    private static IFixture CreateFixture()
    {
        IFixture fixture = new Fixture().Customize(new AutoMoqCustomization
        {
            ConfigureMembers = true
        });

        fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        return fixture;
    }
}