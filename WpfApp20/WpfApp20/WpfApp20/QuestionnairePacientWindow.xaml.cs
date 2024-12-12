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
    /// Логика взаимодействия для QuestionnairePacientWindow.xaml
    /// </summary>
    public partial class QuestionnairePacientWindow : Window
    {
        private int PatientId;

        public QuestionnairePacientWindow(int patientId)
        {
            InitializeComponent();
            PatientId = patientId;
            LoadQuestionnaires();
        }

        private void LoadQuestionnaires()
        {
            using (var db = new test1entities())
            {
                var questionnaires = db.Questionnaire.Where(Й => Й.patient_id == PatientId).ToList();
                QuestionnaireDataGrid.ItemsSource = questionnaires;
            }
        }
        private void OpenQuestionnaireDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedQuestionnaire = QuestionnaireDataGrid.SelectedItem as Questionnaire;
            if (selectedQuestionnaire == null)
            {
                MessageBox.Show("Пожалуйста, выберите тест.");
                return;
            }
            QuestionnaireDetailsWindow questionnaireDetailsWindow = new QuestionnaireDetailsWindow(selectedQuestionnaire.id);
            questionnaireDetailsWindow.ShowDialog();
        }

        private void PassQuestionnaire_Click(object sender, RoutedEventArgs e)
        {
            var selectedQuestionnaire = QuestionnaireDataGrid.SelectedItem as Questionnaire;
            if (selectedQuestionnaire == null)
            {
                MessageBox.Show("Пожалуйста, выберите тест.");
                return;
            }
            ResponseQuestionnaireWindow responseQuestionnaireWindow = new ResponseQuestionnaireWindow(selectedQuestionnaire);
            responseQuestionnaireWindow.ShowDialog();
        }

        private void QuestionnaireResult_Click(object sender, RoutedEventArgs e)
        {
            ViewPatientQuestionnaireWindow viewPatientQuestionnaireWindow = new ViewPatientQuestionnaireWindow(PatientId);
            viewPatientQuestionnaireWindow.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
