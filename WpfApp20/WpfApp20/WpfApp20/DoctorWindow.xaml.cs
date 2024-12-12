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
    /// Логика взаимодействия для DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        private int UserId { get; set; }
        test1entities db = new test1entities();
        public DoctorWindow(int userId)
        {
            InitializeComponent();
            UserId = userId;
            GridPatient.ItemsSource = db.Patient.ToList();
        }
        private void PersonalAccountMenuItem_Click(object sender, RoutedEventArgs e)
        {
            PersonalAccountDoctorWindow personalAccountDoctorWindow = new PersonalAccountDoctorWindow(UserId);
            personalAccountDoctorWindow.Show();
        }

        private void LogoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
   
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedPatient = GridPatient.SelectedItem as Patient;

            if (selectedPatient != null)
            {
                InfoAboutPacientWindow infoAboutPacientWindow = new InfoAboutPacientWindow(selectedPatient);
                infoAboutPacientWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пациента из списка.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var selectedPatient = GridPatient.SelectedItem as Patient;

            if (selectedPatient != null)
            {
                int doctorId = GetDoctorId(UserId); 

                if (doctorId > 0)
                {
                    ViewTestWindow viewTestWindow = new ViewTestWindow(selectedPatient,selectedPatient.id, doctorId);
                    viewTestWindow.ShowDialog();

                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пациента из списка.");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var selectedPatient = GridPatient.SelectedItem as Patient;

            if (selectedPatient != null)
            {
                int doctorId = GetDoctorId(UserId); 

                if (doctorId > 0)
                {
                    ViewQuestionnarieWindow viewQuestionnarieWindow = new ViewQuestionnarieWindow(selectedPatient,selectedPatient.id, doctorId);
                    viewQuestionnarieWindow.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пациента из списка.");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var selectedPatient = GridPatient.SelectedItem as Patient;

            if (selectedPatient != null)
            {
                int doctorId = GetDoctorId(UserId); 

                if (doctorId > 0)
                {
                    ViewDocumentWindow viewDocumentWindow = new ViewDocumentWindow(selectedPatient,selectedPatient.id, doctorId);
                    viewDocumentWindow.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пациента из списка.");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var selectedPatient = GridPatient.SelectedItem as Patient;

            if (selectedPatient != null)
            {
                int doctorId = GetDoctorId(UserId); 
                if (doctorId > 0)
                {
                    ViewTreatmentPlanWindow viewTreatmentPlanWindow = new ViewTreatmentPlanWindow(selectedPatient,selectedPatient.id, doctorId);
                    viewTreatmentPlanWindow.ShowDialog();
 
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пациента из списка.");
            }
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            var selectedPatient = GridPatient.SelectedItem as Patient;

            if (selectedPatient != null)
            {
                int doctorId = GetDoctorId(UserId); 

                if (doctorId > 0)
                {
                    ViewRecommendationWindow viewRecommendationWindow = new ViewRecommendationWindow(selectedPatient,selectedPatient.id, doctorId);
                    viewRecommendationWindow.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пациента из списка.");
            }
        }
        private int GetDoctorId(int userId)
        {
            var doctor = db.Doctor.FirstOrDefault(d => d.user_id == userId);
            return doctor?.id ?? 0; 
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            var selectedPatient = GridPatient.SelectedItem as Patient;

            if (selectedPatient != null)
            {
                int doctorId = GetDoctorId(UserId);

                if (doctorId > 0)
                {
                    ViewPacientRecordWindow viewPacientRecordWindow = new ViewPacientRecordWindow(selectedPatient, selectedPatient.id, doctorId);
                    viewPacientRecordWindow.ShowDialog();
                    
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пациента из списка.");
            }
        }
    }
}
