using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Communication.DatabaseService
{
    public partial class Employee : ICloneable
    {
        #region Constructors

        public Employee() { }

        public Employee(Department category, string firstName, string secondName, string lastName)
        {
            Category = category;
            FirstName = firstName;
            LastName = lastName;
            SecondName = secondName;
        }

        public Employee(string phone, string firstName, string secondName, string lastName, Department category)
        {
            Phone = phone;
            FirstName = firstName;
            LastName = lastName;
            SecondName = secondName;

            Category = category;
        }

        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
