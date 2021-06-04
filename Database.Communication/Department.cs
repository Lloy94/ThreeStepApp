using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Communication.DatabaseService
{
    public partial class Department : ICloneable
    {
        public Department() { }

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
