using System;
using Task_Master.Services;
using Task_Master.Services.TaskService;
using Task_Master.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Task_Master
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            StatusService.Init();
            TaskService.Init();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
