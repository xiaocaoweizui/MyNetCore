using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetEFCore.Events
{
    public class StudentIntegrationEvent
    {
        public StudentIntegrationEvent(int id) => Id = id;
        public int Id { get; }
    }
}
