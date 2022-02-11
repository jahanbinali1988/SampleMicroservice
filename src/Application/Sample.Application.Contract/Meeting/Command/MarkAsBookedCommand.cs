using Sample.SharedKernel.Application;
using System;

namespace Sample.Application.Contract.Meeting.Command
{
    public class MarkAsBookedCommand : CommandBase
    {
        public MarkAsBookedCommand()
        {

        }
        public MarkAsBookedCommand(Guid id)
        {
            this.Id = id;
        }
        public Guid Id { get; set; }
    }
}
