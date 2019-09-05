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

namespace UrlShortener.Common.Contracts.Configuration
{
    /// <summary>
    /// References Swagger license header's data
    /// </summary>
    public class LicenseDto
    {
        /// <summary>
        /// License name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// License text URL
        /// </summary>
        public string Url { get; set; }
    }
}
