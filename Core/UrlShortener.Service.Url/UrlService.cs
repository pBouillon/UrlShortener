using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using UrlShortener.Common.Constants.Url;
using UrlShortener.Common.Contracts.Url;
using UrlShortener.Service.Url.Exceptions;
using UrlShortener.Service.Url.Interfaces;

namespace UrlShortener.Service.Url
{
    public class UrlService : IUrlService
    {
        /// <summary>
        /// TODO: doc
        /// </summary>
        private readonly IDal _dal;

        /// <summary>
        /// Default constructor for the UrlService
        /// </summary>
        public UrlService()
        { 
            _dal = new Dal();
        }

        /// <summary>
        /// Given a string, return a random sequence of a specific length
        /// </summary>
        /// <param name="toShorten"></param>
        /// <see cref="UrlGeneration.GeneratedSequenceLength"/>
        /// <returns>A shorten sequence related to `toShorten`</returns>
        private string GetShortenString(string toShorten)
        {
            string shortUrl;

            if (!_dal.IsUrlStored(toShorten))
            {
                shortUrl = GetMd5Hash(toShorten);
                _dal.StoreShortened(shortUrl, toShorten);
            }
            else
            {
                shortUrl = _dal.GetShortenedFor(toShorten);
            }
            
            return shortUrl;
        }

        /// <summary>
        /// Get the MD5 hash of a given string
        /// </summary>
        /// <param name="toHash">The string to hash</param>
        /// <returns>The MD5 hash of `toHash`</returns>
        private static string GetMd5Hash(string toHash)
        {
            var generated = new StringBuilder();

            // Using the MD5 hashing algorithm
            using (var md5Hash = MD5.Create())
            {
                // Extracting the bytes resulting of the hashed string
                var encodedChain = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(toHash));

                // Gathering `GeneratedSequenceLength` chars from it
                foreach (var encodedChar in encodedChain)
                {
                    // Converting byte to hexadecimal
                    generated.Append(encodedChar.ToString("x2"));
                }
            }

            // Returns the built string
            return generated.ToString().Substring(0, UrlGeneration.GeneratedSequenceLength);
        }

        /// <summary>
        /// Test the validity of an uri
        /// </summary>
        /// <param name="uri">uri to test</param>
        /// <returns>true if valid</returns>
        private static bool IsValidUri(Uri uri)
        {
            return uri.Scheme == Uri.UriSchemeHttp
                || uri.Scheme == Uri.UriSchemeHttps;
        }

        /// <summary>
        /// Given a short url, fetch the long one
        /// </summary>
        /// <param name="shortUrl">Short url provided</param>
        /// <returns>The long url in a `UrlDto`</returns>
        public UrlDto GetLongUrlFor(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
            {
                throw new HttpRequestException(ExceptionMessages.BadUrlProvided);
            }

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
            if (string.IsNullOrEmpty(longUrl))
            {
                throw new HttpRequestException(ExceptionMessages.BadUrlProvided);
            }

            var convertedUrl = new Uri(longUrl, UriKind.Absolute);

            if (!IsValidUri(convertedUrl))
            {
                throw new HttpRequestException(ExceptionMessages.BadUrlProvided);
            }

            return new UrlDto
            {
                LongUrl = longUrl,
                ShortUrl = GetShortenString(longUrl)
            };
        }
    }
}
