/**
 * Author
 *      Pierre Bouillon - https://github.com/pBouillon
 *
 * Repository
 *      UrlShortener - https://github.com/pBouillon/UrlShortener
 *
 * License
 *      MIT - https://github.com/pBouillon/UrlShortener/blob/master/LICENSE
 */

namespace UrlShortener.Common.Constants.Swagger
{
    /// <summary>
    /// References Swagger Tags used in the Swagger UI and for the endpoints qualification
    /// </summary>
    public static class SwaggerTag
    {
        /// <summary>
        /// Endpoints related to URL manipulation
        /// </summary>
        public const string Url = "Url";

        /// <summary>
        /// Endpoints related to long URL manipulation
        /// </summary>
        public const string LongUrl = "Long url";

        /// <summary>
        /// Endpoints related to short URL manipulation
        /// </summary>
        public const string ShortUrl = "Short url";
    }
}
