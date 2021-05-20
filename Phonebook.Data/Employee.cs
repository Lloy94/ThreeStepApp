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
    /// <summary>
    /// Контакт
    /// </summary>
    public class Employee : INotifyPropertyChanged, ICloneable
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string phone;
        private string firstName;
        private string lastName;
        private string secondName;
        private string comment;
        private Department category ;
        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string SecondName
        {
            get { return secondName; }
            set
            {
                secondName = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Отчество
        /// </summary>
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                NotifyPropertyChanged();
            }
        }


        /// <summary>
        /// Категория
        /// </summary>
        public Department Category
        {
            get { return category; }
            set
            {
                category = value;
                NotifyPropertyChanged();
            }
        }

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
        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}