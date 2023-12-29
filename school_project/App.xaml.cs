using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using school_project.Classes;
using school_project.Services;
using System;
using System.Diagnostics;
//using Windows.Web.UI;

namespace school_project
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
