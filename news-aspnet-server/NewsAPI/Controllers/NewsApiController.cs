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

        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<ActionResult<List<News>>> GetNews(int pageNumber, int pageSize)
        {
            List<News> news = (List<News>) await repository.GetNewsAsync(pageNumber, pageSize);
            if (news == null || news.Count == 0)
            {
                return BadRequest("no news in repository");
            }

            return Ok(news);
        }

        [HttpPost("{pageNumber}/{pageSize}/{keyWord}/{from}")]
        public async Task<ActionResult<List<News>>> AddNews(int pageNumber, int pageSize, string keyWord, DateTime from)
        {
            List<News> news = await service.GetNews(pageNumber, pageSize, keyWord, from.ToString("yyyy-mm-dd"), SortType.popularity);
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