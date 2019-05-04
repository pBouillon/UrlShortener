using AutoFixture;
using FluentAssertions;
using System;
using System.Net.Http;
using UrlShortener.Common.Contracts.Url;
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

        [Fact]
        public void GetShortUrlFor_Valid_EnsureImmutability()
        {
            var tested = _fixture.Create<string>();
            var service = new UrlService();

            var expected = service.GetShortUrlFor(tested);
            var actual = service.GetShortUrlFor(tested);

            actual
                .Should()
                .Match<UrlDto>(t => t.ShortUrl == expected.ShortUrl);
        }
    }
}
