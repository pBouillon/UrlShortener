namespace UrlShortener.Service.Url.Constants
{
    public class Database
    {
        public const string DatabaseName = "dev";

        public const string Host = "127.0.0.1";

        public const string Password = "pbouillon";

        public const int Port = 5433;

        public const string TableName = "urlshortener";

        public const string Username = "pbouillon";

        public class Columns
        {
            public const string LongUrl = "raw";

            public const string ShortUrl = "shortened";
        }
    }
}
