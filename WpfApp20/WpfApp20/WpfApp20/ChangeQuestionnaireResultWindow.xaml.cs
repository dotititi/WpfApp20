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
    /// Логика взаимодействия для ChangeQuestionnaireResultWindow.xaml
    /// </summary>
    public partial class ChangeQuestionnaireResultWindow : Window
    {
        private PatientResponse SelectedResponse { get; set; }
        private Questionnaire SelectedQuestionnaire { get; set; }
        private test1entities db = new test1entities();
        public ChangeQuestionnaireResultWindow(PatientResponse selectedResponse)
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
                ResultTextBox.Text = SelectedResponse.response;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ResultTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }
            if (SelectedResponse != null)
            {
                try
                {
                    SelectedResponse.response = ResultTextBox.Text;
                    db.SaveChanges();

                    MessageBox.Show("Ответ успешно сохранён.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
