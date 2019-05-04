using UrlShortener.Common.Contracts.Url;

namespace UrlShortener.Service.Url.Interfaces
{
    public interface IUrlService
    {
        UrlDto GetLongUrlFor(string shortUrl);

        UrlDto GetShortUrlFor(string longUrl);
    }
}
