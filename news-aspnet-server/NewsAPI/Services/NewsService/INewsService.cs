using NewsAPI.Models;

namespace NewsAPI.Services.NewsService
{
    public interface INewsService
    {
        Task<List<News>> GetNews(int pageNumber, int pageSize, string keyWord, string from, SortType sortBy);
    }
}
