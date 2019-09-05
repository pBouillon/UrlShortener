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

namespace UrlShortener.Common.Constants.Routes
{
    /// <summary>
    /// References endpoints addresses
    /// </summary>
    public class Routes
    {
        /// <summary>
        /// Endpoint for the controller converting long urls to short ones
        /// </summary>
        public const string LongToShort = "url/long";

        /// <summary>
        /// Endpoint for the controller converting shot urls to long ones
        /// </summary>
        public const string ShortToLong = "url/long";
    }
}
