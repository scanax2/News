using AspNetServer.Models;
using AspNetServer.Services.NewsApi.Json;
using AspNetServer.Services.NewsService;
using System.Text;
using System.Text.Json;

namespace AspNetServer.Services.NewsApi
{
    public class NewsApiService : INewsService
    {
        private const string URL_ENDPOINT = "https://newsapi.org/v2/top-headlines";
        private const string CATEGORY_PARAMETER = "category";
        private const string COUNTRY_PARAMETER = "country";
        private const string PAGE_SIZE_PARAMETER = "pageSize";
        private const string API_KEY_NAME_PARAMETER = "apiKey";
        private const string API_KEY = "791f1748a7614dd8af9626e9de00ce35";


        public async Task<List<News>> GetNews(NewsQueryData queryData)
        {
            string query = BuildUrlQuery(queryData);

            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Add("User-Agent", "NewsPetProject/1.0");

            string jsonString = await http.GetStringAsync(query);

            List<News>? news = GetNewsList(jsonString, queryData.Category, queryData.Country);

            return news;
        }

        private string BuildUrlQuery(NewsQueryData queryData)
        {
            StringBuilder urlBuilder = new StringBuilder($"{URL_ENDPOINT}?{API_KEY_NAME_PARAMETER}={API_KEY}&");

            urlBuilder.Append($"{PAGE_SIZE_PARAMETER}={queryData.PageSize}&");
            urlBuilder.Append($"{CATEGORY_PARAMETER}={queryData.Category}&");
            urlBuilder.Append($"{COUNTRY_PARAMETER}={queryData.Country}&");

            return urlBuilder.ToString();
        }

        private List<News> GetNewsList(string jsonString, string keyWord, string language)
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
