using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UrlShortener.Common.Contracts.Url;
using UrlShortener.Common.Enums.Swagger;

namespace UrlShortener.Api.Controllers
{
    [Route("url/short")]
    [ApiController]
    [Produces("application/json")]
    public class ShortUrlController : ControllerBase
    {
        [HttpGet("{shortUrl}")]
        [SwaggerResponse(200, "The shortened URL was successfully fetched", typeof(UrlDto))]
        [SwaggerResponse(201, "The shortened URL was generated", typeof(UrlDto))]
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
            return Ok(new UrlDto
            {
                LongUrl = "long url",
                ShortUrl = shortUrl
            });
        }
    }
}
