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
    /// Логика взаимодействия для QuestionnaireDetailsWindow.xaml
    /// </summary>
    public partial class QuestionnaireDetailsWindow : Window
    {
        private int QuestionnaireId;
        public QuestionnaireDetailsWindow(int questionnaireId)
        {
            InitializeComponent();
            QuestionnaireId = questionnaireId;
            LoadTestDetails();
        }
        private void LoadTestDetails()
        {
            using (var db = new test1entities())
            {
                var questionnaire = db.Questionnaire.FirstOrDefault(q => q.id == QuestionnaireId);
                if (questionnaire != null)
                {
                    TitleTextBlock.Text = questionnaire.title;
                    DescriptionTextBlock.Text = questionnaire.description;
                }
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
