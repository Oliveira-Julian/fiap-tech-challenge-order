namespace FoodChallenge.Common.Validators;

public interface IValidator
{
    ResponseValidation Validate(object model);
    Task<ResponseValidation> ValidateModelAsync(object model, CancellationToken cancellationToken);
}
