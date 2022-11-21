using System;
using System.Collections.Generic;
using System.Text;
using Task_Master.Models;

namespace Task_Master.Services
{
    public static class StatusService
    {
        public static List<TaskStatus> StatusList { get; private set; }

		public static void Init()
		{
            StatusList = new List<TaskStatus>()
            {
                new TaskStatus(){Id=EnumTaskStatuses.opened, Name="Открыта"},
                new TaskStatus(){Id=EnumTaskStatuses.inprogress, Name="В процессе"},
                new TaskStatus(){Id=EnumTaskStatuses.finished, Name="Выполнена"},
            };
		}

        public static TaskStatus GetStatus(EnumTaskStatuses id)
        {
            return StatusList.Find(x => x.Id == id);
        }
	}
}
