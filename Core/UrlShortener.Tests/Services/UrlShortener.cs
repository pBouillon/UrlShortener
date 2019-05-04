using AutoFixture;
using FluentAssertions;
using System;
using UrlShortener.Service.Url;
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
                .Throw<InvalidRequestException>("because no empty url should be accepted");
        }

        [Fact]
        public void GetShortUrlFor_Invalid_EmptyUrl()
        {
            var tested = string.Empty;
            var service = new UrlService();

            Action actionPerformed = () => service.GetShortUrlFor(tested);

            actionPerformed
                .Should()
                .Throw<InvalidRequestException>("because no empty url should be accepted");
        }
    }
}
