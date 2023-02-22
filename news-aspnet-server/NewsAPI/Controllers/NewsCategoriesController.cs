using Microsoft.AspNetCore.Mvc;
using AspNetServer.Utilities;

namespace AspNetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsCategoriesController : ControllerBase
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
            Dictionary<string, string> countries = Constants.NEWS_COUNTRIES;

            return Ok(countries);
        }
    }
}