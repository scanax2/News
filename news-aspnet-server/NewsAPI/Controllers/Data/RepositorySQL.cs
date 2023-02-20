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
            var found = await GetFilteredNews(newsQuery);

            int newsCount = found.Count;
            int totalPages = (int)Math.Ceiling((float)newsCount / newsQuery.PageSize);
            return totalPages;
        }

        public async Task<IEnumerable<News>> GetNewsAsync(NewsQuery newsQuery)
        {
            var filtered = await GetFilteredNews(newsQuery);

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

        private async Task<List<News>> GetFilteredNews(NewsQuery newsQuery)
        {
            List<News> filteredByCountry = new List<News>();
            if (!newsQuery.Country.Equals("any"))
            {
                filteredByCountry = await context.News
                                        .Where(x => x.Country.Equals(newsQuery.Country))
                                        .ToListAsync();
            }
            else
            {
                filteredByCountry = await context.News.ToListAsync();
            }

            List<News> filteredByCategory = new List<News>();
            if (!newsQuery.Category.Equals("any"))
            {
                filteredByCategory = await context.News
                                        .Where(x => x.Category.Equals(newsQuery.Category))
                                        .ToListAsync();
            }
            else
            {
                filteredByCategory = await context.News.ToListAsync();
            }

            List<News> found = (from country in filteredByCountry
                                join category in filteredByCategory on country.Id equals category.Id
                                select country).ToList();
            return found;
        }
    }
}
