using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Task_Master.Services.TaskService;
using Xamarin.Forms;

namespace Task_Master.ViewModels
{
    [QueryProperty(nameof(TaskId), nameof(TaskId))]
    public class TaskDetailViewModel: BaseViewModel
    {
        private int taskId;
        private string name;
        private string description;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public int TaskId
        {
            get
            {
                return taskId;
            }
            set
            {
                taskId = value;
                LoadTaskId(value);
            }
        }

        public async void LoadTaskId(int taskId)
        {
            try
            {
                var task = await TaskService.GetTask(taskId);
                Name = task.Name;
                Description = task.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Task");
            }
        }
    }
}
