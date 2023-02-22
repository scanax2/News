using Microsoft.AspNetCore.Mvc;
using AspNetServer.Models;
using AspNetServer.Services.NewsService;
using AspNetServer.Utilities;
using AspNetServer.Data;

namespace AspNetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsRepositoryController : ControllerBase
    {
        private readonly INewsService newsService;
        private readonly IRepository repository;


        public NewsRepositoryController(IRepository repository, INewsService newsService)
        {
            this.repository = repository;
            this.newsService = newsService;
        }

        [HttpPost("init")]
        public async Task<ActionResult<List<News>>> InitNews()
        {
            List<News> news = new List<News>();
            foreach (var categoryData in Constants.NEWS_CATEGORIES)
            {
                foreach (var countryData in Constants.NEWS_COUNTRIES)
                {
                    NewsQueryData newsQuery = new NewsQueryData()
                    {
                        PageNumber = 1,
                        PageSize = 100,
                        Category = categoryData.Value,
                        Country = countryData.Value
                    };
                    var downloadedNews = await newsService.GetNews(newsQuery);
                    news.AddRange(downloadedNews);
                }
            }

            if (news == null)
            {
                return NotFound(Constants.EMPTY_RESULT);
            }

            await repository.AddNewsAsync(news);

            return Ok(news);
        }

        [HttpPost]
        public async Task<ActionResult<List<News>>> AddNews(NewsQueryData newsQuery)
        {
            List<News> news = await newsService.GetNews(newsQuery);
            if (news == null)
            {
                return NotFound(Constants.EMPTY_RESULT);
            }

            await repository.AddNewsAsync(news);

            return Ok(news);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteNews()
        {
            await repository.DeleteNewsAsync();
            return Ok();
        }

        [HttpDelete("{beforeDate}")]
        public async Task<ActionResult<List<News>>> DeleteOutdatedNews(DateTime beforeDate)
        {
            var news = await repository.DeleteNewsAsync(beforeDate);
            return Ok(news);
        }
    }
}
