namespace ChatBackend.Application.WebApi.Helpers;

public class Paginated<T> where T : class
{
    public int Count { get; set; }
    public ICollection<T> Data { get; set; }
    
    public Paginated(ICollection<T> data)
    {
        Count = data.Count;
        Data = data;
    }
}