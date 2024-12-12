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
    /// Логика взаимодействия для QuestionnaireWindow.xaml
    /// </summary>
    public partial class QuestionnaireWindow : Window
    {
        private int PatientId { get; set; }
        private int DoctorId { get; set; }
        public QuestionnaireWindow(int patientId, int doctorId)
        {
            InitializeComponent();
            PatientId = patientId;
            DoctorId = doctorId;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TitleTextBox.Text) || string.IsNullOrEmpty(DescriptionTextBox.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Questionnaire questionnaire = new Questionnaire()
            {
                title = TitleTextBox.Text,
                description = DescriptionTextBox.Text,
                doctor_id = DoctorId,
                patient_id = PatientId
            };

            try
            {
                using (var db = new test1entities())
                {
                    db.Questionnaire.Add(questionnaire);
                    db.SaveChanges();
                }

                MessageBox.Show("Опросник успешно сохранен.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить план рекомендацию: " + ex.Message);
            }
        }



        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
