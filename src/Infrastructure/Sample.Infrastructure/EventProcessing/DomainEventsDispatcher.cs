﻿using System.Linq;
using MediatR;
using System.Threading.Tasks;
using Sample.SharedKernel.EventProcessing.DomainEvent;
using Sample.Infrastructure.Persistence;
using Sample.SharedKernel.SeedWork;

namespace Sample.Infrastructure.EventProcessing
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly SampleDbContext _dbContext;

        public DomainEventsDispatcher(IMediator mediator, SampleDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEntities = this._dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            var tasks = domainEvents.Select(e => _mediator.Publish(e));

            await Task.WhenAll(tasks);
        }
    }
}
