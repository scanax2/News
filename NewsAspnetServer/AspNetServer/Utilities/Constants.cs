namespace AspNetServer.Utilities
{
    public static class Constants
    {
        public static class NewsApi
        {
            public const string URL_ENDPOINT = "https://newsapi.org/v2/top-headlines";
            public const string CATEGORY_PARAMETER = "category";
            public const string COUNTRY_PARAMETER = "country";
            public const string PAGE_SIZE_PARAMETER = "pageSize";
            public const string API_KEY_NAME_PARAMETER = "apiKey";
        }

        public static readonly Dictionary<string, string> NEWS_CATEGORIES = new Dictionary<string, string>
        {
            { "Trending", "any" },
            { "Business", "business" },
            { "Entertainment", "entertainment" },
            { "General", "general" },
            { "Health", "health" },
            { "Science", "science" },
            { "Sports", "sports" },
            { "Technology", "technology" },
        };

        public static readonly Dictionary<string, string> NEWS_COUNTRIES = new Dictionary<string, string>
        {
            { "World", "any" },
            { "Poland", "pl" },
            { "Germany", "de" },
            { "United Kingdom", "gb" },
            { "USA", "us" },
            { "Japan", "jp" },
            { "France", "fr" },
        };

        public const string EMPTY_RESULT = "No matching entries";
    }
}
