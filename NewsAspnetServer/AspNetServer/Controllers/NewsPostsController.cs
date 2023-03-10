using Microsoft.AspNetCore.Mvc;
using AspNetServer.Models;
using AspNetServer.Services.NewsService;
using AspNetServer.Data;
using AspNetServer.Utilities;

namespace AspNetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsPostsController : ControllerBase
    {
        private readonly INewsService newsService;
        private readonly IRepository repository;


        public NewsPostsController(IRepository repository, INewsService newsService)
        {
            this.repository = repository;
            this.newsService = newsService;
        }

        [HttpGet("{pageSize}/{category}/{country}")]
        public async Task<ActionResult<int>> GetTotalPages(int pageSize, string category, string country)
        {
            NewsQueryData queryData = new NewsQueryData()
            {
                PageSize = pageSize,
                Category = category,
                Country = country
            };

            int totalPages = await repository.GetTotalPagesAsync(queryData);

            return Ok(totalPages);
        }

        [HttpGet("{pageNumber}/{pageSize}/{category}/{country}")]
        public async Task<ActionResult<IEnumerable<News>>> GetNews(int pageNumber, int pageSize, string category, string country)
        {
            NewsQueryData queryData = new NewsQueryData()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Category = category,
                Country = country
            };

            List<News> news = (List<News>) await repository.GetNewsAsync(queryData);

            if (news == null || news.Count == 0)
            {
                return NotFound(Constants.EMPTY_RESULT);
            }

            return Ok(news);
        }

        [HttpPost("init")]
        public async Task<ActionResult<IEnumerable<News>>> InitNews()
        {
            NewsQueryData newsQuery = new NewsQueryData()
            {
                PageSize = 100,
                Category = "any",
                Country = "any"
            };
            List<News> news = await newsService.GetNews(newsQuery);

            if (news == null || news.Count == 0)
            {
                return NotFound(Constants.EMPTY_RESULT);
            }

            await repository.AddNewsAsync(news);

            return Ok(news);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<News>>> AddNews(NewsQueryData newsQuery)
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
        public async Task<ActionResult<IEnumerable<News>>> DeleteOutdatedNews(DateTime beforeDate)
        {
            var news = await repository.DeleteNewsAsync(beforeDate);
            return Ok(news);
        }
    }
}