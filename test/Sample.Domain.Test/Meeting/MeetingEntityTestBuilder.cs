using Sample.Domain.Meetings;
using Sample.Domain.Meetings.DomainServices;
using Moq;
using System;
using System.Threading.Tasks;

namespace Sample.Domain.Test.Meetings
{
    public class MeetingEntityTestBuilder
    {
        internal DateTimeOffset _standardDate = new DateTimeOffset(2020, 01, 01, 10, 0, 0, new TimeSpan());
        internal Guid _id;
        internal long _hostMsisdn;
        internal DateTimeOffset _startDate;
        internal DateTimeOffset _endDate;
        internal Mock<IReserveMeetingService> _reserveMeetingService;
        internal Mock<IBookMeetingService> _bookMeetingService;

        public MeetingEntityTestBuilder()
        {
            _reserveMeetingService = new Mock<IReserveMeetingService>();
            _bookMeetingService = new Mock<IBookMeetingService>();

            WithId(Guid.NewGuid());
            WithStartDate(_standardDate);
            WithEndDate(_standardDate.AddHours(1));
            WithHostMsisdn(long.MaxValue);
            SetReserveMeetingService(true);
        }

        public MeetingEntityTestBuilder SetReserveMeetingService(bool result)
        {
            _reserveMeetingService.Setup(s => s.IsValidAsync(_id)).ReturnsAsync(result);
            return this;
        }

        public MeetingEntityTestBuilder SetBookMeetingService(bool result)
        {
            _bookMeetingService.Setup(s => s.IsValidAsync(_id)).ReturnsAsync(result);
            return this;
        }

        public MeetingEntityTestBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }
        public MeetingEntityTestBuilder WithHostMsisdn(long msisdn) 
        {
            _hostMsisdn = msisdn;
            return this;
        }
        public MeetingEntityTestBuilder WithStartDate(DateTimeOffset startDate)
        {
            _startDate = startDate;
            return this;
        }
        public MeetingEntityTestBuilder WithEndDate(DateTimeOffset endDate)
        {
            _endDate = endDate;
            return this;
        }

        public async Task<MeetingEntity> BuildAsync()
        {
            return await MeetingEntity.CreateAsync(_hostMsisdn, _startDate, _endDate, _id);
        }
    }
}
