using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ERNITech.Controls.Utilities;
using GothongApp.Controls.LocalData;
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
    public class StudentsPageViewModel : ViewModelBase, IResponseConnector
    {
        // key: rpro, datatype: StudentPageModel, property: ClassProperty
        private StudentPageModel _ClassProperty = new StudentPageModel();
        public StudentPageModel ClassProperty { get { return _ClassProperty; } set { _ClassProperty = value; this.RaisePropertyChanged(nameof(ClassProperty)); } }

        #region events and delegates
        public DelegateCommand GearCommand { get; set; }
        public DelegateCommand ShowStudentsCommand { get; set; }
        public DelegateCommand<object> ItemTappedCommand { get; set; }
        #endregion

        #region variables
        private INavigationService navigationService;
        private readonly RestService restService;
        private readonly NetworkHelper networkHelper;
        private StudentDataTable studentTable = new StudentDataTable();
        CancellationTokenSource cts;
        #endregion

        public StudentsPageViewModel(INavigationService _navigationService, RestService _restService, NetworkHelper _networkHelper) : base(_navigationService)
        {
            navigationService = _navigationService;
            networkHelper = _networkHelper;
            restService = _restService;
            restService.RestResponseDelegate = this;

            ShowStudentsCommand = new DelegateCommand(ShowStudentsControl);
            GearCommand = new DelegateCommand(GearControl);
            ItemTappedCommand = new DelegateCommand<object>(ItemTappedAction);
        }

        async private void GearControl()
        {
            var action = await Acr.UserDialogs.UserDialogs.Instance.ActionSheetAsync(null, "Cancel", null, null, "Student Form", "Statuses");

            if(action.Equals("Student Form"))
            {
                await navigationService.NavigateAsync(Constants.StudentFormPopupPage, animated: true);
            }
        }

        async private void ShowStudentsControl()
        {
            //await GetStudents();
            GetLocalStudents();
        }

        private void GetLocalStudents()
        {
            ClassProperty.Students = studentTable.GetStudents();
            ClassProperty.HasData = ClassProperty.Students.Count() > 0;
        }

        private void ItemTappedAction(object obj)
        {
            var stud = obj as StudentModel;
            LogConsole.AsyncOutput(this, stud.Firstname);

            NavigationParameters navParams = new NavigationParameters();
            navParams.Add("StudentItem", stud);

            //navigationService.NavigateAsync(Constants.StudentDetailPage, parameters: navParams, animated: true);
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
