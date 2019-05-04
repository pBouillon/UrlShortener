using UrlShortener.Common.Contracts.Url;
using UrlShortener.Service.Url.Interfaces;

namespace UrlShortener.Service.Url
{
    public class UrlService : IUrlService
    {
        /// <summary>
        /// Given a short url, fetch the long one
        /// </summary>
        /// <param name="shortUrl">Short url provided</param>
        /// <returns>The long url in a `UrlDto`</returns>
        public UrlDto GetLongUrlFor(string shortUrl)
        {
            return new UrlDto
            {
                LongUrl = "Associated long url",
                ShortUrl = shortUrl
            };
        }

        /// <summary>
        /// Given a long url, fetch or create the short one
        /// </summary>
        /// <param name="longUrl">Long url provided</param>
        /// <returns>The short url in a `UrlDto`</returns>
        public UrlDto GetShortUrlFor(string longUrl)
        {
            return new UrlDto
            {
                LongUrl = longUrl,
                ShortUrl = "Associated short url"
            };
        }
    }
}
