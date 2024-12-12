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
    /// Логика взаимодействия для PersonalAccountDoctorWindow.xaml
    /// </summary>
    public partial class PersonalAccountDoctorWindow : Window
    {
        private int UserId { get; set; }
        test1entities db = new test1entities();
        private Doctor existingDoctor;

        public PersonalAccountDoctorWindow(int userId)
        {
            InitializeComponent();
            UserId = userId;

            LoadDoctorData();
        }


        private void LoadDoctorData()
        {
            existingDoctor = db.Doctor.FirstOrDefault(d => d.user_id == UserId);

            if (existingDoctor != null)
            {
                txbFirstName.Text = existingDoctor.name;
                txbLastName.Text = existingDoctor.fullname;
                txbMiddleName.Text = existingDoctor.middlename;
                txbHospital.Text = existingDoctor.hospital;
                txbSpecialty.Text = existingDoctor.speciality;
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
                string.IsNullOrWhiteSpace(txbHospital.Text) ||
                string.IsNullOrWhiteSpace(txbSpecialty.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            
            if (!IsTextValid(txbFirstName.Text) || !IsTextValid(txbLastName.Text) || !IsTextValid(txbMiddleName.Text))
            {
                MessageBox.Show("Поля содержат недопустимые символы.");
                return;
            }



            try
            {
                if (existingDoctor != null)
                {
                    existingDoctor.name = txbFirstName.Text;
                    existingDoctor.fullname = txbLastName.Text;
                    existingDoctor.middlename = txbMiddleName.Text;
                    existingDoctor.hospital = txbHospital.Text;
                    existingDoctor.speciality = txbSpecialty.Text;

                    db.SaveChanges();
                }
                else
                {
                    Doctor newDoctor = new Doctor
                    {
                        name = txbFirstName.Text,
                        fullname = txbLastName.Text,
                        middlename = txbMiddleName.Text,
                        hospital = txbHospital.Text,
                        speciality = txbSpecialty.Text,
                        user_id = UserId
                    };

                    db.Doctor.Add(newDoctor);
                    db.SaveChanges();
                }

                MessageBox.Show("Данные успешно сохранены.");
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
