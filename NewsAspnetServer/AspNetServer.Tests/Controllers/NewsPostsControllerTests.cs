namespace AspNetServer.Tests.Controllers
{
    public class NewsPostsControllerTests
    {
        private INewsService newsService;


        [SetUp]
        public void Setup()
        {
            newsService = new NewsApiServiceMock();
        }

        [Test]
        public async Task InitNewsTest()
        {
            using (var context = GetContext())
            {
                var controller = GetController(context);

                var result = (await controller.InitNews()).Result;
                Assert.That(result, Is.Not.Null);

                var resultOk = result as OkObjectResult;
                Assert.That(resultOk, Is.Not.Null);
                Assert.That(resultOk.Value, Has.Count.AtLeast(1));
            }
        }

        [Test]
        public async Task ClearNewsTest()
        {
            using (var context = GetContext())
            {
                var controller = GetController(context);
                await controller.InitNews();

                var result = await controller.DeleteNews();
                Assert.That(result, Is.Not.Null);

                var resultOk = result as OkResult;
                Assert.That(resultOk, Is.Not.Null);
            }
        }

        [Test]
        public async Task AddNewsTest()
        {
            using (var context = GetContext())
            {
                var controller = GetController(context);

                var queryData = new NewsQueryData()
                {
                    Category = Constants.NEWS_CATEGORIES.Values.ToArray()[1],
                    Country = Constants.NEWS_COUNTRIES.Values.ToArray()[1]
                };

                var result = (await controller.AddNews(queryData)).Result;
                Assert.That(result, Is.Not.Null);

                var resultOk = result as OkObjectResult;
                Assert.That(resultOk, Is.Not.Null);
                Assert.That(resultOk.Value, Has.Count.AtLeast(1));
            }
        }

        [Test]
        public async Task GetTotalPagesTest()
        {
            using (var context = GetContext())
            {
                var controller = GetController(context);
                await controller.InitNews();

                var category = Constants.NEWS_CATEGORIES.Values.ToArray()[1];
                var country = Constants.NEWS_COUNTRIES.Values.ToArray()[1];

                var result = (await controller.GetTotalPages(9, category, country)).Result;
                Assert.That(result, Is.Not.Null);

                var resultOk = result as OkObjectResult;
                Assert.That(resultOk, Is.Not.Null);
                Assert.That(resultOk.Value, Is.Not.Zero);
                Assert.That(resultOk.Value, Is.Positive);
            }
        }

        [Test]
        public async Task GetNewsTest()
        {
            using (var context = GetContext())
            {
                var controller = GetController(context);
                await controller.InitNews();

                var category = Constants.NEWS_CATEGORIES.Values.First();
                var country = Constants.NEWS_COUNTRIES.Values.First();

                var result = (await controller.GetNews(0, 9, category, country)).Result;
                Assert.That(result, Is.Not.Null);

                var resultOk = result as OkObjectResult;
                Assert.That(resultOk, Is.Not.Null);
                Assert.That(resultOk.Value, Has.Count.AtLeast(1));
            }
        }

        private NewsPostsController GetController(DataContext context)
        {
            var repository = new NewsRepositorySql(context);
            var controller = new NewsPostsController(repository, newsService);

            return controller;
        }

        private DataContext GetContext()
        {
            var dbOptions = new DbContextOptionsBuilder<DataContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;

            var context = new DataContext(dbOptions);
            context.Database.EnsureCreated();

            return context;
        }
    }
}