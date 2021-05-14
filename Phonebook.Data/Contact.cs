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
    public class Contact
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
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Блокировка
        /// </summary>
        public Boolean Locked { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public ContactCategory Category { get; set; } = ContactCategory.General;

        public string FIO
        {
            get { return $"{LastName} {FirstName} {LastName}"; }
        }

        #region Constructors

        public Contact() { }

        public Contact(string phone, string firstName, string lastName, string secondName)
        {
            Phone = phone;
            FirstName = firstName;
            LastName = lastName;
            SecondName = secondName;
        }

        public Contact(string phone, string firstName, string lastName, string secondName, bool locked, ContactCategory category)
        {
            Phone = phone;
            FirstName = firstName;
            LastName = lastName;
            SecondName = secondName;

            Locked = locked;
            Category = category;
        }

        #endregion

        public override string ToString()
        {
            return $"{Phone} - {LastName} {FirstName} {LastName}";
        }

    }
}
