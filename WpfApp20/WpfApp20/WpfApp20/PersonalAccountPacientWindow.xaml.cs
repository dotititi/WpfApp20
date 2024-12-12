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
    /// Логика взаимодействия для PersonalAccountPacientWindow.xaml
    /// </summary>
    public partial class PersonalAccountPacientWindow : Window
    {
        private int UserId { get; set; }
        test1entities db = new test1entities();
        private Patient existingPatient;
        public PersonalAccountPacientWindow(int userId)
        {
            InitializeComponent();
            UserId = userId;
            LoadPatientData();
        }

        private void LoadPatientData()
        {
            existingPatient = db.Patient.FirstOrDefault(p => p.user_id == UserId);

            if (existingPatient != null)
            {
                txbFirstName.Text = existingPatient.name;
                txbLastName.Text = existingPatient.fullname;
                txbMiddleName.Text = existingPatient.middlename;
                dpBirthDate.SelectedDate = existingPatient.birthday.Date;

            }
        }
        private bool IsTextValid(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsLetter(c) && c != ' ')
                {
                    return false;
                }
            }
            return true;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbLastName.Text) || 
                string.IsNullOrWhiteSpace(txbFirstName.Text) || 
                string.IsNullOrWhiteSpace(txbMiddleName.Text) || 
                !dpBirthDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Всё поля должны быть заполнены.");
                return;
            }
           
            if (!IsTextValid(txbFirstName.Text) || !IsTextValid(txbLastName.Text) || !IsTextValid(txbMiddleName.Text))
            {
                MessageBox.Show("Поля содержат недопустимые символы.");
                return;
            }



            try
            {
                if (existingPatient != null)
                {
                    existingPatient.name = txbFirstName.Text;
                    existingPatient.fullname = txbLastName.Text;
                    existingPatient.middlename = txbMiddleName.Text;
                    existingPatient.birthday = dpBirthDate.SelectedDate.Value.Date;

                    db.SaveChanges();
                }
                else
                {
                    Patient newPatient = new Patient
                    {
                        name = txbFirstName.Text,
                        fullname = txbLastName.Text,
                        middlename = txbMiddleName.Text,
                        birthday = dpBirthDate.SelectedDate.Value.Date,
                        user_id = UserId
                    };

                    db.Patient.Add(newPatient);
                    db.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены.");
                }

                this.Close();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить данные.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
