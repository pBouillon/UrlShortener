using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using UrlShortener.Common.Contracts.Url;
using UrlShortener.Common.Enums.Swagger;

namespace UrlShortener.Api.Controllers
{
    [Route("url/long")]
    [ApiController]
    [Produces("application/json")]
    public class LongUrlController : ControllerBase
    {
        [HttpPost("{longUrl}")]
        [SwaggerResponse(200, "The long URL was successfully fetched", typeof(UrlDto))]
        [SwaggerResponse(400, "The short URL provided is invalid")]
        [SwaggerOperation(
            Summary = "Fetch the long URL for the short URL provided",
            Description = "If the short URL is not stored, throw an error",
            OperationId = nameof(GetShortenedUrlFor),
            Tags = new[] 
            {
                SwaggerTag.Url,
                SwaggerTag.ShortUrl
            }
        )]
        public ActionResult<UrlDto> GetShortenedUrlFor(
            [
                FromRoute, 
                SwaggerParameter("Url to convert", Required = true)
            ] string longUrl)
        {
            return Ok(new UrlDto
            {
                LongUrl = longUrl,
                ShortUrl = "short url"
            });
        }
    }
}
