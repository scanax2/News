using AspNetServer.Models;
using AspNetServer.Services.NewsApi.Json;
using AspNetServer.Services.NewsService;
using AspNetServer.Utilities;
using System.Text;
using System.Text.Json;

namespace AspNetServer.Services.NewsApi
{
    public class NewsApiService : INewsService
    {
        private readonly IConfiguration configuration;


        public NewsApiService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<News>> GetNews(NewsQueryData queryData)
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

            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Add("User-Agent", "NewsPetProject/1.0");

            List<News>? news = await GetNews(http, categories, countries, queryData.PageSize);

            return news;
        }

        private async Task<List<News>> GetNews(HttpClient http, string[] categories, string[] countries, int pageSize)
        {
            List<News> news = new List<News>();
            foreach (var country in countries)
            {
                foreach (var category in categories)
                {
                    var categoryNews = await ProcessNewsRequest(http, category, country, pageSize);
                    news.AddRange(categoryNews);
                }
            }
            return news;
        }

        private async Task<List<News>> ProcessNewsRequest(HttpClient http, string category, string country, int pageSize)
        {
            var queryData = new NewsQueryData()
            {
                Category = category,
                Country = country,
                PageSize = pageSize
            };
            string query = BuildUrlQuery(queryData);
            string jsonString = await http.GetStringAsync(query);
            List<News>? categoryNews = GetNewsListFromJson(jsonString, queryData.Category, queryData.Country);
            return categoryNews;
        }

        private string BuildUrlQuery(NewsQueryData queryData)
        {
            var secret = configuration.GetRequiredSection("Secret");
            string apiKey = secret.GetValue(typeof(string), "NewsApiKey").ToString()!;
            string urlEndpoint = Constants.NewsApi.URL_ENDPOINT;
            string apiKeyParameter = Constants.NewsApi.API_KEY_NAME_PARAMETER;
            string pageSizeParameter = Constants.NewsApi.PAGE_SIZE_PARAMETER;
            string categoryParameter = Constants.NewsApi.CATEGORY_PARAMETER;
            string countryParameter = Constants.NewsApi.COUNTRY_PARAMETER;

            StringBuilder urlBuilder = new StringBuilder($"{urlEndpoint}?{apiKeyParameter}={apiKey}&");

            urlBuilder.Append($"{pageSizeParameter}={queryData.PageSize}&");
            urlBuilder.Append($"{categoryParameter}={queryData.Category}&");
            urlBuilder.Append($"{countryParameter}={queryData.Country}");

            return urlBuilder.ToString();
        }

        private List<News> GetNewsListFromJson(string jsonString, string keyWord, string language)
        {
            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new NewsJsonConverter());

            List<News>? newsList = JsonSerializer.Deserialize<List<News>>(jsonString, deserializeOptions);
            foreach (var news in newsList!)
            {
                news.Category = keyWord;
                news.Country = language;
            }
            return newsList;
        }
    }
}
