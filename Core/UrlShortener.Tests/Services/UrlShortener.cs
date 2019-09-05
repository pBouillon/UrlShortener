using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Net.Http;
using UrlShortener.Common.Constants.Url;
using UrlShortener.Common.Contracts.Url;
using UrlShortener.Service.Url;
using UrlShortener.Tests.Mocks;
using Xunit;

namespace UrlShortener.Tests.Services
{
    public class UrlShortenerTest
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void GetLongUrlFor_EmptyCode()
        {
            // Arrange
            var tested = string.Empty;

            var mockedDal = new MockedDal();
            var service = new UrlService(mockedDal.GetObject);

            // Act
            Action actionPerformed = () 
                => service.GetLongUrlFor(tested);

            // Assert
            actionPerformed
                .Should()
                .Throw<HttpRequestException>()
                .WithMessage(
                    ExceptionMessages.BadUrlProvided, 
                    "because no empty url should be accepted");
        }

        [Fact]
        public void GetLongUrlFor_NonStoredCode()
        {
            // Arrange
            var tested = _fixture.Create<string>();

            var mockedDal = new MockedDal();
            var service = new UrlService(mockedDal.GetObject);

            // Act
            Action actionPerformed = ()
                => service.GetLongUrlFor(tested);

            // Assert
            actionPerformed
                .Should()
                .Throw<HttpRequestException>()
                .WithMessage(
                    ExceptionMessages.UnknownShortUrl,
                    "because requesting a non-existing code should raise an exception");
        }

        [Fact]
        public void GetLongUrlFor_ValidCode()
        {
            // Arrange
            var urlDto = new UrlDto
            {
                LongUrl = _fixture.Create<string>(),
                ShortUrl = _fixture.Create<string>()
            };

            var mockedDal = new MockedDal(
                isShortCodeStored: true,
                getOriginalFor: urlDto.LongUrl);

            var service = new UrlService(mockedDal.GetObject);

            // Act
            var response = service.GetLongUrlFor(urlDto.ShortUrl);

            // Assert
            response
                .Should()
                .BeEquivalentTo(
                    urlDto,
                    "because the same long URL is returned by the program when using its code");
        }

        [Fact]
        public void GetShortUrlFor_EmptyUrl()
        {
            // Arrange
            var tested = string.Empty;

            var mockedDal = new MockedDal();
            var service = new UrlService(mockedDal.GetObject);

            // Act
            Action actionPerformed = () 
                => service.GetShortUrlFor(tested);

            // Assert
            actionPerformed
                .Should()
                .Throw<HttpRequestException>()
                .WithMessage(
                    ExceptionMessages.BadUrlProvided,
                    "because no empty url should be accepted");
        }

        [Fact]
        public void GetShortUrlFor_Http()
        {
            // Arrange
            var tested = $"http://{_fixture.Create<string>()}";

            var mockedDal = new MockedDal();
            var service = new UrlService(mockedDal.GetObject);

            // Act
            var actual = service.GetShortUrlFor(tested);

            // Assert
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
        public void GetShortUrlFor_Https()
        {
            // Arrange
            var tested = $"https://{_fixture.Create<string>()}";

            var mockedDal = new MockedDal();
            var service = new UrlService(mockedDal.GetObject);

            // Act
            var actual = service.GetShortUrlFor(tested);

            // Assert
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
        public void GetShortUrlFor_UrlAlreadyStored()
        {
            // Arrange
            var urlDto = new UrlDto
            {
                LongUrl = $"http://{_fixture.Create<string>()}",
                ShortUrl = _fixture.Create<string>()
            };

            var mockedDal = new MockedDal(
                isUrlStored: true,
                getShortenedUrlFor: urlDto.ShortUrl);

            var service = new UrlService(mockedDal.GetObject);

            // Act
            var response = service.GetShortUrlFor(urlDto.LongUrl);

            // Assert
            response
                .Should()
                .BeEquivalentTo(
                    urlDto,
                    "because the fetched values should match the original ones");

            mockedDal.GetMock
                .Verify(_ =>
                    _.GetShortenedFor(It.IsAny<string>()),
                    Times.Once);

            mockedDal.GetMock
                .Verify(_ =>
                    _.IsUrlStored(It.IsAny<string>()),
                    Times.Once);

            mockedDal.GetMock
                .Verify(_ =>
                    _.StoreShortened(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Never);
        }

        [Fact]
        public void GetShortUrlFor_UrlNotStored()
        {
            // Arrange
            var urlDto = new UrlDto
            {
                LongUrl = $"http://{_fixture.Create<string>()}",
                ShortUrl = string.Empty
            };

            var mockedDal = new MockedDal(
                isUrlStored: false,
                getShortenedUrlFor: urlDto.ShortUrl);

            var service = new UrlService(mockedDal.GetObject);

            // Act
            service.GetShortUrlFor(urlDto.LongUrl);

            // Assert
            mockedDal.GetMock
                .Verify(_ =>
                    _.GetShortenedFor(It.IsAny<string>()),
                    Times.Never);

            mockedDal.GetMock
                .Verify(_ =>
                    _.IsUrlStored(It.IsAny<string>()),
                    Times.Once);

            mockedDal.GetMock
                .Verify(_ =>
                    _.StoreShortened(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Once);
        }
    }
}
