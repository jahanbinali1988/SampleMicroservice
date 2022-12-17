using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sample.Api;
using Sample.Infrastructure.Persistence;
using Sample.IntegrationTest.Creator;
using Xunit;

namespace Sample.IntegrationTest.Setup
{
    public class IntegrationTestFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
    {
        private readonly MsSqlTestcontainer _dbContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
                .WithDatabase(new MsSqlTestcontainerConfiguration
                {
                    Password = "J@han14153",
                    Database = "Test"
                })
                .WithImage("mcr.microsoft.com/mssql/server")
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithEnvironment("SA_PASSWORD", "J@han14153")
                .WithEnvironment("MSSQL_SA_PASSWORD", "J@han14153")
                .WithEnvironment("MSSQL_PID", "Express")
                .WithCleanUp(true)
                .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(SampleDbContext));
                services.AddDbContext<SampleDbContext>(_ => _.UseSqlServer(_dbContainer.ConnectionString));
                //services.AddTransient<MeetingCreator>();
            });
        }

        public async Task InitializeAsync() => await _dbContainer.StartAsync();

        public new async Task DisposeAsync() => await _dbContainer.DisposeAsync();
    }
}
