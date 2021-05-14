using Phonebook.Data;
using System;
using System.Collections.Generic;
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

namespace Phonebook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseDatabase database = new DatabaseDatabase();

        public MainWindow()
        {
            InitializeComponent();

            databaseListView.ItemsSource = database.Contacts;
        }

        private void databaseListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                contactControl.SetContact(e.AddedItems[0] as Employee);
            }
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (databaseListView.SelectedItems.Count < 1)
                return;

            contactControl.UpdateContact();
            UpdateBinding();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ContactEditor editor = new ContactEditor();
            if (editor.ShowDialog() == true)
            {
                database.Contacts.Add(editor.Contact);
                UpdateBinding();
            }
        }

        private void UpdateBinding()
        {
            databaseListView.ItemsSource = null;
            databaseListView.ItemsSource = database.Contacts;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (databaseListView.SelectedItems.Count < 1)
                return;

            if (MessageBox.Show("Вы действительно желаете удалить Сотрудника?", "Удаление сотрудника", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                database.Contacts.Remove((Employee)databaseListView.SelectedItems[0]);
                UpdateBinding();
            }
        }
    }
}
