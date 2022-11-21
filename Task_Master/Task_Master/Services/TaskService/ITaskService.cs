using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task_Master.Models;

namespace Task_Master.Services.TaskService
{
    public interface ITaskService
    {
        Task AddTask(string name, string description);
        Task<IEnumerable<UserTask>> GetTasks();
        Task<UserTask> GetTask(int id);
        Task RemoveTask(int id);
        Task<IEnumerable<UserTask>> UpdateTask(int id, string name, string description);
        Task<UserTask> ChangeStatus(int id, EnumTaskStatuses statusId);
    }
}
