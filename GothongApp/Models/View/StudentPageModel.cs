using System;
using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace GothongApp.Models.View
{
    public class StudentPageModel : BindableBase
    {
        // key: rpro, datatype: bool, property: HasData
        private bool _HasData = false;
        public bool HasData { get { return _HasData; } set { _HasData = value; this.RaisePropertyChanged(nameof(HasData)); } }

        // key: rpro, datatype: ObservableCollection<UserModel>, property: Students
        private ObservableCollection<StudentModel> _Students;
        public ObservableCollection<StudentModel> Students { get { return _Students; } set { _Students = value; this.RaisePropertyChanged(nameof(Students)); } }
    }
}
