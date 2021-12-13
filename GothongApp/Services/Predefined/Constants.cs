using System;
using GothongApp.Models;
using Xamarin.Forms;

namespace GothongApp.Services.Predefined
{
    public class Constants
    {
        #region urls
        private const string Local_BaseUrl = "127.0.0.1";
        private const string Exam_BaseUrl = "";
        #endregion

#if DEBUG
        private static string BASE_ADDRESS = Exam_BaseUrl;
#elif STAGING
        private static string BASE_ADDRESS = Exam_BaseUrl;
#elif RELEASE
        private static string BASE_ADDRESS = Exam_BaseUrl;
#else
        private static string BASE_ADDRESS = Exam_BaseUrl;
#endif

        #region api routes
        public static string URL_STUDENTS = BASE_ADDRESS + "/students";
        public static string URL_STATUSES = BASE_ADDRESS + "/statuses";
        #endregion

        #region keys
        public static string AUTH_HEADER = "SampleHeaderAuthTokenPass@1234";
        #endregion

        #region screen_pages
        public static string NAVIGATION_PAGE = "NavigationPage";
        public static string UserFormPage = "UserFormPage";
        public static string UsersPage = "UsersPage";
        public static string UserDetailPage = "UserDetailPage";
        #endregion

        #region alert_messages
        public static string CriticalTitleAlert = "Error";
        public static string DefaultTitleAlert = "Alert Message";
        public static string AlertPositiveLabel = "Got It!";

        public static AlertMessageModel HOST_UNREACHABLE = new AlertMessageModel { Title = "Host Unreachable", Description = "The URL cannot be reached and seems to be unavailable. Please try again later." };
        public static AlertMessageModel NO_CONNECTION = new AlertMessageModel { Title = "Failed Connection", Description = "Please check your internet connection and try again." };
        public static AlertMessageModel CANCELED_OPERATION = new AlertMessageModel { Title = "Canceled Operation", Description = "Request has been canceled." };
        public static AlertMessageModel REQUEST_TIMEOUT = new AlertMessageModel { Title = "Request Timeout", Description = "Please check your internet connection and try again." };
        #endregion
    }
}
