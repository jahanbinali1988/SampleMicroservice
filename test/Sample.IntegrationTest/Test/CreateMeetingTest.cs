using Sample.IntegrationTest.Setup;
using System.Net.Http.Json;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Sample.Domain.Meetings;
using Sample.IntegrationTest.Creator;

namespace Sample.IntegrationTest.Test
{
    public class CreateMeetingTest : IntegrationTestBase
    {
        // private readonly MeetingCreator _meetingCreator;

        public CreateMeetingTest(IntegrationTestFactory apiFactory) : base(apiFactory)
        {
            //var scope = apiFactory.Services.CreateScope();
            //_meetingCreator = scope.ServiceProvider.GetRequiredService<MeetingCreator>();
        }

        [Fact]
        public async Task Test()
        {
            // Arrange
            //await _meetingCreator.AddMeetingsAsync();

            // Act
            var meetingReponse = await _client.GetFromJsonAsync<IList<MeetingEntity>>("meeting");

            // Assert
            Assert.NotNull(meetingReponse);
        }
    }
}
