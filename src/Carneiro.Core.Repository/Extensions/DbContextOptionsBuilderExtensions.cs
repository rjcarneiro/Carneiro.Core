namespace Carneiro.Core.Repository.Extensions;

/// <summary>
/// Extensions for <see cref="DbContextOptionsBuilder"/>.
/// </summary>
public static class DbContextOptionsBuilderExtensions
{
    /// <summary>
    /// Uses Sql Server database context provider with <see cref="DatabaseOptions"/>.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="connectionString"></param>
    /// <param name="databaseOptions"></param>
    public static DbContextOptionsBuilder UseSqlServerWithOptions(this DbContextOptionsBuilder options, string connectionString, DatabaseOptions databaseOptions)
    {
        Action<DbContextOptionsBuilder> d = o =>
        {
            o.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            o.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
            o.UseSqlServer(connectionString, providerOptions =>
            {
                if (databaseOptions.QuerySplittingBehavior.HasValue)
                {
                    providerOptions.UseQuerySplittingBehavior(databaseOptions.QuerySplittingBehavior.Value);
                }

                if (databaseOptions.UseRelationalNulls.HasValue)
                {
                    providerOptions.UseRelationalNulls(databaseOptions.UseRelationalNulls.Value);
                }

                if (databaseOptions.MinBatchSize.HasValue)
                {
                    providerOptions.MinBatchSize(databaseOptions.MinBatchSize.Value);
                }

                if (databaseOptions.MaxBatchSize.HasValue)
                {
                    providerOptions.MaxBatchSize(databaseOptions.MaxBatchSize.Value);
                }

                providerOptions.CommandTimeout(databaseOptions.Timeout);
                providerOptions.EnableRetryOnFailure(
                    maxRetryCount: databaseOptions.Failure.Retries,
                    maxRetryDelay: TimeSpan.FromSeconds(databaseOptions.Failure.Seconds),
                    errorNumbersToAdd: null);
            });
        };

        d.Invoke(options);

        return options;
    }
}