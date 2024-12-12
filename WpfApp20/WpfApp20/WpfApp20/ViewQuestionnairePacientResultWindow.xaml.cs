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
    /// Логика взаимодействия для ViewQuestionnairePacientResultWindow.xaml
    /// </summary>
    public partial class ViewQuestionnairePacientResultWindow : Window
    {
        test1entities db = new test1entities();
        private int PatientId { get; set; }
        public ViewQuestionnairePacientResultWindow(int PatientId)
        {
            InitializeComponent();
            var patient = db.Patient.FirstOrDefault(p => p.id == PatientId);
            if (patient != null)
            {
                FioTextBlock.Text = $"{patient.fullname} {patient.name} {patient.middlename}";
            }
            var patientQuestionnaire = db.PatientResponse.Where(p => p.patient_id == PatientId).ToList();
            GridQuestionnaireResult.ItemsSource = patientQuestionnaire;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedResult = GridQuestionnaireResult.SelectedItem as PatientResponse;

            if (selectedResult != null)
            {
                Questionnaire selectedQuestionnaire = GetQuestionnaireByResult(selectedResult);

                if (selectedQuestionnaire != null)
                {

                    ShowQuestionnairePacientResultWindow showQuestionnairePacientResultWindow = new ShowQuestionnairePacientResultWindow(selectedQuestionnaire, selectedResult);
                    showQuestionnairePacientResultWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Не удалось найти связанный тест.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
            }
        }
        private Questionnaire GetQuestionnaireByResult(PatientResponse selectedResult)
        {
            using (var db = new test1entities())
            {
                return db.Questionnaire.FirstOrDefault(q => q.id == selectedResult.questionnaire_id);
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
