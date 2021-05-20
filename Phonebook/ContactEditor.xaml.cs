using Phonebook.Data;
using Phonebook.Controls;
using System.Windows;

namespace Phonebook
{
    /// <summary>
    /// Interaction logic for ContactEditor.xaml
    /// </summary>
    public partial class ContactEditor : Window
    {

        private EditorType editorType;

        public ContactEditor()
        {
            InitializeComponent();

            editorType = EditorType.Add;
            Contact.Category = Department.Departments[0];
            contactControl.Contact = Contact;
            contactControl.Contact.Category = (Department)Contact.Category.Clone();
            PrepareUI();
        }

        public ContactEditor(EditorType editorType)
        {
            InitializeComponent();

            this.editorType = editorType;
            Contact.Category = Department.Departments[0];
            contactControl.Contact = Contact;
            contactControl.Contact.Category = (Department)Contact.Category.Clone();
            PrepareUI();
        }

        private void PrepareUI()
        {
            switch (editorType)
            {
                case EditorType.Add:
                    this.Title = $"{this.Title} [Добавление]";
                    break;
                case EditorType.Edit:
                    this.Title = $"{this.Title} [Правка]";
                    break;
            }
            contactControl.PrepareUI(editorType);
        }

        public Employee Contact { get; set; } = new Employee();

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
