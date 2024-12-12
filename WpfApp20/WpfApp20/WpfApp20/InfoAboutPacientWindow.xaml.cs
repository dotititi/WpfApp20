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
using System.Windows.Shapes;

namespace WpfApp20
{
    /// <summary>
    /// Логика взаимодействия для InfoAboutPacientWindow.xaml
    /// </summary>
    public partial class InfoAboutPacientWindow : Window
    {
        private Patient SelectedPatient { get; set; }
        public InfoAboutPacientWindow(Patient selectedPatient)
        {
            InitializeComponent();
            SelectedPatient = selectedPatient;
            DisplayPatientInfo();
        }

        private void DisplayPatientInfo()
        {
            if (SelectedPatient != null)
            {
                FirstNameTextBlock.Text = SelectedPatient.name; 
                LastNameTextBlock.Text = SelectedPatient.fullname;
                MiddleNameTextBlock.Text = SelectedPatient.middlename; 
                BirthdayTextBlock.Text = SelectedPatient.birthday.ToShortDateString();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

    }
}
