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
    /// Логика взаимодействия для ViewDocumentWindow.xaml
    /// </summary>
    public partial class ViewDocumentWindow : Window
    {
        test1entities db = new test1entities();
        private int PatientId { get; set; }
        private int DoctorId { get; set; }
        public ViewDocumentWindow(Patient selectedPatient,int patientId, int doctorId)
        {
            InitializeComponent();
            PatientId = patientId;
            DoctorId = doctorId;
            var patientDocuments = db.Documents.Where(d => d.patient_id == PatientId).ToList();
            GridDocument.ItemsSource = patientDocuments;
            FioTextBlock.Text = $"{selectedPatient.fullname} {selectedPatient.name} {selectedPatient.middlename}";
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedRecord = GridDocument.SelectedItem as Documents;

            if (selectedRecord != null)
            {
                ShowDocumentInfoWindow showDocumentInfoWindow = new ShowDocumentInfoWindow(selectedRecord);
                showDocumentInfoWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddDocumentWindow addDocumentWindow = new AddDocumentWindow(PatientId, DoctorId);
            addDocumentWindow.ShowDialog();
            var patientDocuments = db.Documents.Where(d => d.patient_id == PatientId).ToList();
            GridDocument.ItemsSource = null;
            GridDocument.ItemsSource = patientDocuments;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var selectedRecord = GridDocument.SelectedItem as Documents;

            if (selectedRecord != null)
            {
                ChangeDocumentWindow changeDocumentWindow = new ChangeDocumentWindow(selectedRecord.id, selectedRecord.patient_id, selectedRecord.doctor_id);
                changeDocumentWindow.ShowDialog();
                using (var db = new test1entities()) 
                {
                    var patientDocuments = db.Documents.Where(d => d.patient_id == PatientId).ToList();
                    GridDocument.ItemsSource = patientDocuments;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Documents documents = GridDocument.SelectedValue as Documents;
            if (documents == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                db.Documents.Remove(documents);
                db.SaveChanges();
                using (var db = new test1entities())
                {
                    var patientDocuments = db.Documents.Where(d => d.patient_id == PatientId).ToList();
                    GridDocument.ItemsSource = patientDocuments;
                }

            }
            catch
            {
                MessageBox.Show("Невозможно удалить запись");
            }
        }
 
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ShowDocumentResponseWindow showDocumentResponseWindow = new ShowDocumentResponseWindow(PatientId);
            showDocumentResponseWindow.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
