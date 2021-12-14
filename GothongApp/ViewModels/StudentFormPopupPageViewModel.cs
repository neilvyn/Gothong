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
    public class StudentFormPopupPageViewModel : ViewModelBase
    {
        #region events and delegates
        public DelegateCommand CloseCommand { get; set; }
        #endregion

        #region variables
        private INavigationService navigationService;
        private StudentDataTable studentTable = new StudentDataTable();
        #endregion

        public StudentFormPopupPageViewModel(INavigationService _navigationService, RestService _restService, NetworkHelper _networkHelper) : base(_navigationService)
        {
            navigationService = _navigationService;
            CloseCommand = new DelegateCommand(CloseControl);
        }

        private void CloseControl()
        {
            navigationService.GoBackAsync(animated: true);
        }
    }
}
