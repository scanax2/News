using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;
using NewsAPI.Services.NewsService;

namespace NewsAPI.Controllers.Data
{
    public class RepositorySQL : IRepository
    {
        private readonly DataContext context;


        public RepositorySQL(DataContext context)
        {
            this.context = context;
        }

        public async Task<int> GetTotalNewsPages(NewsQuery newsQuery)
        {
            var found = await context.News
                .Where(x => x.Category.Equals(newsQuery.Category) && x.Country.Equals(newsQuery.Country))
                .ToListAsync();
            int newsCount = found.Count();
            int totalPages = (int)Math.Ceiling((float)newsCount / newsQuery.PageSize);
            return totalPages;
        }

        public async Task<IEnumerable<News>> GetNewsAsync(NewsQuery newsQuery)
        {
            var filtered = await context.News
                .Where(x => x.Category.Equals(newsQuery.Category) && x.Country.Equals(newsQuery.Country))
                .ToListAsync();

            int totalCount = filtered.Count();
            int from = newsQuery.PageNumber * newsQuery.PageSize;

            var found = filtered.Skip(from).Take(newsQuery.PageSize).ToList();

            return found;
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

        public async Task<IEnumerable<News>> DeleteOutdatedNewsAsync(DateTime beforeDate)
        {
            foreach (var news in context.News.Where(x => x.PublishedAt < beforeDate))
            {
                context.News.Remove(news);
            }
            await context.SaveChangesAsync();
            return context.News;
        }

        public async Task DeleteAllNewsAsync()
        {
            context.News.RemoveRange(context.News);
            await context.SaveChangesAsync();
        }
    }
}
