using NewsAPI.Models;

namespace NewsAPI.Controllers.Data
{
    public interface IRepository
    {
        Task<IEnumerable<News>> GetNewsAsync(int pageNumber, int pageSize);
        Task<IEnumerable<News>> AddNewsAsync(IEnumerable<News> newsList);
        Task<IEnumerable<News>> DeleteOutdatedNewsAsync(DateTime beforeDate);
        Task DeleteAllNewsAsync();
    }
}
