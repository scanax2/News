using Microsoft.AspNetCore.Mvc;
using NewsAPI.Controllers.Data;
using NewsAPI.Models;
using NewsAPI.Services.NewsService;

namespace NewsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsApiController : ControllerBase
    {
        private readonly INewsService service;
        private readonly IRepository repository;


        public NewsApiController(INewsService service, IRepository repository)
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

        [HttpPost]
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

        [HttpDelete]
        public async Task<ActionResult> DeleteAllNews()
        {
            await repository.DeleteAllNewsAsync();
            return Ok();
        }

        [HttpDelete("{before}")]
        public async Task<ActionResult<List<News>>> DeleteOutdatedNews(DateTime before)
        {
            var news = await repository.DeleteOutdatedNewsAsync(before);
            return Ok(news);
        }
    }
}