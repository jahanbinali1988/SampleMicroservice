using Sample.Domain.Meetings.DomainServices;
using Sample.Domain.Shared;
using Sample.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Sample.Domain.Meetings;

namespace Sample.Infrastructure.DomainService.Meetings
{
    public class BookMeetingService : IBookMeetingService
    {
        private readonly SampleDbContext _dbContext;
        public BookMeetingService(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsValidAsync(Guid meetingId)
        {
            var meeting = await _dbContext.Set<MeetingEntity>().FirstOrDefaultAsync(c => c.Id.Equals(meetingId));

            if (meeting.StartDate.Subtract(DateTimeOffset.Now) > new TimeSpan(2, 0, 0) && meeting.Status != MeetingStatus.Booked)
                return false;
            else
                return true;
        }
    }
}
