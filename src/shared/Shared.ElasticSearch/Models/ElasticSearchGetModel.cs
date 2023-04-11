namespace Shared.ElasticSearch.Models;

public class ElasticSearchGetModel<T>
{
    public string ElasticId { get; set; }
    public T Item { get; set; }
}
