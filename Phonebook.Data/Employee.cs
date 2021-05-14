using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data
{
    /// <summary>
    /// Контакт
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }


        /// <summary>
        /// Категория
        /// </summary>
        public Department Category { get; set; } = Department.IT;

        public string FIO
        {
            get { return $"{LastName} {FirstName} {SecondName}"; }
        }

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

        public override string ToString()
        {
            return $"{Category} - {SecondName} {FirstName} {LastName}";
        }

    }
}
