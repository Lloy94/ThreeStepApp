using Phonebook.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
namespace Phonebook.Controls
{
    public partial class ContactControl : UserControl, INotifyPropertyChanged

    {

        private Employee contact;

        public Employee Contact
        {
            get { return contact; }
            set
            {
                contact = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Department> Departments { get; set; } = new ObservableCollection<Department>();
        public ContactControl()
        {
            Departments.Add(new Department(0, "IT"));
            Departments.Add(new Department(1, "Склад"));
            Departments.Add(new Department(2, "Охрана"));
            Departments.Add(new Department(3, "Бухгалтерия"));
            Departments.Add(new Department(4, "Управление"));
            InitializeComponent();

            this.DataContext = this;

        }

        public void PrepareUI(EditorType editorType)
        {
            switch (editorType)
            {
                case EditorType.Add:
                    this.tbPhone.IsReadOnly = false;
                    this.tbPhone.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF")); /*#FFF1EFEF*/
                    break;
                case EditorType.Edit:
                    break;
            }
        }

        /*public void SetContact(Employee contact)
        {
            this.contact = contact;
            tbPhone.Text = contact.Phone;
            tbFirstName.Text = contact.FirstName;
            tbLastName.Text = contact.LastName;
            tbSecondName.Text = contact.SecondName;
            cbCatagory.SelectedItem = contact._department;
            tbComment.Text = contact.Comment;

        }

        public Employee UpdateContact()
        {
            contact.Phone = tbPhone.Text;
            contact.FirstName = tbFirstName.Text;
            contact.LastName = tbLastName.Text;
            contact.SecondName = tbSecondName.Text;
            contact.Category.Category = (string)cbCatagory.SelectedItem;
            contact.Comment = tbComment.Text;

            return contact;
        }*/

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}