using FoodChallenge.Common.Entities;

namespace FoodChallenge.Common.Validators;

public sealed class ResponseValidation
{
    public ResponseValidation(IEnumerable<Resposta> response)
    {
        Responses = response;
    }

    public bool Valid => !Responses.Any(r => !r.Sucesso);

    public IEnumerable<Resposta> Responses { get; private set; }
}
