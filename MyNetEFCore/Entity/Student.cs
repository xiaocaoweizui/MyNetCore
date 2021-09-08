using MyNetEFCore.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetEFCore
{
    public class Student : Entity<int>
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public int Age { get; private set; }

        public string Remark { get; private set; }

        protected Student()
        { }

        public Student(int id, string name, int age)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Remark = "";

            //添加领域事件
            this.AddDomainEvent(new StudentCreatedDomainEvent(this));
        }

        /// <summary>
        /// 修改备注信息
        /// </summary>
        /// <param name="address"></param>
        public void ChangeRemark(string remark)
        {
            this.Remark = remark;
            //添加领域事件
            this.AddDomainEvent(new StudentRemarkChangedDomainEvent(this));
        }



    }
}
