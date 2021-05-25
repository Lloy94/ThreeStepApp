using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data
{
    public class Department : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Department() { }
        private int id;
        private string departmentName;

       
        public string DepartmentName {
            get { return departmentName; }
            set
            {
               departmentName = value;
                NotifyPropertyChanged();
            }
        }
        public int Id {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }
        public Department(int id, string departmentName)
        {
            Id = id;
            DepartmentName = departmentName;

            
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
