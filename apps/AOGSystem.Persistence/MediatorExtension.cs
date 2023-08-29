using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence
{
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, AOGSystemContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries();
                //.Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                //.SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList();
                //.ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => await mediator.Publish(domainEvent));

            await Task.WhenAll(tasks);
        }

        public abstract class AOGSystemDomainEvent
        {
            public AOGSystemDomainEvent()
            {
            }
        }
    }
}
