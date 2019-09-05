using Moq;

namespace UrlShortener.Tests.Mocks
{
    public class MockBase<T> where T : class
    {
        protected readonly Mock<T> MockedObject;

        public Mock<T> GetMock
            => MockedObject;

        public T GetObject
            => MockedObject.Object;

        protected MockBase()
        {
            MockedObject = new Mock<T>();
        }

        protected void Setup() { }
    }
}
