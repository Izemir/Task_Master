using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Task_Master.Models;
using Task_Master.Services;
using Task_Master.Services.TaskService;
using Task_Master.Views;
using Xamarin.Forms;

namespace Task_Master.ViewModels
{
    [QueryProperty(nameof(TaskId), nameof(TaskId))]
    public class TaskDetailViewModel: BaseViewModel
    {
        private int taskId;
        private string name;
        private string description;
        private string createDate;
        private TaskStatus taskStatus;
        private bool canChangeStatus;

        public Command DeleteTaskCommand { get; }
        public Command ChangeTaskCommand { get; }
        public Command ChangeStatusCommand { get; }

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

        public string CreateDate
        {
            get => createDate;
            set => SetProperty(ref createDate, value);
        }

        public TaskStatus Status
        {
            get => taskStatus;
            set => SetProperty(ref taskStatus, value);
        }

        public bool CanChangeStatus
        { 
            get => canChangeStatus;
            set => SetProperty(ref canChangeStatus, value);
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

        public TaskDetailViewModel()
        {
            Title = "Задача";
            DeleteTaskCommand = new Command(DeleteTask);
            ChangeStatusCommand = new Command(ChangeTaskStatus);
            ChangeTaskCommand = new Command(ChangeTask);
        }

        public async void LoadTaskId(int taskId)
        {
            try
            {
                var task = await TaskService.GetTask(taskId);
                Name = task.Name;
                Description = task.Description;
                CreateDate = task.CreateDate.ToString("yyyy.MM.dd-HH:mm");
                Status=task.Status;
                Title = Name.Length > 10 ? Name.Substring(10) + "..." : Name;
                CanChangeStatus = (EnumTaskStatuses)task.StatusId != EnumTaskStatuses.finished;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Task");
            }
        }

        async void DeleteTask()
        {
            await TaskService.RemoveTask(TaskId);
            await Shell.Current.GoToAsync("..");
        }

        async void ChangeTaskStatus()
        {
            var next = StatusService.ChangeStatus(Status.Id);
            await TaskService.ChangeStatus(taskId,next.Id);
            LoadTaskId(taskId);
        }

        async void ChangeTask()
        {
            await Shell.Current.GoToAsync($"{nameof(NewTaskPage)}?{nameof(NewTaskViewModel.TaskId)}={taskId}");
        }
    }
}
