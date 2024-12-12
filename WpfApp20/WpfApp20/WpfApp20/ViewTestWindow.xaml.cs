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
    /// Логика взаимодействия для ViewTestWindow.xaml
    /// </summary>
    public partial class ViewTestWindow : Window
    {
        test1entities db = new test1entities();
        private int PatientId { get; set; }
        private int DoctorId { get; set; }
        public ViewTestWindow(Patient selectedPatient,int patientId, int doctorId)
        {
            InitializeComponent();
            PatientId = patientId;
            DoctorId = doctorId;
            var patientTests = db.Test.Where(t => t.patient_id == PatientId).ToList();
            GridTest.ItemsSource = patientTests;
            FioTextBlock.Text = $"{selectedPatient.fullname} {selectedPatient.name} {selectedPatient.middlename}";
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedRecord = GridTest.SelectedItem as Test;

            if (selectedRecord != null)
            {
                ShowTestInfoWindow showTestInfoWindow = new ShowTestInfoWindow(selectedRecord);
                showTestInfoWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TestWindow testWindow = new TestWindow(PatientId, DoctorId);
            testWindow.ShowDialog();
            var patientTest = db.Test.Where(t => t.patient_id == PatientId).ToList();
            GridTest.ItemsSource = null;
            GridTest.ItemsSource = patientTest;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var selectedRecord = GridTest.SelectedItem as Test;

            if (selectedRecord != null)
            {
                ChangeTestWindow changeTestWindow = new ChangeTestWindow(selectedRecord);
                changeTestWindow.ShowDialog();
                var patientTest = db.Test.Where(t => t.patient_id == PatientId).ToList();
                GridTest.ItemsSource = null;
                GridTest.ItemsSource = patientTest;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Test test = GridTest.SelectedValue as Test;
            if (test == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                db.Test.Remove(test);
                db.SaveChanges();
                GridTest.ItemsSource = null;
                GridTest.ItemsSource = db.Test.Where(t => t.patient_id == PatientId).ToList();

            }
            catch
            {
                MessageBox.Show("Невозможно удалить запись");
            }
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ViewTestPacientResultWindow viewTestPacientResultWindow = new ViewTestPacientResultWindow(PatientId);
            viewTestPacientResultWindow.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
