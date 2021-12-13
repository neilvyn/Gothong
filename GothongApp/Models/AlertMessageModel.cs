using System;
using Prism.Mvvm;

namespace GothongApp.Models
{
    public class AlertMessageModel : BindableBase
    {
        // key: rpro, datatype: string, property: Title
        private string _Title;
        public string Title { get { return _Title; } set { _Title = value; this.RaisePropertyChanged(nameof(Title)); } }

        // key: rpro, datatype: string, property: Description
        private string _Description;
        public string Description { get { return _Description; } set { _Description = value; this.RaisePropertyChanged(nameof(Description)); } }
    }
}
