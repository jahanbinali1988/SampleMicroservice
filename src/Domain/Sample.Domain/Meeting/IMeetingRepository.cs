using Sample.SharedKernel.Application;
using Sample.SharedKernel.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Domain.Meeting
{
    public interface IMeetingRepository : IRepository<MeetingEntity>
    {
        void DetachEntity(MeetingEntity meetingEntity);
        Task<Pagination<MeetingEntity>> GetListByHostMsisdnAsync(long msisdn, int skip, int take, CancellationToken token);
    }
}
