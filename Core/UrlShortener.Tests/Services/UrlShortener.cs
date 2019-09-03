using AutoFixture;
using FluentAssertions;
using System;
using System.Net.Http;
using UrlShortener.Common.Constants.Url;
using UrlShortener.Common.Contracts.Url;
using UrlShortener.Service.Url;
using UrlShortener.Service.Url.Exceptions;
using Xunit;

namespace UrlShortener.Tests.Services
{
    public class UrlShortenerTest
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
                    "because no empty url should be accepted");
        }

        [Fact]
        public void GetShortUrlFor_Valid_Http()
        {
            var tested = $"http://{_fixture.Create<string>()}";
            var service = new UrlService();

            var actual = service.GetShortUrlFor(tested);

            actual
                .LongUrl
                .Should()
                .Be(
                    tested,
                    "because no treatment is done on the long url provided");

            actual
                .ShortUrl
                .Should()
                .HaveLength(
                    UrlGeneration.GeneratedSequenceLength,
                    "because the shorten url should have a normalized size length");
        }

        [Fact]
        public void GetShortUrlFor_Valid_Https()
        {
            var tested = $"https://{_fixture.Create<string>()}";
            var service = new UrlService();

            var actual = service.GetShortUrlFor(tested);

            actual
                .LongUrl
                .Should()
                .Be(
                    tested,
                    "because no treatment is done on the long url provided");

            actual
                .ShortUrl
                .Should()
                .HaveLength(
                    UrlGeneration.GeneratedSequenceLength,
                    "because the shorten url should have a normalized size length");
        }

        [Fact]
        public void GetShortUrlFor_Valid_EnsureHttpImmutability()
        {
            var tested = $"http://{_fixture.Create<string>()}";
            var service = new UrlService();

            var expected = service.GetShortUrlFor(tested);
            var actual = service.GetShortUrlFor(tested);

            actual
                .Should()
                .Match<UrlDto>(t => t.ShortUrl == expected.ShortUrl);
        }

        [Fact]
        public void GetShortUrlFor_Valid_EnsureHttpsImmutability()
        {
            var tested = $"https://{_fixture.Create<string>()}";
            var service = new UrlService();

            var expected = service.GetShortUrlFor(tested);
            var actual = service.GetShortUrlFor(tested);

            actual
                .Should()
                .Match<UrlDto>(t => t.ShortUrl == expected.ShortUrl);
        }
    }
}
