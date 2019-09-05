namespace UrlShortener.Common.Contracts.Url
{
    /// <summary>
    /// References URL data
    /// </summary>
    public class UrlDto
    {
        /// <summary>
        /// Original URL
        /// </summary>
        public string LongUrl { get; set; }

        /// <summary>
        /// Shortened version of the long URL
        /// </summary>
        public string ShortUrl { get; set; }
    }
}
