/**
 * Author
 *      Pierre Bouillon - https://github.com/pBouillon
 *
 * Repository
 *      UrlShortener - https://github.com/pBouillon/UrlShortener
 *
 * License
 *      MIT - https://github.com/pBouillon/UrlShortener/blob/master/LICENSE
 */

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Http;
using UrlShortener.Common.Constants.Routes;
using UrlShortener.Common.Constants.Swagger;
using UrlShortener.Common.Contracts.Url;
using UrlShortener.Service.Url.Interfaces;

namespace UrlShortener.Api.Controllers
{
    /// <summary>
    /// References the controller used for the conversion of long URL to short ones
    /// </summary>
    [Route(Routes.LongToShort)]
    [ApiController]
    [Produces("application/json")]
    public class LongUrlController : ControllerBase
    {
        /// <summary>
        /// Url service used for URL conversions
        /// </summary>
        private readonly IUrlService _urlService;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="urlService">Url service used for URL conversions</param>
        public LongUrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet("{longUrl}")]
        [SwaggerResponse(200, "The shortened URL was successfully fetched", typeof(UrlDto))]
        [SwaggerResponse(400, "The provided URL is invalid")]
        [SwaggerOperation(
            Summary = "Fetch the short URL for the long URL provided",
            Description = "If the long URL is not currently tracked, store it and generate a new short URL",
            OperationId = nameof(GetShortenedUrlFor),
            Tags = new[]
            {
                SwaggerTag.Url,
                SwaggerTag.LongUrl
            }
        )]
        public ActionResult<UrlDto> GetShortenedUrlFor([
                FromRoute,
                SwaggerParameter("Url to convert", Required = true)
            ] string longUrl)
        {
            UrlDto shortenUrl;

            // Fetch the non-formatted url
            longUrl = WebUtility.UrlDecode(longUrl);

            try
            {
                shortenUrl = _urlService.GetShortUrlFor(longUrl);
            }
            // Any error occuring would result in an error 400
            catch (HttpRequestException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(shortenUrl);
        }
    }
}
