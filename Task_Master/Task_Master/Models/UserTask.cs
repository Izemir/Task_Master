using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Master.Models
{
    // пользовательская задача
    public class UserTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime Deadline { get; set; } 
        public int StatusId { get; set; } = (int)EnumTaskStatuses.opened;
        [Ignore]
        public TaskStatus Status { get; set; }

    }
}
