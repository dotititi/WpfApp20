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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp20
{
    /// <summary>
    /// Логика взаимодействия для ChangeQuestionnaireWindow.xaml
    /// </summary>
    public partial class ChangeQuestionnaireWindow : Window
    {
        private Questionnaire CurrentQuestionnaire;
        public ChangeQuestionnaireWindow(Questionnaire questionnaire)
        {
            InitializeComponent();
            CurrentQuestionnaire = questionnaire;
            TitleTextBox.Text = questionnaire.title;
            DescriptionTextBox.Text = questionnaire.description;
        }
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentQuestionnaire.title = TitleTextBox.Text;
            CurrentQuestionnaire.description = DescriptionTextBox.Text;

            if (string.IsNullOrEmpty(TitleTextBox.Text) || string.IsNullOrEmpty(DescriptionTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                using (var db = new test1entities())
                {
                    var recordToUpdate = db.Questionnaire.SingleOrDefault(q => q.id == CurrentQuestionnaire.id);
                    if (recordToUpdate != null)
                    {
                        recordToUpdate.title = CurrentQuestionnaire.title;
                        recordToUpdate.description = CurrentQuestionnaire.description;
                        db.SaveChanges();
                        MessageBox.Show("Запись успешно обновлена.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Запись не найдена в базе данных.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось обновить запись: " + ex.Message);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
