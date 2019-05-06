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
        /// Default constructor for the UrlService
        /// </summary>
        public UrlService() { }

        /// <summary>
        /// Given a string, return a random sequence of a specific length
        /// </summary>
        /// <param name="toHash"></param>
        /// <see cref="UrlGeneration.GeneratedSequenceLength"/>
        /// <returns>A shorten sequence related to `toShorten`</returns>
        private string GetShortenString(string toShorten)
        {
            // TODO: add db storage / search
            return GetMd5Hash(toShorten);
        }

        /// <summary>
        /// Get the MD5 hash of a given string
        /// </summary>
        /// <param name="toHash">The string to hash</param>
        /// <returns>The MD5 hash of `toHash`</returns>
        private string GetMd5Hash(string toHash)
        {
            var generated = new StringBuilder();

            // Using the MD5 hashing algorithm
            using (MD5 md5Hash = MD5.Create())
            {
                // Extracting the bytes resulting of the hashed string
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(toHash));

                // Gathering `GeneratedSequenceLength` chars from it
                for (int i = 0; i < data.Length; ++i)
                {
                    // Converting byte to hexadecimal
                    generated.Append(data[i].ToString("x2"));
                }
            }

            // Returns the builded string
            return generated.ToString().Substring(0, UrlGeneration.GeneratedSequenceLength);
        }

        /// <summary>
        /// Test the validity of an uri
        /// </summary>
        /// <param name="uri">uri to test</param>
        /// <returns>true if valid</returns>
        private bool IsValidUri(Uri uri)
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
