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

namespace Phonebook.Controls
{
    public partial class ContactControl : UserControl
    {

        private Employee contact;

        public ContactControl()
        {
            InitializeComponent();

            cbCatagory.ItemsSource = Enum.GetValues(typeof(Department)).Cast<Department>();
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

        public void SetContact(Employee contact)
        {
            this.contact = contact;
            tbPhone.Text = contact.Phone;
            tbFirstName.Text = contact.FirstName;
            tbLastName.Text = contact.LastName;
            tbSecondName.Text = contact.SecondName;
            cbCatagory.SelectedItem = contact.Category;
            tbComment.Text = contact.Comment;

        }

        public Employee UpdateContact()
        {
            contact.Phone = tbPhone.Text;
            contact.FirstName = tbFirstName.Text;
            contact.LastName = tbLastName.Text;
            contact.SecondName = tbSecondName.Text;
            contact.Category = (Department)cbCatagory.SelectedItem;
            contact.Comment = tbComment.Text;

            return contact;
        }
    }
}
