using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Carneiro.Core.Web;

/// <summary>
/// The <see cref="string"/> trim model binder.
/// </summary>
public class StringTrimModelBinder : IModelBinder
{
    /// <inheritdoc />
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        bindingContext.Result = ModelBindingResult.Success(valueProviderResult.FirstValue?.Trim());

        return Task.CompletedTask;
    }
}
