using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Common.Contracts.Url
{
    public class UrlDto
    {
        public string LongUrl { get; set; }

        public string ShortUrl { get; set; }
    }
}
