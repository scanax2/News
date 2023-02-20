using Microsoft.AspNetCore.Mvc;
using NewsAPI.Controllers.Data;
using NewsAPI.Models;
using NewsAPI.Services.NewsService;
using NewsAPI.Utilities;

namespace NewsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsPostsApiController : ControllerBase
    {
        private readonly INewsService service;
        private readonly IRepository repository;


        public NewsPostsApiController(INewsService service, IRepository repository)
        {
            this.service = service;
            this.repository = repository;
        }

        [HttpGet("{pageSize}/{category}/{country}")]
        public async Task<ActionResult<int>> GetTotalPages(int pageSize, string category, string country)
        {
            NewsQuery query = new NewsQuery()
            {
                PageSize = pageSize,
                Category = category,
                Country = country
            };
            int totalPages = await repository.GetTotalNewsPages(query);

            return Ok(totalPages);
        }

        [HttpGet("{pageNumber}/{pageSize}/{category}/{country}")]
        public async Task<ActionResult<List<News>>> GetNews(int pageNumber, int pageSize, string category, string country)
        {
            NewsQuery query = new NewsQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Category = category,
                Country = country
            };
            List<News> news = (List<News>) await repository.GetNewsAsync(query);
            if (news == null || news.Count == 0)
            {
                return BadRequest("no news in repository");
            }

            return Ok(news);
        }

        [HttpPost("init")]
        public async Task<ActionResult<List<News>>> InitNews()
        {
            List<News> news = new List<News>();
            foreach (var categoryData in Constants.NEWS_CATEGORIES)
            {
                foreach (var countryData in Constants.NEWS_COUNTRIES)
                {
                    NewsQuery newsQuery = new NewsQuery()
                    {
                        PageNumber = 1,
                        PageSize = 100,
                        Category = categoryData.Value,
                        Country = countryData.Value
                    };
                    var downloadedNews = await service.GetNews(newsQuery);
                    news.AddRange(downloadedNews);
                }
            }

            if (news == null)
            {
                return BadRequest("no news from News API");
            }

            await repository.AddNewsAsync(news);

            return Ok(news);
        }

        [HttpPost("add")]
        public async Task<ActionResult<List<News>>> AddNews(NewsQuery newsQuery)
        {
            List<News> news = await service.GetNews(newsQuery);
            if (news == null)
            {
                return BadRequest("no news from News API");
            }

            await repository.AddNewsAsync(news);

            return Ok(news);
        }

        [HttpDelete("clear")]
        public async Task<ActionResult> DeleteAllNews()
        {
            await repository.DeleteAllNewsAsync();
            return Ok();
        }

        [HttpDelete("clear/{before}")]
        public async Task<ActionResult<List<News>>> DeleteOutdatedNews(DateTime before)
        {
            var news = await repository.DeleteOutdatedNewsAsync(before);
            return Ok(news);
        }
    }
}