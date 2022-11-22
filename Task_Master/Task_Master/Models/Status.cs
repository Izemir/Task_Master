using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Master.Models
{
    public enum EnumTaskStatuses
    {
        opened = 1,
        inprogress = 2,
        finished = 3
    }

    public class TaskStatus
    {
        public EnumTaskStatuses Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}
