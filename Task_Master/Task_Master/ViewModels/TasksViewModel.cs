using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Task_Master.Models;
using Task_Master.Services.TaskService;
using Task_Master.Views;
using Xamarin.Forms;

namespace Task_Master.ViewModels
{
    /// <summary>
    /// Список всех задач пользователя
    /// </summary>
    public class TasksViewModel : BaseViewModel
    {
        private UserTask _selectedTask;
        public ObservableCollection<UserTask> Tasks { get; }
        public Command LoadTasksCommand { get; }
        public Command AddTaskCommand { get; }
        public Command<UserTask> TaskTapped { get; }

        public TasksViewModel()
        {
            Title = "Задачи";
            Tasks = new ObservableCollection<UserTask>();
            LoadTasksCommand = new Command(async () => await ExecuteLoadTasksCommand());

            TaskTapped = new Command<UserTask>(OnTaskSelected);

            AddTaskCommand = new Command(OnAddTask);
        }

        async Task ExecuteLoadTasksCommand()
        {
            IsBusy = true;

            try
            {
                Tasks.Clear();

                var tasks = await TaskService.GetTasks();
                foreach (var task in tasks)
                {
                    Tasks.Add(task);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedTask = null;
        }

        public UserTask SelectedTask
        {
            get => _selectedTask;
            set
            {
                SetProperty(ref _selectedTask, value);
                OnTaskSelected(value);
            }
        }

        private async void OnAddTask(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewTaskPage));
        }

        async void OnTaskSelected(UserTask task)
        {
            if (task == null)
                return;

            // This will push the TaskDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(TaskDetailPage)}?{nameof(TaskDetailViewModel.TaskId)}={task.Id}");
        }
    }
}
