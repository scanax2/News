using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;

namespace NewsAPI.Controllers.Data
{
    public class RepositorySQL : IRepository
    {
        private readonly DataContext context;


        public RepositorySQL(DataContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<News>> GetNewsAsync(int pageNumber, int pageSize)
        {
            int count = context.News.Count();

            int from = pageNumber * pageSize;
     
            var found = await context.News.Skip(from).Take(pageSize).ToListAsync();

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
