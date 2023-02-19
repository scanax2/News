using NewsAPI.Models;
using NewsAPI.Services.NewsService;

namespace NewsAPI.Controllers.Data
{
    public interface IRepository
    {
        Task<int> GetTotalNewsPages(NewsQuery newsQuery);
        Task<IEnumerable<News>> GetNewsAsync(NewsQuery newsQuery);
        Task<IEnumerable<News>> AddNewsAsync(IEnumerable<News> newsList);
        Task<IEnumerable<News>> DeleteOutdatedNewsAsync(DateTime beforeDate);
        Task DeleteAllNewsAsync();
    }
}
