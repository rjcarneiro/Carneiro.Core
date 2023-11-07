using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Carneiro.Core.Web;

/// <summary>
/// Validation Failed Result.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ObjectResult" />
public class ValidationFailedResult : ObjectResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationFailedResult" /> class.
    /// </summary>
    /// <param name="modelState">State of the model.</param>
    public ValidationFailedResult(ModelStateDictionary modelState)
        : base(null)
    {
        StatusCode = StatusCodes.Status400BadRequest;
        IEnumerable<ApiErrorResponseField> data = modelState.Keys
            .SelectMany(key => modelState[key].Errors.Select(x => new ApiErrorResponseField
            {
                Field = key != string.Empty ? key : null, 
                Message = x.ErrorMessage
            }));
        
        Value = new ApiErrorResponse<IEnumerable<ApiErrorResponseField>>
        {
            Message = "There were problems validating the input data", 
            Data = data
        };
    }
}