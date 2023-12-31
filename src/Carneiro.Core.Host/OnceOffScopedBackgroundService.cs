﻿using Microsoft.Extensions.DependencyInjection;

namespace Carneiro.Core.Host;

/// <summary>
/// Background service job that just runs once with a new <see cref="AsyncServiceScope"/>.
/// </summary>
public abstract class OnceOffScopedBackgroundService : OnceOffBackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="OnceOffScopedBackgroundService" /> class.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="serviceProvider"></param>
    protected OnceOffScopedBackgroundService(ILogger<OnceOffScopedBackgroundService> logger, IServiceProvider serviceProvider) 
        : base(logger)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    protected override async Task RunAsync(CancellationToken cancellationToken)
    {
        await using AsyncServiceScope asyncServiceScope = _serviceProvider.CreateAsyncScope();
        await RunScopedAsync(asyncServiceScope, cancellationToken);
    }

    /// <summary>
    /// Executes the <see cref="OnceOffBackgroundService"/> with a new <see cref="AsyncServiceScope"/>.
    /// </summary>
    /// <param name="asyncServiceScope"></param>
    /// <param name="cancellationToken"></param>
    protected abstract Task RunScopedAsync(AsyncServiceScope asyncServiceScope, CancellationToken cancellationToken);
}
