using Sample.Application.Contract.Exceptions;
using Sample.Application.Contract.Meeting.Command;
using Sample.Domain.Meetings;
using Sample.Domain.Meetings.DomainServices;
using Sample.SharedKernel.Application;
using Sample.SharedKernel.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Application.Meeting.Command
{
    public class MarkAsBookedCommandHandler : ICommandHandler<MarkAsBookedCommand>
    {
        private readonly IBookMeetingService _bookMeetingService;
        private readonly IMeetingRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public MarkAsBookedCommandHandler(IBookMeetingService bookMeetingService, IMeetingRepository repository, IUnitOfWork unitOfWork)
        {
            _bookMeetingService = bookMeetingService;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(MarkAsBookedCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _repository.GetAsync(request.Id, cancellationToken);
            if (meeting == null)
                throw new EntityNotFoundException($"Unable to find meeting with given id '{request.Id}'");

            await meeting.UpdateAsBooked(_bookMeetingService, request.Id);
            await _repository.UpdateAsync(meeting, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
