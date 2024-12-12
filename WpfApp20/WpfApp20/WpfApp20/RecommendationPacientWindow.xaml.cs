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
    /// Логика взаимодействия для RecommendationPacientWindow.xaml
    /// </summary>
    public partial class RecommendationPacientWindow : Window
    {
        private int PatientId;
        private test1entities db = new test1entities();
        public RecommendationPacientWindow(int patientId)
        {
            InitializeComponent();
            PatientId = patientId;
            LoadRecommendation();
        }
        private void LoadRecommendation()
        {
            var recommendation = db.Recommendation.Where(r => r.patient_id == PatientId).ToList();
            RecommendationDataGrid.ItemsSource = recommendation;
        }
        private void OpenRecommendationDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecommendation = RecommendationDataGrid.SelectedItem as Recommendation;
            if (selectedRecommendation != null)
            {
                RecommendationDetailsWindow recommendationDetailsWindow = new RecommendationDetailsWindow(selectedRecommendation);
                recommendationDetailsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите рекомендация.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
