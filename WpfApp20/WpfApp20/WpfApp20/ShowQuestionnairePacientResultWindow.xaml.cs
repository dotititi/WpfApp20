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
    /// Логика взаимодействия для ShowQuestionnairePacientResultWindow.xaml
    /// </summary>
    public partial class ShowQuestionnairePacientResultWindow : Window
    {
        private Questionnaire SelectedQuestionnaire{ get; set; }
        private PatientResponse SelectedResult { get; set; }
        public ShowQuestionnairePacientResultWindow(Questionnaire selectedQuestionnaire, PatientResponse selectedResult)
        {
            InitializeComponent();
            SelectedQuestionnaire = selectedQuestionnaire;
            SelectedResult = selectedResult;
            LoadQuestionnaireAndResponseDetails();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadQuestionnaireAndResponseDetails()
        {
            TitleTextBlock.Text = SelectedQuestionnaire.title;
            DescriptionTextBlock.Text = SelectedQuestionnaire.description;

            if (SelectedResult != null)
            {
                ResultTextBlock.Text = SelectedResult.response;       
            }
            else
            {
                ResultTextBlock.Text = "Результат не найден.";
            }
        }
    }
}
