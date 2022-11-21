using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Master.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Task_Master.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksPage : ContentPage
    {
        TasksViewModel _viewModel;
        public TasksPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TasksViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}