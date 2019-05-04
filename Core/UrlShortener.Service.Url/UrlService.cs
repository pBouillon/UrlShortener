using System;
using System.Net.Http;
using System.Security.Cryptography;
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
        /// <returns></returns>
        private string GetShortenString(string toShorten)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO doc
        /// </summary>
        /// <param name="toHash"></param>
        /// <returns></returns>
        private string GetMd5Hash(string toHash)
        {
            throw new NotImplementedException();
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

            return new UrlDto
            {
                LongUrl = longUrl,
                ShortUrl = GetShortenString(longUrl)
            };
        }
    }
}
