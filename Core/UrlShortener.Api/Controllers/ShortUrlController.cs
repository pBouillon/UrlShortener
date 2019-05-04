using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Common.Contracts.Url;

namespace UrlShortener.Api.Controllers
{
    [Route("url/short")]
    [ApiController]
    [Produces("application/json")]
    public class ShortUrlController : ControllerBase
    {
        [HttpGet("{shortUrl}")]
        public ActionResult<UrlDto> GetLongUrlFor([FromRoute] string longUrl)
        {
            return Ok(new UrlDto
            {
                LongUrl = longUrl,
                ShortUrl = "short url"
            });
        }
    }
}
