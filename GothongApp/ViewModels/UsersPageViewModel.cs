using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ERNITech.Controls.Utilities;
using GothongApp.Models;
using GothongApp.Models.View;
using GothongApp.Services.Network;
using GothongApp.Services.Predefined;
using GothongApp.Services.Rest;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace GothongApp.ViewModels
{
    public class UsersPageViewModel : ViewModelBase, IResponseConnector
    {
        // key: rpro, datatype: UsersPageModel, property: ClassProperty
        private UsersPageModel _ClassProperty = new UsersPageModel();
        public UsersPageModel ClassProperty { get { return _ClassProperty; } set { _ClassProperty = value; this.RaisePropertyChanged(nameof(ClassProperty)); } }

        #region events and delegates
        public DelegateCommand FetchCommand { get; set; }
        public DelegateCommand<object> ItemTappedCommand { get; set; }
        #endregion

        #region variables
        private INavigationService navigationService;
        private readonly RestService restService;
        private readonly NetworkHelper networkHelper;
        CancellationTokenSource cts;
        #endregion

        public UsersPageViewModel(INavigationService _navigationService, RestService _restService, NetworkHelper _networkHelper) : base(_navigationService)
        {
            navigationService = _navigationService;
            networkHelper = _networkHelper;
            restService = _restService;
            restService.RestResponseDelegate = this;

            FetchCommand = new DelegateCommand(FetchControl);
            ItemTappedCommand = new DelegateCommand<object>(ItemTappedAction);
        }

        async private void FetchControl()
        {
            // if validated
            //await GetStudents();

            GetCacheStudents();
        }

        private void GetCacheStudents()
        {
            ClassProperty.Students = JsonConvert.DeserializeObject<ObservableCollection<StudentModel>>(DataStorage.GetInstance.CacheStudents);
        }

        private void ItemTappedAction(object obj)
        {
            var usr = obj as StudentModel;
            LogConsole.AsyncOutput(this, usr.Firstname);

            NavigationParameters navParams = new NavigationParameters();
            navParams.Add("UserItem", usr);

            navigationService.NavigateAsync(Constants.UserDetailPage, parameters: navParams, animated: true);
        }

        private async Task GetStudents()
        {

            if (!networkHelper.HasInternet())
                Acr.UserDialogs.UserDialogs.Instance.Alert(Constants.NO_CONNECTION.Description, Constants.NO_CONNECTION.Title, Constants.AlertPositiveLabel);
            else {
                try
                {
                    cts = new CancellationTokenSource();
                    Acr.UserDialogs.UserDialogs.Instance.ShowLoading();

                    string url = Constants.URL_STUDENTS;
                    await restService.Request(EnumHttpMethod.Get, url, ctoken: cts.Token, ws_query: "show_students");
                    cts = null;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constants.HOST_UNREACHABLE.Description, Constants.HOST_UNREACHABLE.Title, Constants.AlertPositiveLabel);
                }
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        public void ReceiveError(string title, string error, string ws_query) => Acr.UserDialogs.UserDialogs.Instance.Alert(error, title, Constants.AlertPositiveLabel);

        public void ReceiveJSONData(string jsonString, string ws_query)
        {
            Device.BeginInvokeOnMainThread(() => Acr.UserDialogs.UserDialogs.Instance.HideLoading());
            var jsonData = jsonString.ToJObject();

            if (jsonData != null)
            {
                switch (ws_query)
                {
                    case "show_students":
                        if (jsonData.ContainsKey("obj"))
                        {
                            ClassProperty.Students = JsonConvert.DeserializeObject<ObservableCollection<StudentModel>>(jsonData["obj"].ToString());
                        }
                        break;
                    default:
                        break;
                }
            }

            ClassProperty.HasData = ClassProperty.Students.Count > 0;
        }
    }
}
