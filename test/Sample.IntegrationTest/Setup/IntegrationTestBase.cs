using Sample.Api;
using Sample.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Sample.IntegrationTest.Setup
{
    public class IntegrationTestBase : IClassFixture<IntegrationTestFactory<Program, SampleDbContext>>
    {
        public readonly IntegrationTestFactory<Program, SampleDbContext> Factory;
        public readonly SampleDbContext DbContext;

        public IntegrationTestBase(IntegrationTestFactory<Program, SampleDbContext> factory)
        {
            Factory = factory;
            var scope = factory.Services.CreateScope();
            DbContext = scope.ServiceProvider.GetRequiredService<SampleDbContext>();
        }
    }
}
