using Ardalis.GuardClauses;
using Sample.Domain.Meeting.DomainServices;
using Sample.Domain.Meeting.Rules;
using Sample.Domain.Shared;
using Sample.SharedKernel.SeedWork;
using System;
using System.Threading.Tasks;

namespace Sample.Domain.Meeting
{
    public class MeetingEntity : Entity<Guid>, IAggregateRoot
    {
        private MeetingEntity()
        {

        }
        private MeetingEntity(long msisdn, Guid? id)
        {
            if (!id.HasValue)
                id = Guid.NewGuid();

            base.Id = id.Value;
            UpdateHostMsisdn(msisdn);
        }

        public static async Task<MeetingEntity> CreateAsync(long msisdn, 
            DateTimeOffset startDate, DateTimeOffset endDate, Guid? id)
        {
            var meeting = new MeetingEntity(msisdn, id);
            await meeting.UpdateStartDate(startDate);
            await meeting.UpdateEndDate(endDate);
            meeting.UpdateStatus(MeetingStatus.None);

            return meeting;
        }

        public async Task UpdateAsReserved(IReserveMeetingService reserveMeetingService, Guid meetingId)
        {
            await CheckRule(new ReserveMeetingRule(reserveMeetingService, meetingId));

            UpdateStatus(MeetingStatus.Reserved);
        }

        public async Task UpdateAsBooked(IBookMeetingService bookMeetingService, Guid meetingId)
        {
            await CheckRule(new BookMeetingRule(bookMeetingService, meetingId));

            UpdateStatus(MeetingStatus.Booked);
        }

        private void UpdateStatus(MeetingStatus status)
        {
            Status = status;
        }
        private void UpdateHostMsisdn(long msisdn)
        {
            Guard.Against.Default(msisdn, nameof(msisdn));

            this.HostMsisdn = msisdn; 
        }
        private async Task UpdateStartDate(DateTimeOffset startDate) 
        {
            Guard.Against.Default(startDate, nameof(startDate));
            await CheckRule(new DateIsFixedHourRule(startDate, nameof(StartDate)));

            this.StartDate = startDate; 
        }
        private async Task UpdateEndDate(DateTimeOffset endDate)
        {
            Guard.Against.Default(endDate, nameof(endDate));
            await CheckRule(new DateIsFixedHourRule(endDate, nameof(EndDate)));

            var duration = endDate.Subtract(StartDate).Ticks;
            UpdateDurationTicks(duration);
            this.EndDate = endDate;
        }
        private void UpdateDurationTicks(long durationTicks)
        {
            Guard.Against.NegativeOrZero(durationTicks, nameof(durationTicks));

            this.DurationTicks = durationTicks;
        }

        public long HostMsisdn { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public TimeSpan Duration
        {
            get => TimeSpan.FromTicks(DurationTicks);
            set => DurationTicks = value.Ticks;
        }
        public long DurationTicks { get; private set; }
        public MeetingStatus Status { get; private set; }
    }
}
