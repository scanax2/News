namespace AspNetServer.Tests.Mocks
{
    internal class NewsApiServiceMock : INewsService
    {
        private List<News> newsInRepository = new List<News>();


        public NewsApiServiceMock()
        {
            newsInRepository = GetSampleNews();
        }

        public Task<List<News>> GetNews(NewsQueryData queryData)
        {
            var categories = new string[1] { queryData.Category };
            var countries = new string[1] { queryData.Country };

            if (queryData.Category == "any")
            {
                categories = Constants.NEWS_CATEGORIES
                    .Skip(1)
                    .Select(x => x.Value)
                    .ToArray();
            }
            if (queryData.Country == "any")
            {
                countries = Constants.NEWS_COUNTRIES
                    .Skip(1)
                    .Select(x => x.Value)
                    .ToArray();
            }

            var news = GetNews(categories, countries);
       
            return Task.FromResult(news);
        }

        private List<News> GetNews(string[] categories, string[] countries)
        {
            List<News> news = new List<News>();
            foreach (var country in countries)
            {
                foreach (var category in categories)
                {
                    var categoryNews = newsInRepository
                        .Where(x => x.Country == country && x.Category == category)
                        .ToList();

                    news.AddRange(categoryNews);
                }
            }
            return news;
        }

        private List<News> GetSampleNews()
        {
            List<News> news = new List<News>();

            var categories = Constants.NEWS_CATEGORIES
                .Skip(1)
                .Select(x => x.Value)
                .ToArray();

            var countries = Constants.NEWS_COUNTRIES
                .Skip(1)
                .Select(x => x.Value)
                .ToArray();

            foreach (var category in categories)
            {
                foreach (var country in countries)
                {
                    news.Add(new News() 
                    { 
                        Category = category,
                        Country = country
                    });
                }
            }

            return news;
        }
    }
}
