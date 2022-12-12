using System;
using System.Threading.Tasks;

namespace Sample.Domain.Meetings.DomainServices
{
    public interface IBookMeetingService
    {
        Task<bool> IsValidAsync(Guid meetingId);
    }
}
