using AspNetServer.Models;

namespace AspNetServer.Services.NewsService
{
    public interface INewsService
    {
        Task<List<News>> GetNews(NewsQueryData newsQuery);
    }
}
