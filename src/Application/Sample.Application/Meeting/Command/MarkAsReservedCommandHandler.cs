using Sample.Application.Contract.Exceptions;
using Sample.Application.Contract.Meeting.Command;
using Sample.Domain.Meeting;
using Sample.Domain.Meeting.DomainServices;
using Sample.SharedKernel.Application;
using Sample.SharedKernel.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Application.Meeting.Command
{
    public class MarkAsReservedCommandHandler : ICommandHandler<MarkAsReservedCommand>
    {
        private readonly IReserveMeetingService _reserveMeetingService;
        private readonly IMeetingRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public MarkAsReservedCommandHandler(IReserveMeetingService reserveMeetingService, IMeetingRepository repository, IUnitOfWork unitOfWork)
        {
            _reserveMeetingService = reserveMeetingService;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(MarkAsReservedCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _repository.GetAsync(request.Id, cancellationToken);
            if (meeting == null)
                throw new EntityNotFoundException($"Unable to find meeting with given id '{request.Id}'");

            await meeting.UpdateAsReserved(_reserveMeetingService, request.Id);
            await _repository.UpdateAsync(meeting, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
