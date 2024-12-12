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
    /// Логика взаимодействия для ChangeTreatmentPlanWindow.xaml
    /// </summary>
    public partial class ChangeTreatmentPlanWindow : Window
    {
        private TreatmentPlan TreatmentPlan;
        public ChangeTreatmentPlanWindow(TreatmentPlan treatmentPlan)
        {
            InitializeComponent();
            TreatmentPlan = treatmentPlan;
            PlanDetailTextBox.Text = TreatmentPlan.plan_detailt;
        }
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            TreatmentPlan.plan_detailt = PlanDetailTextBox.Text;

            if (string.IsNullOrWhiteSpace(PlanDetailTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }
            try
            {
                using (var db = new test1entities())
                {
                    var recordToUpdate = db.TreatmentPlan.SingleOrDefault(r => r.id == TreatmentPlan.id);
                    if (recordToUpdate != null)
                    {
                        recordToUpdate.plan_detailt = TreatmentPlan.plan_detailt;
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
