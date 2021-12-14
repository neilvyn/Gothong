using System;
using System.Collections.ObjectModel;
using GothongApp.Models;
using GothongApp.Services.Predefined;
using SQLite;
using Xamarin.Forms;

namespace GothongApp.Controls.LocalData
{
    public class StatusDataTable : DataStorage
    {
        public StatusDataTable() { }

        public ObservableCollection<StatusModel> GetStatuss()
        {
            ObservableCollection<StatusModel> Statuss = new ObservableCollection<StatusModel>();

            lock (locker)
            {
                if (database.Table<StatusModel>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    var ret = database.Table<StatusModel>();
                    try
                    {
                        foreach (var user in ret)
                        {
                            Statuss.Add(user);
                        }
                        return Statuss;
                    }
                    catch (Exception ex)
                    {
                        LogConsole.AsyncOutput(this, ex.Message);
                        return null;
                    }
                }
            }
        }

        public StatusModel CreateStatus(StatusModel Status)
        {
            lock (locker)
            {
                if (Status != null)
                {
                    database.Insert(Status);
                    return Status;
                }
                else
                {
                    return null;
                }
            }
        }

        public StatusModel UpdateStatus(StatusModel Status)
        {
            lock (locker)
            {
                database.Update(Status);

                var ret = database.Table<StatusModel>().Where(v => v.StatusId.Equals(Status.StatusId));

                if (ret != null)
                {
                    return ret.FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }

        public int DeleteStatus(int StatusId)
        {
            lock (locker)
            {
                return database.Delete<StatusModel>(StatusId);
            }
        }
    }
}
