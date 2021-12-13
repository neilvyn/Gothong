using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using GothongApp.Services.Network;
using GothongApp.Services.Rest;
using GothongApp.Services.Predefined;
using GothongApp.Views;
using GothongApp.ViewModels;
using System.Collections.Generic;
using GothongApp.Models;
using RandomNameGeneratorLibrary;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace GothongApp
{
    public partial class App
    {
        #region device scale
        public static float ScreenHeight { get; set; }
        public static float ScreenWidth { get; set; }
        public static float DeviceScale { get; set; }
        public static double StatusBarHeight { get; set; }
        public static double NativeScale { get; set; }
        public static double AppScale { get; set; }
        public static double ScreenScale { get { return (ScreenHeight + ScreenHeight) / (320.0f + 568.0f); } }
        #endregion

        private NetworkHelper NetworkHelper;
        private RestService RestServices;

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            // Instantiate possible services (Session, Permissions, Socket, Analytics, Subscription Keys, etc)

            // Page Redirections
            NavigationService.NavigateAsync(Constants.UsersPage);

            // Dummy Records
            AddStatuses();
            AddStudents();
        }

        private void AddStatuses()
        {
            ObservableCollection<StatusModel> stats = new ObservableCollection<StatusModel>();

            stats.Add(new StatusModel { StatusId = 1, StatusCode = "Online" });
            stats.Add(new StatusModel { StatusId = 1, StatusCode = "Offline" });

            DataStorage.GetInstance.CacheStatuses = JsonConvert.SerializeObject(stats);
        }

        private void AddStudents()
        {
            List<StudentModel> studs = new List<StudentModel>();

            studs.Add(new StudentModel { StudentId = 1, Firstname = "Juan", Lastname = "Dela Cruz", Birthday = DateTime.Today, Gender = 1, Address = "Cebu City", StatusId = 1 });
            studs.Add(new StudentModel { StudentId = 2, Firstname = "Alex", Lastname = "Dela Cruz", Birthday = DateTime.Today, Gender = 1, Address = "Cebu City", StatusId = 0 });
            DataStorage.GetInstance.CacheStudents = JsonConvert.SerializeObject(studs);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>(Constants.NAVIGATION_PAGE);
            containerRegistry.RegisterForNavigation<UsersPage, UsersPageViewModel>(Constants.UsersPage);

            containerRegistry.RegisterInstance<INetworkHelper>(NetworkHelper);
            containerRegistry.RegisterInstance<IRestService>(RestServices);
        }
    }
}
