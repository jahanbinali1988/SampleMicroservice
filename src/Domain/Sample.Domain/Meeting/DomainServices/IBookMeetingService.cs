using System;
using System.Threading.Tasks;

namespace Sample.Domain.Meeting.DomainServices
{
    public interface IBookMeetingService
    {
        Task<bool> IsValidAsync(Guid meetingId);
    }
}
