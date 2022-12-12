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
    public class ReserveMeetingService : IReserveMeetingService
    {
        private readonly SampleDbContext _dbContext;
        public ReserveMeetingService(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsValidAsync(Guid meetingId)
        {
            var meeting = await _dbContext.Set<MeetingEntity>().FirstOrDefaultAsync(c => c.Id.Equals(meetingId));

            if (meeting.StartDate.Subtract(DateTimeOffset.Now) > new TimeSpan(2, 0, 0) &&
                !_dbContext.Set<MeetingEntity>().Any(c => c.StartDate == meeting.StartDate && c.HostMsisdn == meeting.HostMsisdn && meeting.Status == MeetingStatus.Booked)
                )
                return true;
            else
                return false;
        }
    }
}
