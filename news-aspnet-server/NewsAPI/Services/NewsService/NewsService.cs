using NewsAPI.Models;
using System.Web;

namespace NewsAPI.Services.NewsService
{
    public class NewsService : INewsService
    {
        private const string API_KEY = "791f1748a7614dd8af9626e9de00ce35";
        private const string URL = "https://newsapi.org/v2/";
        private const string HEADLINE_QUERY_EXAMPLE = 
          "category=Health&" +
          "page=1&" +
          "country=us&" +
          "pageSize=100&" +
          "apiKey=791f1748a7614dd8af9626e9de00ce35";
        private const string URL_EXAMPLE = "https://newsapi.org/v2/everything?" + HEADLINE_QUERY_EXAMPLE;
        private const string CATEGORY_WORD_NAME = "category";
        private const string COUNTRY_NAME = "country";
        private const string SORT_BY_NAME = "sortBy";
        private const string PAGE_NUMBER_NAME = "page";
        private const string PAGE_SIZE_NAME = "pageSize";
        private const string API_KEY_NAME = "apiKey";


        public async Task<List<News>> GetNews(NewsQuery newsQuery)
        {
            string url = BuildUrlQuery(newsQuery);

            Console.Write(url);

            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Add("User-Agent", "NewsPetProject/1.0");
            string jsonString = await http.GetStringAsync(url);

            NewsFactory newsFactory = new NewsFactory();

            List<News>? news = newsFactory.GetNewsList(jsonString, newsQuery.Category, newsQuery.Country);
            
            return news;
        }

        private string BuildUrlQuery(NewsQuery newsQuery)
        {
            var query = HttpUtility.ParseQueryString(HEADLINE_QUERY_EXAMPLE);

            query[CATEGORY_WORD_NAME] = newsQuery.Category;
            query[COUNTRY_NAME] = newsQuery.Country;
            query[PAGE_NUMBER_NAME] = newsQuery.PageNumber.ToString();
            query[PAGE_SIZE_NAME] = newsQuery.PageSize.ToString();
            query[API_KEY_NAME] = API_KEY;

            string url = URL + "top-headlines?";//"everything?";
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
