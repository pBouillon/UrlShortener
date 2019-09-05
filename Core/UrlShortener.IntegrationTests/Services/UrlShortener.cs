using AutoFixture;
using Moq;
using UrlShortener.Service.Url;
using UrlShortener.Tests.Mocks;
using Xunit;

namespace UrlShortener.IntegrationTests.Services
{
    public class UrlShortener
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void GetShortUrlFor_GenerateTwice()
        {
            // Arrange
            var tested = $"http://{_fixture.Create<string>()}";

            var mockedDal = new MockedDal(
                isUrlStored: false,
                getShortenedUrlFor: tested);

            var service = new UrlService(mockedDal.GetObject);

            // Act
            service.GetShortUrlFor(tested);

            // Assert initial state
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

            // Act
            service.GetShortUrlFor(tested);

            // Assert final state
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
