namespace FoodChallenge.CrossCutting.Paging;

public class Ordernation
{
    public int Order { get; protected set; }
    public string Field { get; protected set; }
    public bool OrderAsc { get; protected set; }

    public Ordernation(int order, string field, bool orderAsc = true)
    {
        Order = order;
        Field = field;
        OrderAsc = orderAsc;
    }
}
