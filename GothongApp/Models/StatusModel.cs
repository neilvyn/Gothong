using System;
using Prism.Mvvm;

namespace GothongApp.Models
{
    public class StatusModel : BindableBase
    {
        // key: rpro, datatype: int, property: StatusId
        private int _StatusId;
        public int StatusId { get { return _StatusId; } set { _StatusId = value; this.RaisePropertyChanged(nameof(StatusId)); } }

        // key: rpro, datatype: string, property: StatusCode
        private string _StatusCode;
        public string StatusCode { get { return _StatusCode; } set { _StatusCode = value; this.RaisePropertyChanged(nameof(StatusCode)); } }
    }
}
