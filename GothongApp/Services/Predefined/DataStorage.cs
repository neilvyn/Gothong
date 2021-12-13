using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using GothongApp.Models;
using Prism.Mvvm;

namespace GothongApp.Services.Predefined
{
    public class DataStorage : BindableBase
    {
        static DataStorage dataClass;
        private static readonly object Lock = new object();
        public static DataStorage GetInstance
        {
            get
            {
                lock (Lock)
                {
                    return dataClass ?? new DataStorage();
                }
            }
        }

        // key: capro, datatype: string, property: CacheStatuses
        string _CacheStatuses = "";
        public string CacheStatuses
        {
            set
            {
                _CacheStatuses = value;
                Application.Current.Properties["CacheStatuses"] = _CacheStatuses;
                RaisePropertyChanged(nameof(CacheStatuses));
                Application.Current.SavePropertiesAsync();
            }
            get
            {
                if (Application.Current.Properties.ContainsKey("CacheStatuses"))
                {
                    _CacheStatuses = Application.Current.Properties["CacheStatuses"].ToString();
                }
                else
                {
                    _CacheStatuses = default(string);
                }
                return _CacheStatuses;
            }
        }

        // key: capro, datatype: string, property: CacheStudents
        string _CacheStudents = "";
        public string CacheStudents
        {
            set
            {
                _CacheStudents = value;
                Application.Current.Properties["CacheStudents"] = _CacheStudents;
                RaisePropertyChanged(nameof(CacheStudents));
                Application.Current.SavePropertiesAsync();
            }
            get
            {
                if (Application.Current.Properties.ContainsKey("CacheStudents"))
                {
                    _CacheStudents = Application.Current.Properties["CacheStudents"].ToString();
                }
                else
                {
                    _CacheStudents = default(string);
                }
                return _CacheStudents;
            }
        }
    }
}
