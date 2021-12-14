using System;
using SQLite;

namespace GothongApp.Controls.LocalData
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
