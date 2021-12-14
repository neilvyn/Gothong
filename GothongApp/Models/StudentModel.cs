using System;
using Newtonsoft.Json;
using Prism.Mvvm;
using SQLite;

namespace GothongApp.Models
{
    public class StudentModel : BindableBase
    {
        // key: rpro, datatype: int, property: StudentId
        private int _StudentId;
        [PrimaryKey, AutoIncrement]
        public int StudentId { get { return _StudentId; } set { _StudentId = value; this.RaisePropertyChanged(nameof(StudentId)); } }

        // key: rpro, datatype: string, property: Firstname
        private string _Firstname;
        [JsonProperty("Firstname")]
        public string Firstname { get { return _Firstname; } set { _Firstname = value; this.RaisePropertyChanged(nameof(Firstname)); } }

        // key: rpro, datatype: string, property: Lastname
        private string _Lastname;
        [JsonProperty("_Lastname")]
        public string Lastname { get { return _Lastname; } set { _Lastname = value; this.RaisePropertyChanged(nameof(Lastname)); } }

        // key: rpro, datatype: string, property: Address
        private string _Address;
        [JsonProperty("_Address")]
        public string Address { get { return _Address; } set { _Address = value; this.RaisePropertyChanged(nameof(Address)); } }

        // key: rpro, datatype: DateTime, property: Birthday
        private DateTime _Birthday;
        [JsonProperty("Birthday")]
        public DateTime Birthday { get { return _Birthday; } set { _Birthday = value; this.RaisePropertyChanged(nameof(Birthday)); } }

        // key: rpro, datatype: int, property: Gender
        private int _Gender;
        [JsonProperty("Gender")]
        public int Gender { get { return _Gender; } set { _Gender = value; this.RaisePropertyChanged(nameof(Gender)); } }

        // key: rpro, datatype: int, property: StatusId
        private int _StatusId;
        [JsonProperty("StatusId")]
        public int StatusId { get { return _StatusId; } set { _StatusId = value; this.RaisePropertyChanged(nameof(StatusId)); } }
    }
}
