using Sample.SharedKernel.Application;
using System;

namespace Sample.Application.Contract.Meeting.Command
{
    public class MarkAsReservedCommand : CommandBase
    {
        public MarkAsReservedCommand()
        {

        }
        public MarkAsReservedCommand(Guid id)
        {
            this.Id = id;
        }
        public Guid Id { get; set; }
    }
}
