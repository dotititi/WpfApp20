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
    /// Логика взаимодействия для ViewReccomendationWindow.xaml
    /// </summary>
    public partial class ViewRecommendationWindow : Window
    {
        test1entities db = new test1entities();
        private int PatientId { get; set; }
        private int DoctorId { get; set; }
        public ViewRecommendationWindow(Patient selectedPatient,int patientId, int doctorId)
        {
            InitializeComponent();
            PatientId = patientId;
            DoctorId = doctorId;
            var patientRecommendation = db.Recommendation.Where(r => r.patient_id == PatientId).ToList();
            GridRecommendation.ItemsSource = patientRecommendation;
            FioTextBlock.Text = $"{selectedPatient.fullname} {selectedPatient.name} {selectedPatient.middlename}";
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedRecord = GridRecommendation.SelectedItem as Recommendation;

            if (selectedRecord != null)
            {
                ShowRecommendationInfoWindow showRecommendationInfoWindow = new ShowRecommendationInfoWindow(selectedRecord);
                showRecommendationInfoWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            RecommendationWindow recommendationWindow = new RecommendationWindow(PatientId, DoctorId);
            recommendationWindow.ShowDialog();
            var patientRecommendation = db.Recommendation.Where(t => t.patient_id == PatientId).ToList();
            GridRecommendation.ItemsSource = null;
            GridRecommendation.ItemsSource = patientRecommendation;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var selectedRecord = GridRecommendation.SelectedItem as Recommendation;

            if (selectedRecord != null)
            {
                ChangeRecommendationWindow changeRecommendationWindow = new ChangeRecommendationWindow(selectedRecord);
                changeRecommendationWindow.ShowDialog();
                var patientRecommendation = db.Recommendation.Where(t => t.patient_id == PatientId).ToList();
                GridRecommendation.ItemsSource = null;
                GridRecommendation.ItemsSource = patientRecommendation;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Recommendation recommendation = GridRecommendation.SelectedValue as Recommendation;
            if (recommendation == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                db.Recommendation.Remove(recommendation);
                db.SaveChanges();
                GridRecommendation.ItemsSource = null;
                GridRecommendation.ItemsSource = db.Recommendation.Where(t => t.patient_id == PatientId).ToList();

            }
            catch
            {
                MessageBox.Show("Невозможно удалить запись");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
