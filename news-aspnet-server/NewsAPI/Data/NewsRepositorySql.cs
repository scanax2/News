using Microsoft.EntityFrameworkCore;
using AspNetServer.Models;
using AspNetServer.Services.NewsService;

namespace AspNetServer.Data
{
    public class NewsRepositorySql : IRepository
    {
        private readonly DataContext context;


        public NewsRepositorySql(DataContext context)
        {
            this.context = context;
        }

        public async Task<int> GetTotalPagesAsync(NewsQueryData newsQuery)
        {
            List<News> found = await GetFilteredNews(newsQuery);

            int newsCount = found.Count;
            int totalPages = (int)Math.Ceiling((float)newsCount / newsQuery.PageSize);
            return totalPages;
        }

        public async Task<IEnumerable<News>> GetNewsAsync(NewsQueryData newsQuery)
        {
            List<News> filtered = await GetFilteredNews(newsQuery);

            int from = newsQuery.PageNumber * newsQuery.PageSize;

            List<News> found = filtered.Skip(from).Take(newsQuery.PageSize).ToList();

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

        private async Task<List<News>> GetFilteredNews(NewsQueryData newsQuery)
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
