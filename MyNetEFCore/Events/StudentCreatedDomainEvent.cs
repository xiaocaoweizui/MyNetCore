
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetEFCore.Events
{
    public class StudentCreatedDomainEvent: IDomainEvent
    {
        public Student Student { get; private set; }
        public StudentCreatedDomainEvent(Student student)
        {
            this.Student = student;
        }
    }
}
