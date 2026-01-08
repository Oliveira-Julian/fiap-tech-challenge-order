namespace FoodChallenge.CrossCutting.Paging;

public class Pagination<T>(int page, int sizePage, long totalRecords, IEnumerable<T> records)
{
    public int Page { get; set; } = page;
    public int SizePage { get; set; } = sizePage;
    public long TotalRecords { get; set; } = totalRecords;
    public IEnumerable<T> Records { get; set; } = records;
}
