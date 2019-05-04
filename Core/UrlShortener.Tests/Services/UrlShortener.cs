using AutoFixture;
using FluentAssertions;
using System;
using System.Net.Http;
using UrlShortener.Service.Url;
using UrlShortener.Service.Url.Exceptions;
using Xunit;

namespace UrlShortener.Tests.Services
{
    public class UrlShortener
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void GetLongUrlFor_Invalid_EmptyUrl()
        {
            var tested = string.Empty;
            var service = new UrlService();

            Action actionPerformed = () => service.GetLongUrlFor(tested);

            actionPerformed
                .Should()
                .Throw<HttpRequestException>()
                .WithMessage(
                    ExceptionMessages.BadUrlProvided, 
                    "because no empty url should be accepted"
                );
        }

        [Fact]
        public void GetShortUrlFor_Invalid_EmptyUrl()
        {
            var tested = string.Empty;
            var service = new UrlService();

            Action actionPerformed = () => service.GetShortUrlFor(tested);

            actionPerformed
                .Should()
                .Throw<HttpRequestException>()
                .WithMessage(
                    ExceptionMessages.BadUrlProvided,
                    "because no empty url should be accepted"
                );
        }
    }
}
