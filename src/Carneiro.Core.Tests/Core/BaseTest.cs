﻿namespace Carneiro.Core.Tests.Core;

/// <summary>
/// Base test class.
/// </summary>
public abstract class BaseTest
{
    /// <summary>
    /// Gets the faker.
    /// </summary>
    /// <value>
    /// The faker.
    /// </value>
    public Faker Faker { get; } = new();

    /// <summary>
    /// Gets the UTC now.
    /// </summary>
    /// <value>
    /// The UTC now.
    /// </value>
    public DateTime UtcNow => DateTime.UtcNow;

    /// <summary>
    /// Generates the email.
    /// </summary>
    public string GenerateEmail() => Faker.GenerateEmail();

    /// <summary>
    /// Generates the password.
    /// </summary>
    public string GeneratePassword() => Faker.GeneratePassword();

    /// <summary>
    /// Generates the mock.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public Mock<T> GenerateMock<T>() where T : class => new();

    /// <summary>
    /// Generates the logger mock.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public ILogger<T> GenerateLoggerMock<T>() where T : class => new Mock<ILogger<T>>().Object;
}