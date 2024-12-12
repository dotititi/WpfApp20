using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для ChangePatientRecordWindow.xaml
    /// </summary>
    public partial class ChangePatientRecordWindow : Window
    {
        private PatientRecord CurrentRecord;
        public ChangePatientRecordWindow(PatientRecord record)
        {
            InitializeComponent();
            CurrentRecord = record;
            ReasonTextBox.Text = record.reason;
            DiagnosisTextBox.Text = record.diagnosis;
            DateOfVisitPicker.SelectedDate = record.date_of_visit;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentRecord.reason = ReasonTextBox.Text;
            CurrentRecord.diagnosis = DiagnosisTextBox.Text;
            CurrentRecord.date_of_visit = DateOfVisitPicker.SelectedDate.Value.Date;

            if (string.IsNullOrWhiteSpace(ReasonTextBox.Text) ||
              string.IsNullOrWhiteSpace(DiagnosisTextBox.Text) ||
              !DateOfVisitPicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            } 

            try
            {
                using (var db = new test1entities())
                {
                    var recordToUpdate = db.PatientRecord.SingleOrDefault(r => r.id == CurrentRecord.id);
                    if (recordToUpdate != null)
                    {
                        recordToUpdate.reason = CurrentRecord.reason;
                        recordToUpdate.diagnosis = CurrentRecord.diagnosis;
                        recordToUpdate.date_of_visit = CurrentRecord.date_of_visit;
                        db.SaveChanges();
                        MessageBox.Show("Запись успешно обновлена.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Запись не найдена в базе данных.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось обновить запись: " + ex.Message);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
