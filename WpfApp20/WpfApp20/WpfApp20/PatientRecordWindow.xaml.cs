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
    /// Логика взаимодействия для PatientRecordWindow1xaml.xaml
    /// </summary>
    public partial class PatientRecordWindow1xaml : Window
    {
        private int PatientId { get; set; }
        private int DoctorId { get; set; }
        public PatientRecordWindow1xaml(int patientId, int doctorId)
        {
            InitializeComponent();
            PatientId = patientId;
            DoctorId = doctorId;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ReasonTextBox.Text) ||
              string.IsNullOrWhiteSpace(DiagnosisTextBox.Text) ||
              !DateOfVisitPicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }


            PatientRecord record = new PatientRecord()
            {
                reason = ReasonTextBox.Text,
                diagnosis = DiagnosisTextBox.Text,
                date_of_visit = DateOfVisitPicker.SelectedDate.Value.Date,  
                doctor_id = DoctorId,
                patient_id = PatientId
            };

            try
            {
                using (var db = new test1entities())
                {
                    db.PatientRecord.Add(record);
                    db.SaveChanges();
                }

                MessageBox.Show("Запись успешно сохранен.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить план рекомендацию: " + ex.Message);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
