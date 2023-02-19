using NewsAPI.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NewsAPI.Json.Converters
{
    public class NewsJsonConverter : JsonConverter<List<News>>
    {
        private const string TITLE_PROPERTY_NAME = "title";
        private const string IMAGE_URL_PROPERTY_NAME = "urlToImage";
        private const string URL_PROPERTY_NAME = "url";
        private const string DESCRIPTION_PROPERTY_NAME = "description";
        private const string PUBLISHED_AT_PROPERTY_NAME = "publishedAt";
        private readonly static string[] PROPERTIES_TO_SAVE = new string[]
        {
            TITLE_PROPERTY_NAME, 
            IMAGE_URL_PROPERTY_NAME, 
            URL_PROPERTY_NAME, 
            DESCRIPTION_PROPERTY_NAME,
            PUBLISHED_AT_PROPERTY_NAME
        };


        public override List<News>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<News> news = new List<News>();

            Dictionary<string, string> propertiesValue = new Dictionary<string, string>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string? propertyName = reader.GetString();

                    if (PROPERTIES_TO_SAVE.All(p => p != propertyName))
                    {
                        continue;
                    }
                    else if (propertyName == null)
                    {
                        continue;
                    }

                    reader.Read();

                    string? propertyValue = reader.GetString();
                    propertiesValue.Add(propertyName, propertyValue);

                    if (propertiesValue.Count >= PROPERTIES_TO_SAVE.Length)
                    {
                        DateTime publishedAt = default;
                        try
                        {
                            publishedAt = DateTime.Parse(propertiesValue[PUBLISHED_AT_PROPERTY_NAME]);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.StackTrace);
                        }

                        news.Add(new News()
                        {
                            Title = propertiesValue[TITLE_PROPERTY_NAME],
                            ImageUrl = propertiesValue[IMAGE_URL_PROPERTY_NAME],
                            Url = propertiesValue[URL_PROPERTY_NAME],
                            Description = propertiesValue[DESCRIPTION_PROPERTY_NAME],
                            PublishedAt = publishedAt
                        });
                        propertiesValue.Clear();
                    }
                }
            }
            return news;
        }

        public override void Write(Utf8JsonWriter writer, List<News> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
