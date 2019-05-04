using UrlShortener.Common.Contracts.Url;
using UrlShortener.Service.Url.Interfaces;

namespace UrlShortener.Service.Url
{
    public class UrlService : IUrlService
    {
        public UrlDto GetLongUrlFor(string shortUrl)
        {
            return new UrlDto
            {
                LongUrl = "Associated long url",
                ShortUrl = shortUrl
            };
        }

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
