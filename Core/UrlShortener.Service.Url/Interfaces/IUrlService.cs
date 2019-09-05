using UrlShortener.Common.Contracts.Url;

namespace UrlShortener.Service.Url.Interfaces
{
    /// <summary>
    /// References the URL service's contract
    /// </summary>
    /// <see cref="UrlService"/>
    public interface IUrlService
    {
        /// <summary>
        /// Fetch the long URL from the short one provided
        /// </summary>
        /// <param name="shortUrl">Url code</param>
        /// <returns>The short URL and its original URL</returns>
        /// <see cref="UrlDto"/>
        UrlDto GetLongUrlFor(string shortUrl);

        /// <summary>
        /// Fetch the long URL from the short one provided
        /// </summary>
        /// <param name="longUrl">Original URL</param>
        /// <returns>The long URL and its associated short code</returns>
        /// <see cref="UrlDto"/>
        UrlDto GetShortUrlFor(string longUrl);
    }
}
