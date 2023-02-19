using NewsAPI.Json.Converters;
using System.Text.Json;

namespace NewsAPI.Models
{
    public class NewsFactory
    {
        public List<News> GetNewsList(string jsonString, string keyWord, string language)
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
