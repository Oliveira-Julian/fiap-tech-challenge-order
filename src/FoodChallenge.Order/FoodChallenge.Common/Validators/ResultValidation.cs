namespace FoodChallenge.Common.Validators;

public sealed class ResultValidation
{
    public bool IsValid => !Messages.Any();
    public IEnumerable<string> Messages { get; private set; }
    public ResultValidation(IEnumerable<string> messages)
    {
        Messages = messages;
    }
}
