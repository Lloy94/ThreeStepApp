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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Database.Communication.DatabaseService;

namespace Phonebook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseDatabase database = new DatabaseDatabase();

        public static ObservableCollection<Employee> ContactList { get; set; }


        public Employee SelectedContact { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ContactList = database.Contacts;
            
        }

        private void databaseListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                contactControl.Contact = (Employee)SelectedContact.Clone();
                contactControl.Contact.Category = (Department)SelectedContact.Category.Clone();
                
            }
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (databaseListView.SelectedItems.Count < 1)
                return;
            if (database.Update(contactControl.Contact) > 0)
            {
                int temp = ContactList.IndexOf(SelectedContact);
                ContactList[ContactList.IndexOf(SelectedContact)] = (Employee)contactControl.Contact.Clone();
                databaseListView.SelectedItem = ContactList[temp];
                
            }                  
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ContactEditor editor = new ContactEditor();
            if (editor.ShowDialog() == true)
            {
                if (database.Add(editor.Contact) > 0)
                    MessageBox.Show("Запись успешно добавлена", "Добавление записи", MessageBoxButton.OK, MessageBoxImage.Information);
                else MessageBox.Show("Сотрудник с таким номером уже есть", "Добавление записи не удалось", MessageBoxButton.OK);
            }
        }

        private void btnAdd_Copy_Click(object sender, RoutedEventArgs e)
        {
            DepartmentEditor window = new DepartmentEditor();
         window.Show();
                 
        }

            private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (databaseListView.SelectedItems.Count < 1)
                return;

            if (MessageBox.Show("Вы действительно желаете удалить Сотрудника?", "Удаление сотрудника", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (database.Remove((Employee)databaseListView.SelectedItems[0]) > 0)
                    MessageBox.Show("Запись успешно удалена", "Удаление записи", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
