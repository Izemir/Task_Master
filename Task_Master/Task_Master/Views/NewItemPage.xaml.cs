using System;
using System.Collections.Generic;
using System.ComponentModel;
using Task_Master.Models;
using Task_Master.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Task_Master.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}