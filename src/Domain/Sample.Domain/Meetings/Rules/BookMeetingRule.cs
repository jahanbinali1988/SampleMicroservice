using Sample.Domain.Meetings.DomainServices;
using Sample.Domain.Shared;
using Sample.SharedKernel.SeedWork;
using System;
using System.Threading.Tasks;

namespace Sample.Domain.Meetings.Rules
{
    public class BookMeetingRule : IBusinessRule
    {
        private readonly IBookMeetingService _bookMeetingService;
        private readonly Guid _meetingId;
        public BookMeetingRule(IBookMeetingService bookMeetingService, Guid meetingId)
        {
            _bookMeetingService = bookMeetingService;
            _meetingId = meetingId;
        }

        public string Message => $"Unable to book meeting with given id '{_meetingId}'";

        public string[] Properties => new[] { nameof(MeetingEntity.Status) };

        public string ErrorType => BusinessRuleType.IdValidity.ToString("G");

        public async Task<bool> IsBroken() => await _bookMeetingService.IsValidAsync(_meetingId);
    }
}
