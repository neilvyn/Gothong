using System;
using System.Collections.ObjectModel;
using GothongApp.Models;
using GothongApp.Services.Predefined;
using SQLite;
using Xamarin.Forms;

namespace GothongApp.Controls.LocalData
{
    public class StudentDataTable : DataStorage
    {
        public StudentDataTable() { }

        public ObservableCollection<StudentModel> GetStudents()
        {
            ObservableCollection<StudentModel> students = new ObservableCollection<StudentModel>();

            lock (locker)
            {
                if (database.Table<StudentModel>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    var ret = database.Table<StudentModel>();
                    try
                    {
                        foreach (var user in ret)
                        {
                            students.Add(user);
                        }
                        return students;
                    }
                    catch (Exception ex)
                    {
                        LogConsole.AsyncOutput(this, ex.Message);
                        return null;
                    }
                }
            }
        }

        public StudentModel CreateStudent(StudentModel student)
        {
            lock (locker)
            {
                if (student != null)
                {
                    database.Insert(student);
                    return student;
                }
                else
                {
                    return null;
                }
            }
        }

        public StudentModel UpdateStudent(StudentModel student)
        {
            lock (locker)
            {
                database.Update(student);

                var ret = database.Table<StudentModel>().Where(v => v.StudentId.Equals(student.StudentId));

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

        public int DeleteStudent(int studentId)
        {
            lock (locker)
            {
                return database.Delete<StudentModel>(studentId);
            }
        }
    }
}
