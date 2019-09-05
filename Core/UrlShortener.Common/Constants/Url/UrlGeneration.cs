namespace UrlShortener.Common.Constants.Url
{
    /// <summary>
    /// Reference the URL generation's constants
    /// </summary>
    public class UrlGeneration
    {
        /// <summary>
        /// Length of the short url
        /// <remarks>
        /// Increasing this number will help to avoid collision but will increase storage needs
        /// </remarks>
        /// </summary>
        public const int GeneratedSequenceLength = 7;
    }
}
