using System;
using System.Collections.Generic;
using Task_Master.ViewModels;
using Task_Master.Views;
using Xamarin.Forms;

namespace Task_Master
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(TaskDetailPage), typeof(TaskDetailPage));
            Routing.RegisterRoute(nameof(NewTaskPage), typeof(NewTaskPage));
        }

    }
}
