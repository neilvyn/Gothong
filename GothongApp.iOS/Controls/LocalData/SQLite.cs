using System;
using System.IO;
using GothongApp.Controls.LocalData;
using GothongApp.iOS.Controls.LocalData;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_IOS))]
namespace GothongApp.iOS.Controls.LocalData
{
    public class SQLite_IOS : ISQLite
    {
        public SQLite_IOS() { }

        public SQLite.SQLiteConnection GetConnection()
        {
            var fileName = "Gothong.db3";
            var docuPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libPath = Path.Combine(docuPath, "..", "Library");
            var path = Path.Combine(libPath, fileName);
            var connection = new SQLite.SQLiteConnection(path);

            return connection;
        }
    }
}
