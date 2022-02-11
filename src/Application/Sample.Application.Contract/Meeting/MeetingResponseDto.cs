using Sample.Application.Contract.Shared;
using Sample.Domain.Shared;
using System;

namespace Sample.Application.Contract.Meeting
{
    public class MeetingResponseDto : EntityBaseDto
    {
        public long HostMsisdn { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public MeetingStatus Status { get; set; }
    }
}
