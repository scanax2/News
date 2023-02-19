using NewsAPI.Models;

namespace NewsAPI.Services.NewsService
{
    public interface INewsService
    {
        Task<List<News>> GetNews(NewsQuery newsQuery);
    }
}
