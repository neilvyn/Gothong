using System;
using System.IO;
using System.Runtime.InteropServices;
using GothongApp.Controls.LocalData;
using GothongApp.Droid.Controls.LocalData;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace GothongApp.Droid.Controls.LocalData
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android() { }

        public SQLite.SQLiteConnection GetConnection()
        {
            var fileName = "Gothong.db3";
            var docuPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(docuPath, fileName);
            var connection = new SQLite.SQLiteConnection(path);

            return connection;
        }
    }
}