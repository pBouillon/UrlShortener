using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Http;
using UrlShortener.Common.Constants.Routes;
using UrlShortener.Common.Constants.Swagger;
using UrlShortener.Common.Contracts.Url;
using UrlShortener.Service.Url.Interfaces;

namespace UrlShortener.Api.Controllers
{
    /// <summary>
    /// References the controller used for the conversion of short URL to long ones
    /// </summary>
    [Route(Routes.ShortToLong)]
    [ApiController]
    [Produces("application/json")]
    public class ShortUrlController : ControllerBase
    {
        /// <summary>
        /// Url service used for URL conversions
        /// </summary>
        private readonly IUrlService _urlService;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="urlService">Url service used for URL conversions</param>
        public ShortUrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet("{shortUrl}")]
        [SwaggerResponse(200, "The long url was successfully fetched", typeof(UrlDto))]
        [SwaggerResponse(400, "The short url provided is invalid")]
        [SwaggerOperation(
            Summary = "Fetch the long url for the short URL provided",
            Description = "If the short url is not stored, throw an error",
            OperationId = nameof(GetLongUrlFor),
            Tags = new[]
            {
                SwaggerTag.Url,
                SwaggerTag.ShortUrl
            }
        )]
        public ActionResult<UrlDto> GetLongUrlFor(
            [
                FromRoute,
                SwaggerParameter("Url to convert", Required = true)
            ] string shortUrl)
        {
            UrlDto longUrl;

            try
            {
                longUrl = _urlService.GetLongUrlFor(shortUrl);
            }
            // Any error occuring would result in an error 400
            catch (HttpRequestException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(longUrl);
        }
    }
}
