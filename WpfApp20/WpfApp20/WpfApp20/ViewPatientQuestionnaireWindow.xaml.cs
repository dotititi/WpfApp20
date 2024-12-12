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
    /// Логика взаимодействия для ViewPatientQuestionnaireWindow.xaml
    /// </summary>
    public partial class ViewPatientQuestionnaireWindow : Window
    {
        private int PatientId {  get; set; }
        test1entities db = new test1entities();
        public ViewPatientQuestionnaireWindow(int patientId)
        {
            InitializeComponent();
            PatientId = patientId;
            QuestionnaireDataGrid.ItemsSource = db.PatientResponse.Where(q => q.patient_id == PatientId).ToList();
        }

        private void OpenTestDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedResponse = QuestionnaireDataGrid.SelectedValue as PatientResponse;
            if (selectedResponse != null)
            {
                ViewQuestionnaireResultWindow viewQuestionnaireResultWindow = new ViewQuestionnaireResultWindow(selectedResponse);
                viewQuestionnaireResultWindow.ShowDialog();
                QuestionnaireDataGrid.ItemsSource = null;
                QuestionnaireDataGrid.ItemsSource = db.PatientResponse.Where(q => q.patient_id == PatientId).ToList();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
                return;
            }
        }

        private void ChangeTestDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedResponse = QuestionnaireDataGrid.SelectedValue as PatientResponse;
            if (selectedResponse != null)
            {
                ChangeQuestionnaireResultWindow changeQuestionnaireResultWindow = new ChangeQuestionnaireResultWindow(selectedResponse);
                changeQuestionnaireResultWindow.ShowDialog();
                QuestionnaireDataGrid.ItemsSource = null;
                QuestionnaireDataGrid.ItemsSource = db.PatientResponse.Where(q => q.patient_id == PatientId).ToList();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
                return;
            }
        }

        private void DeleteTest_Click(object sender, RoutedEventArgs e)
        {
            PatientResponse patientResponse = QuestionnaireDataGrid.SelectedValue as PatientResponse;
            if (patientResponse == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                db.PatientResponse.Remove(patientResponse);
                db.SaveChanges();
                QuestionnaireDataGrid.ItemsSource = null;
                QuestionnaireDataGrid.ItemsSource = db.PatientResponse.Where(q => q.patient_id == PatientId).ToList();

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
