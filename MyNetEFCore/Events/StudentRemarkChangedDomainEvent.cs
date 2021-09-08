
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetEFCore.Events
{
    public class StudentRemarkChangedDomainEvent : IDomainEvent
    {
        public Student Student { get; private set; }
        public StudentRemarkChangedDomainEvent(Student student)
        {
            this.Student = student;
        }
    }
}
