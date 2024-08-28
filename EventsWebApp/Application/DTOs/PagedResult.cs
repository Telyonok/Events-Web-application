namespace EventsWebApp.Application.DTOs
{
    public class PagedResult<T>
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }

        public PagedResult()
        {
            Items = new List<T>();
        }
    }
}
