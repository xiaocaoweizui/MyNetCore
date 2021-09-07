using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetEFCore
{
   public  static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            //调用mediator.Publish
            //mediator.Publish 和mediator.Send的差别，send只触发订阅一次，publish可以触发多次
            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
