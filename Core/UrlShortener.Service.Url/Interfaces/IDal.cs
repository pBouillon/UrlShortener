namespace UrlShortener.Service.Url.Interfaces
{
    /// <summary>
    /// References the data access layer's contract
    /// </summary>
    /// <see cref="Dal"/>
    public interface IDal
    {
        /// <summary>
        /// Get the original URL from its shortened version
        /// </summary>
        /// <param name="shortUrl">The short URL's code</param>
        /// <returns>The original URL</returns>
        string GetOriginalFrom(string shortUrl);

        /// <summary>
        /// Get the shortened URL from its long version
        /// </summary>
        /// <param name="originUrl">The original URL</param>
        /// <returns>The shortened URL code</returns>
        string GetShortenedFor(string originUrl);

        /// <summary>
        /// Check if the original URL is currently tracked by the system
        /// </summary>
        /// <param name="originUrl">Original URL to check</param>
        /// <returns>True if stored; false otherwise</returns>
        bool IsUrlStored(string originUrl);

        /// <summary>
        /// Check if the shortened URL code is currently stored in the database
        /// </summary>
        /// <param name="shortUrl">The short URL's code</param>
        /// <returns>True if stored; false otherwise</returns>
        bool IsShortUrlStored(string shortUrl);

        /// <summary>
        /// Store the original URL and its associated short code
        /// </summary>
        /// <param name="originUrl">Original URL</param>
        /// <param name="shortened">Associated short code</param>
        void StoreShortened(string originUrl, string shortened);
    }
}
