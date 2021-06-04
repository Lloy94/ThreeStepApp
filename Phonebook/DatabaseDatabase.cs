using Database.Communication.DatabaseService;
using Phonebook.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace Phonebook
{
    public class DatabaseDatabase
    {
        DatabaseServiceSoapClient databaseServiceSoapClient = new DatabaseServiceSoapClient();
        public ObservableCollection<Employee> Contacts { get; set; }

        public static ObservableCollection<Department> Departments { get; set; } = ContactControl.Departments;


        public DatabaseDatabase()
        {
            Contacts = new ObservableCollection<Employee>();
            LoadDepartments();
            LoadEmployees();
        }

        private void LoadDepartments()
        {
            foreach (var department in databaseServiceSoapClient.LoadDepartmens())
            {
                Departments.Add(department);
            }
        }

        private void LoadEmployees()
        {
            foreach (var employee in databaseServiceSoapClient.LoadEmployees())
            {
                Contacts.Add(employee);
            }
        }
        public int Add(Employee employee)
        {
            int res = 0;
            foreach (var employe in Contacts)
                if (employe.Phone == employee.Phone)
                    return res;
            res = databaseServiceSoapClient.Add(employee);
                if(res > 0)
            {
                Contacts.Add(employee);
            }
            return res;
        }

        

        public int Update(Employee employee)
        {
            return databaseServiceSoapClient.Update(employee);
        }

        public int Remove(Employee employee)
        {
            var res = databaseServiceSoapClient.Remove(employee);
            if (res>0)
            {
                Contacts.Remove(employee);
            }
            return res;
        }
       



        

        
        

    }
}