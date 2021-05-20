using Phonebook.Data;
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
        public const string
            ConnectionString = @"Data Source=DESKTOP-E646LMM\SQLEXPRESS;Initial Catalog=Employees;User ID=Employeeroot;Password=19052015";

        private static string[] PHONE_PREFIX = { "906", "495", "499" }; // Префексы телефонных номеров

        private Random random = new Random();
        public ObservableCollection<Employee> Contacts { get; set; }

        public DatabaseDatabase()
        {
            Department.Departments.Add(new Department(0, "IT"));
            Department.Departments.Add(new Department(1, "Склад"));
            Department.Departments.Add(new Department(2, "Охрана"));
            Department.Departments.Add(new Department(3, "Бухгалтерия"));
            Department.Departments.Add(new Department(4, "Управление"));
            Contacts = new ObservableCollection<Employee>();
            LoadFromDatabase();
        }

       public void SyncToDatabase()
       {
           foreach(var contact in Contacts)
           {
               Add(contact);
           }
       }
        public int Add(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string exp = $@"INSERT INTO Employees (Phone, SecondName, FirstName, LastName, Comment, DepartmentId)
                                VALUES ('{employee.Phone}','{employee.SecondName}',
                             '{employee.FirstName}','{employee.LastName}','{employee.Comment}', {employee.Category.Id})";

                SqlCommand command = new SqlCommand(exp, connection);
                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    Contacts.Add(employee);
                }
                return res;
            }
        }

        public int Update(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sqlExpression = $@"UPDATE Employees 
                    SET SecondName = '{employee.SecondName}', FirstName = '{employee.FirstName}', LastName = '{employee.LastName}', Comment = '{employee.Comment}', DepartmentId = {(int)employee.Category.Id}
                    WHERE phone = '{employee.Phone}'";
                var command = new SqlCommand(sqlExpression, connection);
                return command.ExecuteNonQuery();
            }
        }

        public int Remove(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sqlExpression = $@"DELETE FROM Employees WHERE Phone = '{employee.Phone}'";
                var command = new SqlCommand(sqlExpression, connection);
                var res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    Contacts.Remove(employee);
                }
                return res;
            }
        }

        private void LoadFromDatabase()
        {
            string sqlExpression = "SELECT * FROM Employees";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee()
                            {
                                Phone = reader.GetValue(0).ToString(),
                                SecondName = reader["SecondName"].ToString(),
                                FirstName = reader.GetString(2),
                                LastName = reader["LastName"].ToString(),
                                Comment = reader["Comment"].ToString(),
                                Category = Department.Departments[(int)reader["DepartmentId"]]
                            };
                            Contacts.Add(employee);
                        }
                    }
                }
            }
        }

        
        

    }
}