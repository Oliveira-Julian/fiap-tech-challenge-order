using FluentValidation;
using FoodChallenge.Common.Entities;

namespace FoodChallenge.Common.Validators;

public class FluentValidatorBase<TModel> : AbstractValidator<TModel>, IValidator
{
    public ResponseValidation Validate(object model)
    {
        var result = base.Validate((TModel)model);
        var responses = result.Errors.Select(x => Resposta.ComFalha(x.ErrorMessage));
        return new ResponseValidation(responses);
    }

    public async Task<ResponseValidation> ValidateModelAsync(object model, CancellationToken cancellationToken)
    {
        var result = await ValidateAsync((TModel)model, cancellationToken);
        var validationMessages = result.Errors.Select(x => Resposta.ComFalha(x.ErrorMessage));
        return new ResponseValidation(validationMessages);
    }
}
