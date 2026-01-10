namespace FoodChallenge.Common.Validators;

public class ValidationContext
{
    private readonly List<string> validationMessages;
    public IReadOnlyCollection<string> ValidationMessages => validationMessages;

    public ValidationContext()
    {
        validationMessages = new List<string>();
    }

    public bool HasValidations => validationMessages.Any();

    public ValidationContext AddValidation(string message)
    {
        validationMessages.Add(message);
        return this;
    }

    public ValidationContext AddValidations(IEnumerable<string> validationMessages)
    {
        this.validationMessages.AddRange(validationMessages);
        return this;
    }

    public ValidationContext AddValidations(ResponseValidation responseValidation)
    {
        responseValidation.Responses
            .Where(r => !r.Sucesso)
            .SelectMany(r => r.Mensagens)
            .ToList().ForEach(mensagem =>
            {
                AddValidation(mensagem);
            });
        return this;
    }

    public ValidationContext AddValidations(object target, params IValidator[] validators)
    {
        if (validators != null && validators.Length > 0)
        {
            foreach (var resposta in validators.SelectMany(v => v.Validate(target).Responses))
            {
                AddValidations(resposta.Mensagens);
            }
        }

        return this;
    }

    public async Task<ValidationContext> AddValidationsAsync(object target, CancellationToken cancellationToken, params IValidator[] validators)
    {
        if (validators != null && validators.Length > 0)
        {
            var validationResults = await Task.WhenAll(
                validators.Select(v => v.ValidateModelAsync(target, cancellationToken))
            );

            foreach (var result in validationResults)
            {
                foreach (var resposta in result.Responses)
                {
                    AddValidations(resposta.Mensagens);
                }
            }
        }
        return this;
    }
}
