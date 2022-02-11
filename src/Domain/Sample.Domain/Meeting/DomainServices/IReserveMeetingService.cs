using System;
using System.Threading.Tasks;

namespace Sample.Domain.Meeting.DomainServices
{
    public interface IReserveMeetingService
    {
        Task<bool> IsValidAsync(Guid meetingId);
    }
}
