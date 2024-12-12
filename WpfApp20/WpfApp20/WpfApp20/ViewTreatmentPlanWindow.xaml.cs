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
    /// Логика взаимодействия для ViewTreatmentPlanWindow.xaml
    /// </summary>
    public partial class ViewTreatmentPlanWindow : Window
    {
        test1entities db = new test1entities();
        private int PatientId { get; set; }
        private int DoctorId { get; set; }
        public ViewTreatmentPlanWindow(Patient selectedPatient,int patientId, int doctorId)
        {
            InitializeComponent();
            PatientId = patientId;
            DoctorId = doctorId;
            var patientTreatmentPlan = db.TreatmentPlan.Where(t => t.patient_id == PatientId).ToList();
            GridTreatmentPlan.ItemsSource = patientTreatmentPlan;
            FioTextBlock.Text = $"{selectedPatient.fullname} {selectedPatient.name} {selectedPatient.middlename}";
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedRecord = GridTreatmentPlan.SelectedItem as TreatmentPlan;

            if (selectedRecord != null)
            {
                ShowTreatmentPlanInfoWindow showTreatmentPlanInfo = new ShowTreatmentPlanInfoWindow(selectedRecord);
                showTreatmentPlanInfo.ShowDialog();

            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TreatmentPlanWindow treatmentPlanWindow = new TreatmentPlanWindow(PatientId, DoctorId);
            treatmentPlanWindow.ShowDialog();
            var patientTreatmentPlan = db.TreatmentPlan.Where(t => t.patient_id == PatientId).ToList();
            GridTreatmentPlan.ItemsSource = null;
            GridTreatmentPlan.ItemsSource = patientTreatmentPlan;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var selectedRecord = GridTreatmentPlan.SelectedItem as TreatmentPlan;

            if (selectedRecord != null)
            {
                ChangeTreatmentPlanWindow treatmentPlanWindow = new ChangeTreatmentPlanWindow(selectedRecord);
                treatmentPlanWindow.ShowDialog();
                var patientTreatmentPlan = db.TreatmentPlan.Where(t => t.patient_id == PatientId).ToList();
                GridTreatmentPlan.ItemsSource = null;
                GridTreatmentPlan.ItemsSource = patientTreatmentPlan;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            TreatmentPlan treatmentPlan = GridTreatmentPlan.SelectedValue as TreatmentPlan;
            if (treatmentPlan == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                db.TreatmentPlan.Remove(treatmentPlan);
                db.SaveChanges();
                var patientTreatmentPlan = db.TreatmentPlan.Where(t => t.patient_id == PatientId).ToList();
                GridTreatmentPlan.ItemsSource = null;
                GridTreatmentPlan.ItemsSource = patientTreatmentPlan;

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
