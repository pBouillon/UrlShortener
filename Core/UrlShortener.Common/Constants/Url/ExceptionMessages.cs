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

namespace UrlShortener.Common.Constants.Url
{
    /// <summary>
    /// References custom exception messages
    /// </summary>
    public class ExceptionMessages
    {
        /// <summary>
        /// Message provided with the exception when the system is asked to process a bad URL
        /// </summary>
        public const string BadUrlProvided = "The provided url shouldn't be empty.";

        /// <summary>
        /// Message provided with the exception when the system is asked to process an unknown short URL
        /// </summary>
        public const string UnknownShortUrl = "This shortened url does not exists in the system.";
    }
}
