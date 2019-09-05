namespace UrlShortener.Service.Url.Interfaces
{
    public interface IDal
    {
        string GetOriginalFrom(string shortUrl);

        string GetShortenedFor(string originUrl);

        bool IsUrlStored(string originUrl);

        bool IsShortUrlStored(string shortUrl);

        void StoreShortened(string originUrl, string shortened);
    }
}
