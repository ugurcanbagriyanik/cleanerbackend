namespace SharedLibrary.Models;

public class Paging<T>
{
    public List<T> PagingData { get; set; } = new List<T>();
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;

}