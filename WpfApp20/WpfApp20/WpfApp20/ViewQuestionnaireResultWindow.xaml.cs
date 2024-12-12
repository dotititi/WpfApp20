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
    /// Логика взаимодействия для ViewQuestionnaireResultWindow.xaml
    /// </summary>
    public partial class ViewQuestionnaireResultWindow : Window
    {
        private PatientResponse SelectedResponse { get; set; }
        private Questionnaire SelectedQuestionnaire { get; set; }

        public ViewQuestionnaireResultWindow(PatientResponse selectedResponse)
        {
            InitializeComponent();
            SelectedResponse = selectedResponse;
            SelectedQuestionnaire = SelectedResponse.Questionnaire;
            LoadData();
        }

        private void LoadData()
        {
            if (SelectedQuestionnaire != null)
            {
                TitleTextBlock.Text = SelectedQuestionnaire.title;
                DescriptionTextBlock.Text = SelectedQuestionnaire.description;
            }

            if (SelectedResponse != null)
            {
                ResultTextBlock.Text = SelectedResponse.response;
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
