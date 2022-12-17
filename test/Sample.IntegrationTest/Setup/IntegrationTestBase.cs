using Xunit;

namespace Sample.IntegrationTest.Setup
{
    public class IntegrationTestBase : IClassFixture<IntegrationTestFactory>
    {
        protected readonly HttpClient _client;

        public IntegrationTestBase(IntegrationTestFactory factory)
        {
            _client = factory.CreateClient();
        }
    }
}
