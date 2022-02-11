using Sample.Application.Contract.Shared;
using Sample.SharedKernel.Application;

namespace Sample.Application.Contract.Meeting.Query
{
    public class GetHostMeetingsListQuery : GetListQueryBase, IQuery<Pagination<MeetingResponseDto>>
    {
        public GetHostMeetingsListQuery(long hostMsisdn, int offset, int count) : base(offset, count)
        {
            HostMsisdn = hostMsisdn;
        }

        public long HostMsisdn { get; private set; }
    }
}
