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
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using GothongApp.Controls.LocalData;

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
            NavigationService.NavigateAsync(Constants.StudentsPage);

            // Dummy Records
            DataStorage storage = new DataStorage();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterForNavigation<NavigationPage>(Constants.NAVIGATION_PAGE);
            containerRegistry.RegisterForNavigation<StudentsPage, StudentsPageViewModel>(Constants.StudentsPage);
            containerRegistry.RegisterForNavigation<StudentFormPopupPage, StudentFormPopupPageViewModel>(Constants.StudentFormPopupPage);

            containerRegistry.RegisterInstance<INetworkHelper>(NetworkHelper);
            containerRegistry.RegisterInstance<IRestService>(RestServices);
        }
    }
}
