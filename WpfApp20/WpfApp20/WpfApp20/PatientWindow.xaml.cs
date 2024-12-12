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
    /// Логика взаимодействия для PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        private int UserId { get; set; }
        test1entities db = new test1entities();
        private int PatientId { get; set; }
        public PatientWindow(int userId)
        {
            InitializeComponent();
            UserId = userId;
            var patient = db.Patient.FirstOrDefault(p => p.user_id == UserId);
            if (patient != null)
            {
                PatientId = patient.id; 
            }
        }
        
        private void PersonalAccountMenuItem_Click(object sender, RoutedEventArgs e)
        {
            PersonalAccountPacientWindow personalAccountPacientWindow = new PersonalAccountPacientWindow(UserId);
            personalAccountPacientWindow.Show();
        }
        private void LogoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void OpenTestWindow_Click(object sender, RoutedEventArgs e)
        {
            TestPacientWindow testPacientWindow = new TestPacientWindow(PatientId);
            testPacientWindow.Show();
        }

        private void OpenQuestionnaireWindow_Click(object sender, RoutedEventArgs e)
        {
            QuestionnairePacientWindow questionnairePacientWindow = new QuestionnairePacientWindow(PatientId);
            questionnairePacientWindow.Show();
        }

        private void OpenDocumentsWindow_Click(object sender, RoutedEventArgs e)
        {
            DocumentPatientWindow documentPatientWindow = new DocumentPatientWindow(PatientId);
            documentPatientWindow.Show();
        }

        private void OpenRecommendationWindow_Click(object sender, RoutedEventArgs e)
        {
            RecommendationPacientWindow recommendationPacientWindow = new RecommendationPacientWindow(PatientId);
            recommendationPacientWindow.Show();
        }
    }
}
