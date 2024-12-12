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
    /// Логика взаимодействия для ViewPacientRecordWindow.xaml
    /// </summary>
    public partial class ViewPacientRecordWindow : Window
    {
        test1entities db = new test1entities();
        private int PatientId { get; set; }
        private int DoctorId { get; set; }
        public ViewPacientRecordWindow(Patient selectedPatient, int patientId, int doctorId)
        {
            InitializeComponent();
            PatientId = patientId;
            DoctorId = doctorId;
            var patientPacientRecord = db.PatientRecord.Where(p => p.patient_id == PatientId).ToList();
            GridPatientRecord.ItemsSource = patientPacientRecord;
            FioTextBlock.Text = $"{selectedPatient.fullname} {selectedPatient.name} {selectedPatient.middlename}";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedRecord = GridPatientRecord.SelectedItem as PatientRecord;

            if (selectedRecord != null)
            {
                ShowPatientRecordInfoWindow showPatientRecordWindow = new ShowPatientRecordInfoWindow(selectedRecord);
                showPatientRecordWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PatientRecordWindow1xaml patientRecordWindow1Xaml = new PatientRecordWindow1xaml(PatientId, DoctorId);
            patientRecordWindow1Xaml.ShowDialog();
            GridPatientRecord.ItemsSource = null;
            var patientPacientRecord = db.PatientRecord.Where(p => p.patient_id == PatientId).ToList();
            GridPatientRecord.ItemsSource = patientPacientRecord;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var selectedRecord = GridPatientRecord.SelectedItem as PatientRecord;

            if (selectedRecord != null)
            {
                ChangePatientRecordWindow changePatientRecordWindow = new ChangePatientRecordWindow(selectedRecord);
                changePatientRecordWindow.ShowDialog();
                GridPatientRecord.ItemsSource = null;
                var patientPacientRecord = db.PatientRecord.Where(p => p.patient_id == PatientId).ToList();
                GridPatientRecord.ItemsSource = patientPacientRecord;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            PatientRecord patientRecord = GridPatientRecord.SelectedValue as PatientRecord;
            if (patientRecord == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                db.PatientRecord.Remove(patientRecord);
                db.SaveChanges();
                GridPatientRecord.ItemsSource = null;
                GridPatientRecord.ItemsSource = db.PatientRecord.Where(p => p.patient_id == PatientId).ToList();

            }
            catch
            {
                MessageBox.Show("Невозможно удалить запись");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
