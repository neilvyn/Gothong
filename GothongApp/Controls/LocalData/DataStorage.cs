using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using GothongApp.Models;
using Prism.Mvvm;
using SQLite;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace GothongApp.Controls.LocalData
{
    public class DataStorage : BindableBase
    {
        public static object locker = new object();
        public SQLiteConnection database;

        public DataStorage()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<StatusModel>();
            database.CreateTable<StudentModel>();
        }
    }
}
