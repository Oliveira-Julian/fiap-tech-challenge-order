using FoodChallenge.Common.Interfaces;

namespace FoodChallenge.CrossCutting.Paging;

public class Filter : IFilter
{
    public int Page { get; set; }
    public int SizePage { get; set; }
    public string FieldOrdenation { get; set; }
    public bool OrdenationAsc { get; set; }

    public Filter()
    {
        Page = 1;
        SizePage = 10;
    }

    public Filter(int page, int sizePage)
    {
        Page = page;
        SizePage = sizePage;
    }

    public Filter(int page, int sizePage, string fieldOrdenation, bool ordenationAsc)
    {
        Page = page;
        SizePage = sizePage;
        FieldOrdenation = fieldOrdenation;
        OrdenationAsc = ordenationAsc;
    }
}

public class Filter<T> : Filter where T : class, new()
{
    public T Fields { get; set; }

    public Filter()
    {
        Fields = new T();
    }

    public Filter(int page, int sizePage, T fields)
    {
        Page = page;
        SizePage = sizePage;
        Fields = fields;
    }

    public Filter(int page, int sizePage, string fieldOrdenation, bool ordenationAsc, T fields)
    {
        Page = page;
        SizePage = sizePage;
        FieldOrdenation = fieldOrdenation;
        OrdenationAsc = ordenationAsc;
        Fields = fields;
    }
}
