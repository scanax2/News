namespace AspNetServer.Services.NewsService
{
    public class NewsQueryData
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
