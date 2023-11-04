namespace Carneiro.Core.Mvc;

/// <summary>
/// The string trim validation model binder provider. Works with <see cref="IgnoreStringTrimAttribute"/> to ignore <see cref="string.Trim()"/> on model state.
/// </summary>
public class StringTrimModelBinderProvider : IModelBinderProvider
{
    private static readonly ConcurrentDictionary<string, bool> SmallCache = new();

    /// <inheritdoc />
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.IsComplexType || context.Metadata.ModelType != typeof(string))
        {
            return null;
        }

        if (!(context.Metadata.ContainerMetadata?.ModelType.IsClass ?? false))
        {
            return null;
        }

        var key = $"{context.Metadata.ContainerMetadata.ModelType.FullName}.{context.Metadata.Name}";
        if (SmallCache.TryGetValue(key, out var hasBinder))
        {
            return !hasBinder ? null : new BinderTypeModelBinder(typeof(StringTrimModelBinder));
        }

        // check if class has ignore attribute
        if (context.Metadata.ContainerMetadata.ModelType
            .GetCustomAttributes()
            .OfType<IgnoreStringTrimAttribute>()
            .Any())
        {
            SmallCache.TryAdd(key, false);
            return null;
        }

        if (context.Metadata is not DefaultModelMetadata defaultModelMetadata)
        {
            SmallCache.TryAdd(key, false);
            return null;
        }

        if (defaultModelMetadata.Attributes.Attributes.OfType<IgnoreStringTrimAttribute>().Any())
        {
            SmallCache.TryAdd(key, false);
            return null;
        }

        SmallCache.TryAdd(key, true);
        return new BinderTypeModelBinder(typeof(StringTrimModelBinder));
    }
}