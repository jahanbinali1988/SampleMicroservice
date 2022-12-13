using Docker.DotNet.Models;
using Sample.Domain.Meetings;
using Sample.Infrastructure.Persistence;

namespace Sample.IntegrationTest.Creator
{
    public class MeetingCreator
    {
        private readonly SampleDbContext _curatedArtDbContext;

        public MeetingCreator(SampleDbContext curatedArtDbContext)
        {
            _curatedArtDbContext = curatedArtDbContext;
        }

        public async Task AddMeetingsAsync()
        {
            var meetings = new List<MeetingEntity>
            {
                MeetingEntity.CreateForTest(9224957626, DateTimeOffset.Now.AddDays(10), DateTimeOffset.Now.AddDays(10).AddMinutes(20), Guid.NewGuid())
            };

            await _curatedArtDbContext.AddRangeAsync(meetings);
            await _curatedArtDbContext.SaveChangesAsync();
        }

        public async Task<Guid> AddMeetingAsync()
        {
            var meeting = MeetingEntity.CreateForTest(9224957626, DateTimeOffset.Now.AddDays(10), DateTimeOffset.Now.AddDays(10).AddMinutes(20), Guid.NewGuid());

            await _curatedArtDbContext.AddAsync(meeting);
            await _curatedArtDbContext.SaveChangesAsync();

            return meeting.Id;
        }

        public static MeetingEntity CreateMeetingkDto()
        {
            return MeetingEntity.CreateForTest(9224957626, DateTimeOffset.Now.AddDays(10), DateTimeOffset.Now.AddDays(10).AddMinutes(20), Guid.NewGuid());
        }
    }
}
