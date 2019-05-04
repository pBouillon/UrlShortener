using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UrlShortener.Common.Contracts.Url;
using UrlShortener.Common.Enums.Swagger;
using UrlShortener.Service.Url;
using UrlShortener.Service.Url.Interfaces;

namespace UrlShortener.Api.Controllers
{
    [Route("url/short")]
    [ApiController]
    [Produces("application/json")]
    public class ShortUrlController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public ShortUrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet("{shortUrl}")]
        [SwaggerResponse(200, "The shortened URL was successfully fetched", typeof(UrlDto))]
        [SwaggerResponse(400, "The provided URL is invalid")]
        [SwaggerOperation(
            Summary = "Fetch the short URL for the long URL provided",
            Description = "If the long URL is not currently tracked, store it and generate a new short URL",
            OperationId = nameof(GetLongUrlFor),
            Tags = new[]
            {
                SwaggerTag.Url,
                SwaggerTag.LongUrl
            }
        )]
        public ActionResult<UrlDto> GetLongUrlFor([
                FromRoute,
                SwaggerParameter("Shortened url", Required = true)
            ] string shortUrl)
        {
            UrlDto shortenUrl;

            try
            {
                shortenUrl = _urlService.GetLongUrlFor(shortUrl);
            }
            catch (InvalidRequestException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(shortenUrl);
        }
    }
}
