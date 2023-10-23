using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Carneiro.Core.Mvc;

/// <summary>
/// 
/// </summary>
public class TrimFormValuesModelBinderProvider : IModelBinderProvider
{
    private static readonly ConcurrentDictionary<string, bool> ClassesWithTrimAttribute = new();

    /// <inheritdoc />
    public IModelBinder GetBinder(ModelBinderProviderContext objModelBinderProviderContext)
    {
        ArgumentNullException.ThrowIfNull(objModelBinderProviderContext, nameof(objModelBinderProviderContext));

        if (!ShouldUseCustomModelBinder(objModelBinderProviderContext))
        {
            return null;
        }

        return new TrimFormValuesModelBinder(new SimpleTypeModelBinder(objModelBinderProviderContext.Metadata.ModelType, 
            objModelBinderProviderContext.Services.GetRequiredService<ILoggerFactory>()));
    }

    private static bool ShouldUseCustomModelBinder(ModelBinderProviderContext objModelBinderProviderContext)
    {
        if (objModelBinderProviderContext.Metadata.IsComplexType
            || objModelBinderProviderContext.Metadata.ModelType != typeof(string))
        {
            return false;
        }

        //ok it's a string, but does this string belongs to a property in a class?
        if (!(objModelBinderProviderContext.Metadata.ContainerMetadata?.ModelType.IsClass ?? false))
        {
            return false;
        }

        var key = objModelBinderProviderContext.Metadata.ContainerMetadata.ModelType.FullName;

        return ClassesWithTrimAttribute.GetOrAdd(key, _ => objModelBinderProviderContext.Metadata
            .ContainerMetadata
            .ModelType
            .GetCustomAttributes()
            .OfType<TrimFormStringsAttribute>()
            .Any());
    }
}

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class TrimFormStringsAttribute : Attribute
{
}

/// <summary>
/// 
/// </summary>
public class TrimFormValuesModelBinder : IModelBinder
{
    private readonly IModelBinder _objIModelBinder;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objIModelBinder"></param>
    public TrimFormValuesModelBinder(IModelBinder objIModelBinder)
    {
        ArgumentNullException.ThrowIfNull(objIModelBinder, nameof(objIModelBinder));

        _objIModelBinder = objIModelBinder;
    }

    /// <inheritdoc />
    public async Task BindModelAsync(ModelBindingContext objModelBindingContext)
    {
        ArgumentNullException.ThrowIfNull(objModelBindingContext, nameof(objModelBindingContext));

        var providedValue = objModelBindingContext.ValueProvider.GetValue(objModelBindingContext.ModelName);

        if (providedValue.FirstValue is { } str && !string.IsNullOrEmpty(str))
        {
            objModelBindingContext.Result = ModelBindingResult.Success(str.Trim());

            return;
        }

        await _objIModelBinder.BindModelAsync(objModelBindingContext);
    }
}