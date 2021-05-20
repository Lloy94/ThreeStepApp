using Phonebook.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace Phonebook
{
    public class DatabaseDatabase
    {
        private static string[] PHONE_PREFIX = { "906", "495", "499" }; // Префексы телефонных номеров

        private Random random = new Random();
        public ObservableCollection<Employee> Contacts { get; set; }
        public ObservableCollection<Department> Departments { get; set; }

        public DatabaseDatabase()
        {
            Contacts = new ObservableCollection<Employee>();
            Departments = new ObservableCollection<Department>();
            Departments.Add(new Department(0, "IT"));
            Departments.Add(new Department(1, "Склад"));
            Departments.Add(new Department(2, "Охрана"));
            Departments.Add(new Department(3, "Бухгалтерия"));
            Departments.Add(new Department(4, "Управление"));
            GenerateContacts(35);
        }



        private string GeneratePhone()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("+7").Append(PHONE_PREFIX[random.Next(3)]);
            for (int i = 0; i < 6; i++)
                stringBuilder.Append(random.Next(10));
            return stringBuilder.ToString();
        }

        private void GenerateContacts(int contactCount)
        {
            Contacts.Clear();
            string firstName;
            string secondName;
            string lastName;

            var names = new[] { "Анатолий", "Глеб", "Клим", "Мартин", "Лазарь", "Владлен", "Клим", "Панкратий", "Рубен", "Герман" };
            var secondNames = new[] { "Григорьев", "Фокин", "Шестаков", "Хохлов", "Шубин", "Бирюков", "Копылов", "Горбунов", "Лыткин", "Соколов" };
            var lastNames = new[] { "Иванович", "Александрович", "Степанович", "Эдуардович", "Игоревич", "Николаевич", "Артёмович", "Сергеевич", "Тимофеевич", "Артурович" };
            Department category;
            for (int i = 0; i < contactCount; i++)
            {

                firstName = names[random.Next(0, 10)];
                lastName = lastNames[random.Next(0, 10)];
                secondName = secondNames[random.Next(0, 10)];
                Debug.WriteLine(random.Next(0, 2));
                category = Departments[random.Next(0, 5)];





                string phone = GeneratePhone();
                Contacts.Add(new Employee(phone, firstName, secondName, lastName, category));
            }
        }

    }
}