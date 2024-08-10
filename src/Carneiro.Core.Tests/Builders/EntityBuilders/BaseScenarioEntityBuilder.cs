namespace Carneiro.Core.Tests.Builders.EntityBuilders;

/// <summary>
/// The base scenario entity builder.
/// </summary>
public abstract class BaseScenarioEntityBuilder
{
    /// <summary>
    /// Gets the service provider.
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Gets the faker.
    /// </summary>
    protected Faker Faker { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseScenarioEntityBuilder"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    protected BaseScenarioEntityBuilder(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        Faker = new Faker();
    }
}