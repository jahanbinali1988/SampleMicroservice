using Sample.Domain.Shared;
using System;

namespace Sample.Api.Models.Meeting
{
    public class MeetingViewModel : ViewModelBase
    {
        public long HostMsisdn { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public MeetingStatus Status { get; set; }
    }
}
