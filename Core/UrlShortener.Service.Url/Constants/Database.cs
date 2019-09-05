namespace UrlShortener.Service.Url.Constants
{
    /// <summary>
    /// References constants related to the database
    /// <remarks>
    /// The current used database is the latest postgres's docker image
    /// </remarks>
    /// </summary>
    public class Database
    {
        /// <summary>
        /// Database name
        /// </summary>
        public const string DatabaseName = "dev";

        /// <summary>
        /// Database server address
        /// </summary>
        public const string Host = "127.0.0.1";

        /// <summary>
        /// Database user password
        /// </summary>
        public const string Password = "pbouillon";

        /// <summary>
        /// Database server port
        /// </summary>
        public const int Port = 5433;

        /// <summary>
        /// Database table name
        /// </summary>
        public const string TableName = "urlshortener";

        /// <summary>
        /// Database user login
        /// </summary>
        public const string Username = "pbouillon";

        /// <summary>
        /// References database schema
        /// </summary>
        public class Columns
        {
            /// <summary>
            /// Database column storing original URLs
            /// </summary>
            public const string LongUrl = "raw";

            /// <summary>
            /// Database column storing the shortened version of the provided URLs
            /// </summary>
            public const string ShortUrl = "shortened";
        }
    }
}
