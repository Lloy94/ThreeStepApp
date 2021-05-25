using Phonebook.Data;
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
        public const string
            ConnectionString = @"Data Source=DESKTOP-E646LMM\SQLEXPRESS;Initial Catalog=Employees;User ID=Employeeroot;Password=19052015";

        public ObservableCollection<Employee> Contacts { get; set; }

        public static ObservableCollection<Department> Departments { get; set; } = ContactControl.Departments;


        public DatabaseDatabase()
        {
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
                int res = 0;
                connection.Open();
                string exp = $@"INSERT INTO Employees (Phone, SecondName, FirstName, LastName, Comment, DepartmentId)
                                VALUES ('{employee.Phone}','{employee.SecondName}',
                             '{employee.FirstName}','{employee.LastName}','{employee.Comment}', {employee.Category.Id})";
                foreach (var employe in Contacts)
                    if (employe.Phone == employee.Phone)
                        return res;
                SqlCommand command = new SqlCommand(exp, connection);
                 res = command.ExecuteNonQuery();               
                if (res > 0)
                {
                    Contacts.Add(employee);
                }
                return res;
            }
        }

        public static int DepatmentAdd(Department department)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string exp = $@"INSERT INTO Departments (DepartmentId, DepartmentName)
                                VALUES ('{department.Id}', '{department.DepartmentName}' )";

                SqlCommand command = new SqlCommand(exp, connection);
                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    Departments.Add(department);
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
        public static int DepartmenRemove(Department deparment)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sqlExpression = $@"DELETE FROM Employees WHERE DepartmentId = '{deparment.Id}'; DELETE FROM Departments WHERE DepartmentId = '{deparment.Id}'";
                var command = new SqlCommand(sqlExpression, connection);
                var res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    for (int i = MainWindow.ContactList.Count-1; i >= 0 ; i--)
                    {
                        if (MainWindow.ContactList[i].Category.Id == deparment.Id)
                            MainWindow.ContactList.Remove(MainWindow.ContactList[i]);
                        continue;
                    }
                    Departments.Remove(deparment);
                   
                }
                return res;
            }
        }

        private void LoadFromDatabase()
        {
            string sqlExpression1 = "SELECT * FROM Departments";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression1, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var department = new Department()
                            {
                                Id = (int)reader["DepartmentId"],
                                DepartmentName = reader["DepartmentName"].ToString(),
                               
                            };
                           Departments.Add(department);
                        }
                    }
                }
            }
            string sqlExpression2 = "SELECT * FROM Employees";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression2, connection);
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
                                Category = Departments[(int)reader["DepartmentId"]]
                            };
                            Contacts.Add(employee);
                        }
                    }
                }
            }
        }

        
        

    }
}