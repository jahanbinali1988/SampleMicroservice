using Sample.Domain.Shared;
using Sample.SharedKernel.SeedWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sample.Domain.Test.Meeting
{
    public class MeetingEntityTests
    {
        public MeetingEntityTests()
        {

        }

        #region Create

        [Fact]
        internal async Task Create_meeting_entity_successfully()
        {
            var builder = new MeetingEntityTestBuilder();
            var meeting = await builder.BuildAsync();

            Assert.NotNull(meeting);
            Assert.NotEqual(Guid.Empty, meeting.Id);

        }

        [Theory]
        [ClassData(typeof(SampleData))]
        internal async Task Meeting_entity_could_not_create_successfully_with_wrong_inputs(long msisdn, DateTimeOffset startDate, DateTimeOffset endDate, Type expectedException)
        {
            var builder = new MeetingEntityTestBuilder();

            await Assert.ThrowsAsync(expectedException, () => builder.WithHostMsisdn(msisdn).WithStartDate(startDate).WithEndDate(endDate).BuildAsync());
        }

        private class SampleData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                var startDate = new DateTimeOffset(2020, 01, 01, 10, 0, 0, new TimeSpan());
                var endDate = new DateTimeOffset(2020, 01, 01, 11, 0, 0, new TimeSpan());
                yield return new object[]
                {
                    0, startDate, endDate, typeof(ArgumentException)
                };

                yield return new object[]
                {
                    long.MaxValue, new DateTimeOffset(), new DateTimeOffset(), typeof(ArgumentException)
                };

                yield return new object[]
                {
                    long.MaxValue, startDate, new DateTimeOffset(), typeof(ArgumentException)
                };

                yield return new object[]
                {
                    long.MaxValue, new DateTimeOffset(), endDate, typeof(ArgumentException)
                };

                yield return new object[]
                {
                    long.MaxValue, endDate.AddHours(1), endDate, typeof(ArgumentException)
                };

                yield return new object[]
                {
                    long.MaxValue, startDate.AddMinutes(1), endDate, typeof(BusinessRuleValidationException)
                };


                yield return new object[]
                {
                    long.MaxValue, startDate.AddSeconds(1), endDate, typeof(BusinessRuleValidationException)
                };

                yield return new object[]
                {
                    long.MaxValue, startDate, endDate.AddMinutes(1), typeof(BusinessRuleValidationException)
                };


                yield return new object[]
                {
                    long.MaxValue, startDate, endDate.AddSeconds(1), typeof(BusinessRuleValidationException)
                };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        #endregion

        #region UpdateAsReserved

        [Fact]
        internal async Task Update_meeting_as_reserved_successfully()
        {
            var builder = new MeetingEntityTestBuilder();
            var meeting = await builder.SetReserveMeetingService(true).BuildAsync();
            await meeting.UpdateAsReserved(builder._reserveMeetingService.Object, builder._id);

            Assert.True(true);
        }

        [Fact]
        internal async Task Meeting_entity_could_not_be_reserved_successfully_when_the_meeting_is_booked()
        {
            var builder = new MeetingEntityTestBuilder();
            var meeting = await builder.SetReserveMeetingService(false).BuildAsync();

            await Assert.ThrowsAsync<BusinessRuleValidationException>(()=> meeting.UpdateAsReserved(builder._reserveMeetingService.Object, builder._id));
        }

        #endregion

        #region UpdateAsBooked

        [Fact]
        internal async Task Update_meeting_as_booked_successfully()
        {
            var builder = new MeetingEntityTestBuilder();
            var meeting = await builder.SetBookMeetingService(true).BuildAsync();
            await meeting.UpdateAsBooked(builder._bookMeetingService.Object, builder._id);

            Assert.True(true);
        }

        [Fact]
        internal async Task Meeting_entity_could_not_be_booked_successfully_when_the_meeting_is_booked()
        {
            var builder = new MeetingEntityTestBuilder();
            var meeting = await builder.SetBookMeetingService(false).BuildAsync();

            await Assert.ThrowsAsync<BusinessRuleValidationException>(() => meeting.UpdateAsBooked(builder._bookMeetingService.Object, builder._id));
        }

        #endregion
    }
}
