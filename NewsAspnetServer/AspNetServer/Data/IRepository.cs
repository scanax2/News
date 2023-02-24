using AspNetServer.Models;
using AspNetServer.Services.NewsService;

namespace AspNetServer.Data
{
    public interface IRepository
    {
        Task<int> GetTotalPagesAsync(NewsQueryData newsQuery);
        Task<IEnumerable<News>> GetNewsAsync(NewsQueryData newsQuery);
        Task<IEnumerable<News>> AddNewsAsync(IEnumerable<News> newsList);
        Task<IEnumerable<News>> DeleteNewsAsync(DateTime beforeDate);
        Task DeleteNewsAsync();
    }
}
