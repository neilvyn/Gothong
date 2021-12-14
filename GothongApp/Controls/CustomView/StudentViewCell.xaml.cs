using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GothongApp.Controls.CustomView
{
    public partial class StudentViewCell : ContentView
    {
        #region bindables
        // key: bpro_twoway, datatype: int, property: StudentId, class: StudentViewCell
        public static readonly BindableProperty StudentIdProperty = BindableProperty.Create(nameof(StudentId), typeof(int), typeof(StudentViewCell), default(int), BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var view = bindable as StudentViewCell;
        });
        public int StudentId
        {
            get { return (int)GetValue(StudentIdProperty); }
            set { SetValue(StudentIdProperty, value); }
        }

        // key: bpro_twoway, datatype: string, property: Firstname, class: StudentViewCell
        public static readonly BindableProperty FirstnameProperty = BindableProperty.Create(nameof(Firstname), typeof(string), typeof(StudentViewCell), default(string), BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var view = bindable as StudentViewCell;
            var val = (string)newValue;
        });
        public string Firstname
        {
            get { return (string)GetValue(FirstnameProperty); }
            set { SetValue(FirstnameProperty, value); }
        }

        // key: bpro_twoway, datatype: string, property: Lastname, class: StudentViewCell
        public static readonly BindableProperty LastnameProperty = BindableProperty.Create(nameof(Lastname), typeof(string), typeof(StudentViewCell), default(string), BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var view = bindable as StudentViewCell;
            var val = (string)newValue;
        });
        public string Lastname
        {
            get { return (string)GetValue(LastnameProperty); }
            set { SetValue(LastnameProperty, value); }
        }
        #endregion

        public StudentViewCell()
        {
            InitializeComponent();
        }
    }
}
