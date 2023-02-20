using Microsoft.AspNetCore.Mvc;
using NewsAPI.Utilities;

namespace NewsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsCategoriesApiController : ControllerBase
    {
        [HttpGet("categories")]
        public ActionResult<Dictionary<string, string>> GetCategories()
        {
            Dictionary<string, string> categories = Constants.NEWS_CATEGORIES;

            return Ok(categories);
        }

        [HttpGet("countries")]
        public ActionResult<Dictionary<string, string>> GetCountries()
        {
            Dictionary<string, string> categories = Constants.NEWS_COUNTRIES;

            return Ok(categories);
        }
    }
}