using Moq;

namespace UrlShortener.Tests.Mocks
{
    /// <summary>
    /// References the default mock structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MockBase<T> where T : class
    {
        /// <summary>
        /// Mock object manipulated
        /// </summary>
        protected readonly Mock<T> MockedObject;

        /// <summary>
        /// Mock getter
        /// </summary>
        /// <returns>The mock object</returns>
        public Mock<T> GetMock
            => MockedObject;

        /// <summary>
        /// Mock type getter
        /// </summary>
        /// <returns>The type of the mocked object</returns>
        public T GetObject
            => MockedObject.Object;

        /// <summary>
        /// Default constructor
        /// </summary>
        protected MockBase()
        {
            MockedObject = new Mock<T>();
        }

        /// <summary>
        /// Method to be called to set up mock responses to its calls
        /// </summary>
        protected abstract void Setup();
    }
}
