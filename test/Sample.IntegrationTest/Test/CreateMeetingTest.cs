using Bogus;
using FluentAssertions;
using Sample.Api.Models.Meeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sample.IntegrationTest.Test
{
    public class CreateMeetingTest : IClassFixture<Bootstraper>
    {
        private readonly HttpClient _client;
        private readonly Faker<CreateMeetingRequest> _meetingGenerator = new Faker<CreateMeetingRequest>()
            .RuleFor(x => x.HostMsisdn, 123)
            .RuleFor(x => x.StartDate, faker => faker.Date.FutureOffset())
            .RuleFor(x => x.EndDate, faker => faker.Date.FutureOffset().AddMinutes(20));

        public CreateMeetingTest(Bootstraper bootstrapper)
        {
            _client = bootstrapper.CreateClient();
        }

        [Fact]
        public async Task Test()
        {
            // Arrange
            var meeting = _meetingGenerator.Generate();

            // Act
            var response = await _client.PostAsJsonAsync("meeting", meeting);

            // Assert
            var customerResponse = await response.Content.ReadFromJsonAsync<MeetingViewModel>();
            customerResponse.Should().BeEquivalentTo(meeting);
        }
    }
}
