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
    /// Логика взаимодействия для PassQuestionnaireWindow.xaml
    /// </summary>
    public partial class ResponseQuestionnaireWindow : Window
    {
        private Questionnaire SelectedQuestionnaire {  get; set; }
        public ResponseQuestionnaireWindow(Questionnaire selectedQuestionnaire)
        {
            InitializeComponent();
            SelectedQuestionnaire = selectedQuestionnaire;
            LoadQuestionnaireDetails();
        }
        private void LoadQuestionnaireDetails()
        {
            TitleTextBlock.Text = SelectedQuestionnaire.title;
            DescriptionTextBlock.Text = SelectedQuestionnaire.description;
        }
        private void SaveResponseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string response = ResponseTextBox.Text;

                if (string.IsNullOrWhiteSpace(response))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (var context = new test1entities())
                {
                    var patientResponse = new PatientResponse
                    {
                        questionnaire_id = SelectedQuestionnaire.id, 
                        patient_id = SelectedQuestionnaire.patient_id,             
                        response = response 
                    };

                    context.PatientResponse.Add(patientResponse);
                    context.SaveChanges();
                }

                MessageBox.Show("Ответ успешно сохранён.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении ответа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
