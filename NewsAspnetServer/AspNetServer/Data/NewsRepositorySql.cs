using Microsoft.EntityFrameworkCore;
using AspNetServer.Models;
using AspNetServer.Services.NewsService;
using System.Reflection.PortableExecutable;
using System.Linq;

namespace AspNetServer.Data
{
    public class NewsRepositorySql : IRepository
    {
        private readonly DataContext context;


        public NewsRepositorySql(DataContext context)
        {
            this.context = context;
        }

        public async Task<int> GetTotalPagesAsync(NewsQueryData queryData)
        {
            int newsCount = await context.News
                .CountAsync(x => (x.Country == queryData.Country || queryData.Country == "any") &&
                                 (x.Category == queryData.Category || queryData.Category == "any"));

            int totalPages = (int)Math.Ceiling((float)newsCount / queryData.PageSize);

            return totalPages;
        }

        public async Task<IEnumerable<News>> GetNewsAsync(NewsQueryData queryData)
        {
            int postsToSkip = queryData.PageNumber * queryData.PageSize;
            int postsToTake = queryData.PageSize;

            List<News> news = await context.News
                .Where(x => (x.Country == queryData.Country || queryData.Country == "any") &&
                            (x.Category == queryData.Category || queryData.Category == "any"))
                .Skip(postsToSkip)
                .Take(postsToTake)
                .ToListAsync();

            return news;
        }

        public async Task<IEnumerable<News>> AddNewsAsync(IEnumerable<News> newsList)
        {
            foreach (var news in newsList)
            {
                if (context.News
                    .Any(x => x.Title == news.Title &&
                              x.Description == news.Description))
                {
                    continue;
                }
                context.News.Add(news);
            }
            await context.SaveChangesAsync();
            return context.News;
        }

        /// <summary>
        /// Used for removing outdated news
        /// </summary>
        public async Task<IEnumerable<News>> DeleteNewsAsync(DateTime beforeDate)
        {
            foreach (var news in context.News.Where(x => x.PublishedAt < beforeDate))
            {
                context.News.Remove(news);
            }
            await context.SaveChangesAsync();
            return context.News;
        }

        public async Task DeleteNewsAsync()
        {
            context.News.RemoveRange(context.News);
            await context.SaveChangesAsync();
        }
    }
}
