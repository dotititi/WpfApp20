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
    /// Логика взаимодействия для TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private int PatientId { get; set; }
        private int DoctorId { get; set; }
        public TestWindow(int patientId, int doctorId)
        {
            InitializeComponent();
            PatientId = patientId;
            DoctorId = doctorId;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) || string.IsNullOrWhiteSpace(DescriptionTextBox.Text) || string.IsNullOrWhiteSpace(InstructionTextBox.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            Test test = new Test()
            {
               title = TitleTextBox.Text,
               description = DescriptionTextBox.Text,
               instruction = InstructionTextBox.Text,
               doctor_id = DoctorId,
               patient_id = PatientId
           };

            try
            {
                using (var db = new test1entities())
                {
                    db.Test.Add(test); 
                    db.SaveChanges();
                }

                MessageBox.Show("Тест успешно сохранен.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить план рекомендацию: " + ex.Message);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
