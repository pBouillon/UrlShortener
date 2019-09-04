using Moq;
using UrlShortener.Service.Url.Interfaces;

namespace UrlShortener.Tests.Mocks
{
    public class MockedDal : MockBase<IDal>
    {
        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <param name="isUrlStored"></param>
        /// <param name="getShortenedUrlFor"></param>
        public MockedDal(bool isUrlStored = false, string getShortenedUrlFor = null)
        {
            Setup(isUrlStored, getShortenedUrlFor);
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <param name="isUrlStored"></param>
        /// <param name="getShortenedUrlFor"></param>
        protected void Setup(bool isUrlStored, string getShortenedUrlFor)
        {
            MockedObject
                .Setup(_ => _.GetShortenedFor(It.IsAny<string>()))
                .Returns(getShortenedUrlFor);

            MockedObject.Setup(_ => _.IsUrlStored(It.IsAny<string>()))
                .Returns(isUrlStored);

            MockedObject
                .Setup(_ => _.StoreShortened(It.IsAny<string>(), It.IsAny<string>()))
                .Callback(() => isUrlStored = true);
        }
    }
}
