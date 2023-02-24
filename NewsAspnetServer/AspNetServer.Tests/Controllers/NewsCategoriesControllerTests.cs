namespace AspNetServer.Tests.Controllers
{
    public class NewsCategoriesControllerTests
    {
        private NewsCategoriesController categoriesController;


        [SetUp]
        public void Setup()
        {
            categoriesController = new NewsCategoriesController();
        }

        [Test]
        public void GetCategoriesTest()
        {
            var result = categoriesController.GetCategories().Result;
            Assert.That(result, Is.Not.Null);

            var resultOk = result as OkObjectResult;
            Assert.That(resultOk, Is.Not.Null);

            var categories = resultOk.Value;
            Assert.That(categories, Is.EqualTo(Constants.NEWS_CATEGORIES));
        }

        [Test]
        public void GetCountriesTest()
        {
            var result = categoriesController.GetCountries().Result;
            Assert.That(result, Is.Not.Null);

            var resultOk = result as OkObjectResult;
            Assert.That(resultOk, Is.Not.Null);

            var countries = resultOk.Value;
            Assert.That(countries, Is.EqualTo(Constants.NEWS_COUNTRIES));
        }
    }
}