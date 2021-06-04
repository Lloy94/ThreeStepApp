using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Database.Communication.DatabaseService;
using Phonebook.Controls;

namespace Phonebook
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class DepartmentEditor : Window
    {
        DatabaseServiceSoapClient databaseServiceSoapClient = new DatabaseServiceSoapClient();
        public static ObservableCollection<Department> DepartmentList { get; set; }

        public Department SelectedDepartment { get; set; }
        public DepartmentEditor()
        {          
            InitializeComponent();
            this.DataContext = this;
            DepartmentList = DatabaseDatabase.Departments;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (deparmentView.SelectedItems.Count < 1)
                return;

            if (MessageBox.Show("Вы действительно желаете удалить Департамент?", "Удаление департамента", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (DepartmenRemove((Department)deparmentView.SelectedItems[0]) > 0) { 
                    MessageBox.Show("Запись успешно удалена", "Удаление записи", MessageBoxButton.OK, MessageBoxImage.Information);                 
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Department department = new Department
            {
                Id = DepartmentList[DepartmentList.Count - 1].Id + 1,
                DepartmentName = textBox.Text
            };

            if (DepatmentAdd(department) > 0)
                MessageBox.Show("Запись успешно добавлена", "Добавление записи", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        public int DepartmenRemove(Department deparment)
        {
            var res = databaseServiceSoapClient.DepartmenRemove(deparment);
            if (res > 0)
            {
                for (int i = MainWindow.ContactList.Count - 1; i >= 0; i--)
                {
                    if (MainWindow.ContactList[i].Category.Id == deparment.Id)
                        MainWindow.ContactList.Remove(MainWindow.ContactList[i]);
                    continue;
                }
                DepartmentList.Remove(deparment);

            }
            return res;
        }
        public int DepatmentAdd(Department department)
        {
            var res = databaseServiceSoapClient.DepatmentAdd(department);

            if (res > 0)
            {
                DepartmentList.Add(department);
            }
            return res;

        }
    }
}
