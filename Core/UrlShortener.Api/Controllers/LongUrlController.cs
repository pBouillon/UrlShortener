using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Http;
using UrlShortener.Common.Contracts.Url;
using UrlShortener.Common.Enums.Swagger;
using UrlShortener.Service.Url.Interfaces;

namespace UrlShortener.Api.Controllers
{
    [Route("url/long")]
    [ApiController]
    [Produces("application/json")]
    public class LongUrlController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public LongUrlController(IUrlService urlService)
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
            catch (HttpRequestException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(longUrl);
        }
    }
}
