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
    /// Логика взаимодействия для ChangeReccomendationWindow.xaml
    /// </summary>
    public partial class ChangeRecommendationWindow : Window
    {
        private Recommendation CurrentRecommendation;
        public ChangeRecommendationWindow(Recommendation recommendation)
        {
            InitializeComponent();
            CurrentRecommendation = recommendation;
            RecommendationTextBox.Text = recommendation.context;
        }
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentRecommendation.context = RecommendationTextBox.Text;

            if (string.IsNullOrWhiteSpace(RecommendationTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите рекомендацию.");
                return;
            }
            try
            {
                using (var db = new test1entities())
                {
                    var recordToUpdate = db.Recommendation.SingleOrDefault(r => r.id == CurrentRecommendation.id);
                    if (recordToUpdate != null)
                    {
                        recordToUpdate.context = CurrentRecommendation.context;
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
