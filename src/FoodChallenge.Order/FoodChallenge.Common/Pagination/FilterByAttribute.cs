namespace FoodChallenge.CrossCutting.Paging;

[AttributeUsage(AttributeTargets.Property)]
public class FilterByAttribute(string propertyName, FilterType filterType = FilterType.Equals) : Attribute
{
    public string PropertyName { get; } = propertyName;
    public FilterType FilterType { get; } = filterType;
}