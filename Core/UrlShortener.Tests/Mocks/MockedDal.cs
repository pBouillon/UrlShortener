/**
 * Author
 *      Pierre Bouillon - https://github.com/pBouillon
 *
 * Repository
 *      UrlShortener - https://github.com/pBouillon/UrlShortener
 *
 * License
 *      MIT - https://github.com/pBouillon/UrlShortener/blob/master/LICENSE
 */

using Moq;
using UrlShortener.Service.Url.Interfaces;

namespace UrlShortener.Tests.Mocks
{
    public class MockedDal : MockBase<IDal>
    {
        /// <summary>
        /// Provided value to return on `GetOriginalFrom` call
        /// </summary>
        private readonly string _getOriginalFor;

        /// <summary>
        /// Provided value to return on `GetShortenedUrlFor` call
        /// </summary>
        private readonly string _getShortenedUrlFor;

        /// <summary>
        /// Provided value to return on `IsShortCodeStored` call
        /// </summary>
        private readonly bool _isShortCodeStored;

        /// <summary>
        /// Provided status of the url storage to return on `IsUrlStored` call
        /// </summary>
        private bool _isUrlStored;

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <param name="getOriginalFor">Provided value to return on `GetOriginalFrom` call</param>
        /// <param name="getShortenedUrlFor">Provided value to return on `GetShortenedUrlFor` call</param>
        /// <param name="isShortCodeStored">Provided value to return on `IsShortCodeStored` call</param>
        /// <param name="isUrlStored">Provided status of the url storage to return on `IsUrlStored` call</param>
        public MockedDal(
            bool isUrlStored = false, 
            string getShortenedUrlFor = null, 
            bool isShortCodeStored = false, 
            string getOriginalFor = null)
        {
            _getOriginalFor = getOriginalFor;
            _getShortenedUrlFor = getShortenedUrlFor;
            _isShortCodeStored = isShortCodeStored;
            _isUrlStored = isUrlStored;

            Setup();
        }

        /// <summary>
        /// Setup the mock's behaviour
        /// </summary>
        protected sealed override void Setup()
        {
            MockedObject
                .Setup(_ => _.GetOriginalFrom(It.IsAny<string>()))
                .Returns(_getOriginalFor);

            MockedObject
                .Setup(_ => _.GetShortenedFor(It.IsAny<string>()))
                .Returns(_getShortenedUrlFor);

            MockedObject.Setup(_ => _.IsShortCodeStored(It.IsAny<string>()))
                .Returns(_isShortCodeStored);

            MockedObject.Setup(_ => _.IsUrlStored(It.IsAny<string>()))
                .Returns(_isUrlStored);

            MockedObject
                .Setup(_ => _.StoreShortened(It.IsAny<string>(), It.IsAny<string>()))
                .Callback(() => _isUrlStored = true);
        }
    }
}
