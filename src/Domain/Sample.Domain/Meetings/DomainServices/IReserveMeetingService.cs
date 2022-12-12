using System;
using System.Threading.Tasks;

namespace Sample.Domain.Meetings.DomainServices
{
    public interface IReserveMeetingService
    {
        Task<bool> IsValidAsync(Guid meetingId);
    }
}
