namespace UrlShortener.Service.Url.Interfaces
{
    public interface IDal
    {
        string GetShortenedFor(string originUrl);

        bool IsUrlStored(string originUrl);

        void StoreShortened(string originUrl, string shortened);
    }
}
