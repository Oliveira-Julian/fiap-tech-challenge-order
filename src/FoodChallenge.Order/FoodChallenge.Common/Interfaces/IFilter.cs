namespace FoodChallenge.Common.Interfaces;

public interface IFilter
{
    int Page { get; }
    int SizePage { get; }
    string FieldOrdenation { get; }
    bool OrdenationAsc { get; }
}

public interface IFilter<out T> : IFilter where T : class, new()
{
    T Fields { get; }
}
