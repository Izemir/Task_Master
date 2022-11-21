using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Master.Models;
using Task_Master.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Task_Master.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTaskPage : ContentPage
    {
        public UserTask UserTask { get; set; }
        public NewTaskPage()
        {
            InitializeComponent();
            BindingContext = new NewTaskViewModel();
        }
    }
}