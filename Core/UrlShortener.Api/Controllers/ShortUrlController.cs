using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Http;
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

            try
            {
                shortenUrl = _urlService.GetShortUrlFor(longUrl);
            }
            catch (HttpRequestException e)
            {
                return BadRequest(e.Message);
            }

            // TODO: find the correct way to do this (no hard coding)
            shortenUrl.ShortUrl = $"http://{HttpContext.Request.Host.Value}/" +
                $"url/long/{shortenUrl.ShortUrl}";

            return Ok(shortenUrl);
        }
    }
}
