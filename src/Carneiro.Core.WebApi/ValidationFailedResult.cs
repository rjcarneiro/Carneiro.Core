using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Carneiro.Core.WebApi;

/// <summary>
/// Validation Failed Result.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ObjectResult" />
public class ValidationFailedResult : ObjectResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationFailedResult" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="modelState">State of the model.</param>
    public ValidationFailedResult(ILogger<ValidationFailedResult> logger, ModelStateDictionary modelState)
        : base(null)
    {
        StatusCode = StatusCodes.Status400BadRequest;
        IEnumerable<ApiErrorResponseField> data = modelState.Keys.SelectMany(key =>
            modelState[key].Errors.Select(x => new ApiErrorResponseField { Field = key != string.Empty ? key : null, Message = x.ErrorMessage }));
        
        Value = new ApiErrorResponse<IEnumerable<ApiErrorResponseField>> { Message = "There were problems validating the input data", Data = data };
        
        foreach (ApiErrorResponseField field in data)
            logger.LogError($"Error in property {field}");
    }
}