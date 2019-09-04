using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Net.Http;
using UrlShortener.Common.Constants.Url;
using UrlShortener.Service.Url;
using UrlShortener.Service.Url.Exceptions;
using UrlShortener.Tests.Mocks;
using Xunit;

namespace UrlShortener.Tests.Services
{
    public class UrlShortenerTest
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void GetLongUrlFor_Invalid_EmptyUrl()
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
                    "because no empty url should be accepted"
                );
        }

        [Fact]
        public void GetShortUrlFor_Invalid_EmptyUrl()
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
        public void GetShortUrlFor_Valid_Http()
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
        public void GetShortUrlFor_Valid_Https()
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
        public void GetShortUrlFor_Valid_EnsureWorkFlow_NotStored()
        {
            // Arrange
            var tested = $"http://{_fixture.Create<string>()}";

            var mockedDal = new MockedDal(
                isUrlStored: false,
                getShortenedUrlFor: tested);

            var service = new UrlService(mockedDal.GetObject);

            // Act
            service.GetShortUrlFor(tested);

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

        [Fact]
        public void GetShortUrlFor_Valid_EnsureWorkFlow_AlreadyStored()
        {
            // Arrange
            var tested = $"http://{_fixture.Create<string>()}";

            var mockedDal = new MockedDal(
                isUrlStored: true,
                getShortenedUrlFor: tested);

            var service = new UrlService(mockedDal.GetObject);

            // Act
            service.GetShortUrlFor(tested);

            // Assert
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
    }
}
