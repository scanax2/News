using NewsAPI.Models;
using System.Web;
using System.Text.Json;
using NewsAPI.Json.Converters;

namespace NewsAPI.Services.NewsService
{
    public class NewsService : INewsService
    {
        private const string API_KEY = "791f1748a7614dd8af9626e9de00ce35";
        private const string URL = "https://newsapi.org/v2/";
        private const string QUERY_EXAMPLE = "q=Apple&" +
          "from=2023-02-04&" +
          "sortBy=popularity&" +
          "page=1&" +
          "pageSize=100&" +
          "apiKey=791f1748a7614dd8af9626e9de00ce35";
        private const string URL_EXAMPLE = "https://newsapi.org/v2/everything?" + QUERY_EXAMPLE;
        private const string SEARCH_WORD_NAME = "q";
        private const string FROM_DATE_NAME = "from";
        private const string SORT_BY_NAME = "sortBy";
        private const string PAGE_NUMBER_NAME = "page";
        private const string PAGE_SIZE_NAME = "pageSize";
        private const string API_KEY_NAME = "apiKey";


        public async Task<List<News>> GetNews(int pageNumber, int pageSize, string keyWord, string from, SortType sortBy)
        {
            string url = BuildUrlQuery(pageNumber, pageSize, keyWord, from, sortBy);

            Console.Write(url);

            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Add("User-Agent", "NewsPetProject/1.0");
            string jsonString = await http.GetStringAsync(url);

            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new NewsJsonConverter());

            List<News>? news = JsonSerializer.Deserialize<List<News>>(jsonString, deserializeOptions);
            return news;
        }

        private string BuildUrlQuery(int pageNumber, int pageSize, string keyWord, string from, SortType sortBy)
        {
            var query = HttpUtility.ParseQueryString(QUERY_EXAMPLE);

            query[SEARCH_WORD_NAME] = keyWord;
            query[FROM_DATE_NAME] = from;
            query[SORT_BY_NAME] = sortBy.ToString();
            query[PAGE_NUMBER_NAME] = pageNumber.ToString();
            query[PAGE_SIZE_NAME] = pageSize.ToString();
            query[API_KEY_NAME] = API_KEY;

            string url = URL + "everything?";
            for (int i = 0; i < query.AllKeys.Length; i++)
            {
                string separator = "&";
                if (i == query.AllKeys.Length - 1)
                {
                    separator = "";
                }
                string? queryKey = query.AllKeys[i];
                string? queryValue = query[queryKey];
                if (string.IsNullOrEmpty(queryValue))
                {
                    continue;
                }

                url += $"{queryKey}={queryValue}{separator}";
            }

            return url;
        }
    }
}
