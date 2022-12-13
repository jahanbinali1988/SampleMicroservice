using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Sample.Api;
using Sample.Api.Models.Meeting;
using Sample.Domain.Meetings;
using Sample.Infrastructure.Persistence;
using Sample.IntegrationTest.Creator;
using Sample.IntegrationTest.Setup;
using System.Net.Http.Json;
using Xunit;

namespace Sample.IntegrationTest.Test
{
    public class CreateMeetingTest : IntegrationTestBase
    {
        private readonly IntegrationTestFactory<Program, SampleDbContext> _integrationTestFactory;
        private readonly MeetingCreator _meetingCreator;

        public CreateMeetingTest(IntegrationTestFactory<Program, SampleDbContext> factory) : base(factory)
        {
            _integrationTestFactory = factory;
            var scope = factory.Services.CreateScope();
            _meetingCreator = scope.ServiceProvider.GetRequiredService<MeetingCreator>();
        }

        [Fact]
        public async Task Test()
        {
            // Arrange
            await _meetingCreator.AddMeetingsAsync();
            var client = _integrationTestFactory.CreateClient();

            // Act
            var meetingReponse = await client.GetFromJsonAsync<IList<MeetingEntity>>("meeting");

            // Assert
            Assert.NotNull(meetingReponse);
        }
    }
}
