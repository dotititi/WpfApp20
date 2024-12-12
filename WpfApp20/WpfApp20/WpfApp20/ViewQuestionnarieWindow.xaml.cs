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
    /// Логика взаимодействия для ViewQuestionnarieWindow.xaml
    /// </summary>
    public partial class ViewQuestionnarieWindow : Window
    {
        test1entities db = new test1entities();
        private int PatientId { get; set; }
        private int DoctorId { get; set; }
        public ViewQuestionnarieWindow(Patient selectedPatient, int patientId, int doctorId)
        {
            InitializeComponent();
            PatientId = patientId;
            DoctorId = doctorId;
            var patientQuestionnaire = db.Questionnaire.Where(q => q.patient_id == PatientId).ToList();
            GridQuestionnaire.ItemsSource = patientQuestionnaire;
            FioTextBlock.Text = $"{selectedPatient.fullname} {selectedPatient.name} {selectedPatient.middlename}";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedRecord = GridQuestionnaire.SelectedItem as Questionnaire;

            if (selectedRecord != null)
            {
                ShowQuestionnaireInfoWindow showQuestionnaireInfoWindow = new ShowQuestionnaireInfoWindow(selectedRecord);
                showQuestionnaireInfoWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            QuestionnaireWindow questionnaireWindow = new QuestionnaireWindow(PatientId, DoctorId);
            questionnaireWindow.ShowDialog();
            var patientQuestionnaire = db.Questionnaire.Where(q => q.patient_id == PatientId).ToList();
            GridQuestionnaire.ItemsSource = null;
            GridQuestionnaire.ItemsSource = patientQuestionnaire;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var selectedRecord = GridQuestionnaire.SelectedItem as Questionnaire;

            if (selectedRecord != null)
            {
                ChangeQuestionnaireWindow changeQuestionnaireWindow = new ChangeQuestionnaireWindow(selectedRecord);
                changeQuestionnaireWindow.ShowDialog();
                var patientQuestionnaire = db.Questionnaire.Where(q => q.patient_id == PatientId).ToList();
                GridQuestionnaire.ItemsSource = null;
                GridQuestionnaire.ItemsSource = patientQuestionnaire;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Questionnaire questionnaire = GridQuestionnaire.SelectedValue as Questionnaire;
            if (questionnaire == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                db.Questionnaire.Remove(questionnaire);
                db.SaveChanges();
                GridQuestionnaire.ItemsSource = null;
                GridQuestionnaire.ItemsSource = db.Questionnaire.Where(q => q.patient_id == PatientId).ToList();

            }
            catch
            {
                MessageBox.Show("Невозможно удалить запись");
            }
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ViewQuestionnairePacientResultWindow viewQuestionnairePacientResultWindow = new ViewQuestionnairePacientResultWindow(PatientId);
            viewQuestionnairePacientResultWindow.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
