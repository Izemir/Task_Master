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
    [QueryProperty(nameof(TaskId), nameof(TaskId))]
    public class NewTaskViewModel: BaseViewModel
    {
        private int taskId;
        private string name;
        private string description;
        private bool edit = false;
        public NewTaskViewModel()
        {
            Title = "Новая задача";
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
                Title = Name.Length > 10 ? Name.Substring(10) + "..." : Name;
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            if (edit) await TaskService.UpdateTask(TaskId, Name, Description);
            else await TaskService.AddTask(Name, Description);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
