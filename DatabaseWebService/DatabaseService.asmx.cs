using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using Phonebook.Data;

namespace DatabaseWebService
{
    /// <summary>
    /// Сводное описание для DatabaseService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class DatabaseService : System.Web.Services.WebService
    {
        private string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

        [WebMethod]
        public int Add(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string exp = $@"INSERT INTO Employees (Phone, SecondName, FirstName, LastName, Comment, DepartmentId)
                                VALUES ('{employee.Phone}','{employee.SecondName}',
                             '{employee.FirstName}','{employee.LastName}','{employee.Comment}', {employee.Category.Id})";
                SqlCommand command = new SqlCommand(exp, connection);
                return command.ExecuteNonQuery();             
            }
        }
        [WebMethod]
        public int DepatmentAdd(Department department)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string exp = $@"INSERT INTO Departments (DepartmentId, DepartmentName)
                                VALUES ('{department.Id}', '{department.DepartmentName}' )";

                SqlCommand command = new SqlCommand(exp, connection);
                return command.ExecuteNonQuery();
            }
        }
        [WebMethod]
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
        [WebMethod]
        public int Remove(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sqlExpression = $@"DELETE FROM Employees WHERE Phone = '{employee.Phone}'";
                var command = new SqlCommand(sqlExpression, connection);
                return command.ExecuteNonQuery();            
            }
        }

        [WebMethod]
        public int DepartmenRemove(Department deparment)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sqlExpression = $@"DELETE FROM Employees WHERE DepartmentId = '{deparment.Id}'; DELETE FROM Departments WHERE DepartmentId = '{deparment.Id}'";
                var command = new SqlCommand(sqlExpression, connection);
                return command.ExecuteNonQuery();               
            }
        }
        [WebMethod]
        public ObservableCollection<Department> LoadDepartmens()
        {
            var departments = new ObservableCollection<Department>();
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
                            departments.Add(department);
                        }
                    }
                }
            }
            return departments;
        }
        [WebMethod]
        public ObservableCollection<Employee> LoadEmployees()
        {
            var employess = new ObservableCollection<Employee>();
            var departmens = LoadDepartmens();
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
                            int index = 0;
                            for (int i = 0; i < departmens.Count; i++)
                            {
                                if (departmens[i].Id == (int)reader["DepartmentId"]) index += i;
                            }
                            var employee = new Employee()
                            {
                                Phone = reader.GetValue(0).ToString(),
                                SecondName = reader["SecondName"].ToString(),
                                FirstName = reader.GetString(2),
                                LastName = reader["LastName"].ToString(),
                                Comment = reader["Comment"].ToString(),
                                Category = departmens[index]
                            };
                            employess.Add(employee);
                        }
                    }
                }
            }
            return employess;
        }
    }
}
