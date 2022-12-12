using Sample.Domain.Meetings.DomainServices;
using Sample.Domain.Shared;
using Sample.SharedKernel.SeedWork;
using System;
using System.Threading.Tasks;

namespace Sample.Domain.Meetings.Rules
{
    public class ReserveMeetingRule : IBusinessRule
    {
        private readonly IReserveMeetingService _reserveMeetingService;
        private readonly Guid _meetingId;
        public ReserveMeetingRule(IReserveMeetingService reserveMeetingService, Guid meetingId)
        {
            _reserveMeetingService = reserveMeetingService;
            _meetingId = meetingId;
        }

        public string Message => $"Unable to reserve meeting with given id '{_meetingId}'";

        public string[] Properties => new[] { nameof(MeetingEntity.Status) };

        public string ErrorType => BusinessRuleType.ValueConstraints.ToString("G");

        public async Task<bool> IsBroken() => !await _reserveMeetingService.IsValidAsync(_meetingId);
    }
}
