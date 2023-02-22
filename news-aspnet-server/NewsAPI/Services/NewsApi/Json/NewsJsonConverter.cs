using AspNetServer.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AspNetServer.Services.NewsApi.Json
{
    public class NewsJsonConverter : JsonConverter<List<News>>
    {
        private const string TITLE_PROPERTY = "title";
        private const string IMAGE_URL_PROPERTY = "urlToImage";
        private const string URL_PROPERTY = "url";
        private const string DESCRIPTION_PROPERTY = "description";
        private const string PUBLISHED_AT_PROPERTY = "publishedAt";
        private readonly static string[] PROPERTIES_TO_SAVE = new string[]
        {
            TITLE_PROPERTY,
            IMAGE_URL_PROPERTY,
            URL_PROPERTY,
            DESCRIPTION_PROPERTY,
            PUBLISHED_AT_PROPERTY
        };


        public override List<News>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var news = new List<News>();
            var properties = new Dictionary<string, string?>();

            bool isNullValue = false;

            while (reader.Read())
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    continue;
                }

                string? name = reader.GetString();

                if (PROPERTIES_TO_SAVE.All(p => p != name))
                {
                    continue;
                }
                else if (name == null)
                {
                    continue;
                }

                reader.Read();

                string? value = reader.GetString();

                if (value == null)
                {
                    isNullValue = true;
                }

                properties.Add(name, value);

                if (properties.Count >= PROPERTIES_TO_SAVE.Length)
                {
                    if (!isNullValue)
                    {
                        var record = DeserializeRecord(properties!, isNullValue);
                        news.Add(record);
                    }
                    properties.Clear();
                    isNullValue = false;
                }
            }

            return news;
        }

        public override void Write(Utf8JsonWriter writer, List<News> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private News DeserializeRecord(Dictionary<string, string> properties, bool isNullValue)
        {
            DateTime publishedAt = default;
            try
            {
                publishedAt = DateTime.Parse(properties[PUBLISHED_AT_PROPERTY]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return CreateNews(properties, publishedAt);
        }

        private News CreateNews(Dictionary<string, string> propertiesValue, DateTime publishedAt)
        {
            News news = new News()
            {
                Title = propertiesValue[TITLE_PROPERTY],
                ImageUrl = propertiesValue[IMAGE_URL_PROPERTY],
                Url = propertiesValue[URL_PROPERTY],
                Description = propertiesValue[DESCRIPTION_PROPERTY],
                PublishedAt = publishedAt
            };
            return news;
        }
    }
}
