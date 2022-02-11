using Sample.Domain.Meeting;
using Sample.Infrastructure.Persistence;
using Sample.SharedKernel.Application;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Infrastructure.Domain.Meeting
{
    public class MeetingRepository : RepositoryBase<MeetingEntity>, IMeetingRepository
    {
        public MeetingRepository(SampleDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Pagination<MeetingEntity>> GetListByHostMsisdnAsync(long msisdn, int skip, int take, CancellationToken token)
        {
            var count = await base.DbContext.Meetings.CountAsync(c => c.HostMsisdn == msisdn);
            if (count == 0)
                return new Pagination<MeetingEntity>()
                {
                    Items = new List<MeetingEntity>(),
                    TotalItems = 0
                };

            return new Pagination<MeetingEntity>()
            {
                Items = await base.DbContext.Meetings.Where(c => c.HostMsisdn == msisdn).Skip(take * skip).Take(take).ToListAsync(),
                TotalItems = count
            };
        }

        public void DetachEntity(MeetingEntity meetingEntity)
        {
            DbContext.Entry(meetingEntity).State = EntityState.Detached;
        }
    }
}
