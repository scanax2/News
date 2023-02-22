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
        private readonly IRepository repository;


        public NewsPostsController(IRepository repository)
        {
            this.repository = repository;
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
    }
}