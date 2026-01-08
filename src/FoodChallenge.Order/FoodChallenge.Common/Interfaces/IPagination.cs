namespace FoodChallenge.Common.Interfaces;

public interface IPagination<T>
{
    int Page { get; }
    int SizePage { get; }
    long TotalRecords { get; }
    IEnumerable<T> Records { get; }
}
