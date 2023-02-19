namespace NewsAPI.Models
{
    public class News
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime PublishedAt { get; set; } = DateTime.Now;
    }
}
