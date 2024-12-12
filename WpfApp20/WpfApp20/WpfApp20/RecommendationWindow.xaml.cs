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
    /// Логика взаимодействия для ReccomendationWindow.xaml
    /// </summary>
    public partial class RecommendationWindow : Window
    {
        private int PatientId { get; set; }
        private int DoctorId { get; set; }
        public RecommendationWindow(int patientId, int doctorId)
        {
            InitializeComponent();
            PatientId = patientId;
            DoctorId = doctorId;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RecommendationTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            Recommendation recommendation = new Recommendation
            {
                patient_id = PatientId,
                doctor_id = DoctorId,
                context = RecommendationTextBox.Text
            };

            try
            {
                using (var db = new test1entities())
                {
                    db.Recommendation.Add(recommendation);
                    db.SaveChanges();
                }

                MessageBox.Show("Рекомендация успешно сохранен.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить рекомендацию: " + ex.Message);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
