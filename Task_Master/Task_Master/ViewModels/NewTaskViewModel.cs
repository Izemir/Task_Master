using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Task_Master.Models;
using Task_Master.Services.TaskService;
using Xamarin.Forms;

namespace Task_Master.ViewModels
{
    /// <summary>
    /// Создание новой задачи и редактирование уже существующей
    /// </summary>
    [QueryProperty(nameof(TaskId), nameof(TaskId))]
    public class NewTaskViewModel: BaseViewModel
    {
        private int taskId;
        private string name;
        private string description;
        private bool edit = false;
        private DateTime deadline;
        public NewTaskViewModel()
        {
            Title = "Новая задача";
            Deadline = DateTime.Now;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public int TaskId
        {
            get
            {
                return taskId;
            }
            set
            {
                if(taskId != null)
                {
                    taskId = value;
                    LoadTaskId(value);
                }                
            }
        }

        public async void LoadTaskId(int taskId)
        {
            try
            {
                var task = await TaskService.GetTask(taskId);
                Name = task.Name;
                Description = task.Description;
                Deadline = task.Deadline;
                Title = Name.Length > 10 ? Name.Substring(0, 10) + "..." : Name;
                edit = true;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Task");
            }
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(description);
        }

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

        public DateTime Deadline
        {
            get => deadline;
            set => SetProperty(ref deadline, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var newTask = new UserTask()
            {
                Name = Name,
                Description = Description,
                Deadline = Deadline
            };
            if (edit) await TaskService.UpdateTask(TaskId, newTask);
            else await TaskService.AddTask(Name, Description, Deadline);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
