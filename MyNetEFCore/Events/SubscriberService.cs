using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace MyNetEFCore.Events
{
    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        IMediator _mediator;
        public SubscriberService(IMediator mediator)
        {
            _mediator = mediator;
        }


        [CapSubscribe("OrderPaymentSucceeded")]
        public void OrderPaymentSucceeded(StudentIntegrationEvent @event)
        {
            //Do SomeThing
        }

    }
}
