using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetEFCore.Events
{
    public interface ISubscriberService
    {
        void OrderPaymentSucceeded(StudentIntegrationEvent @event);
    }
}
