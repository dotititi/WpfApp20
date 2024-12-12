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
    /// Логика взаимодействия для ViewTestPacientResultWindow.xaml
    /// </summary>
    public partial class ViewTestPacientResultWindow : Window
    {
        test1entities db = new test1entities();
        private int PatientId { get; set; }
        public ViewTestPacientResultWindow(int PatientId)
        {
            InitializeComponent();
            var patient = db.Patient.FirstOrDefault(p => p.id == PatientId);
            if (patient != null)
            {
                FioTextBlock.Text = $"{patient.fullname} {patient.name} {patient.middlename}";
            }
            var patientResultTests = db.PatientResult.Where(p => p.patient_id == PatientId).Select(pr => new{pr.id, pr.result_text}).ToList();
            GridTestResult.ItemsSource = patientResultTests;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedResult = GridTestResult.SelectedItem;

            if (selectedResult != null)
            {
                var selectedId = (int)selectedResult.GetType().GetProperty("id").GetValue(selectedResult);

                var fullResult = db.PatientResult.FirstOrDefault(pr => pr.id == selectedId);

                if (fullResult != null)
                {
                    Test selectedTest = GetTestByResult(fullResult);

                    if (selectedTest != null)
                    {
                        ShowTestContextWindow showTestContextWindow = new ShowTestContextWindow(selectedTest,fullResult);
                        showTestContextWindow.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("Не удалось найти связанный тест.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
            }
        }
        private Test GetTestByResult(PatientResult selectedResult)
        {
            using (var db = new test1entities())
            {
                return db.Test.FirstOrDefault(t => t.id == selectedResult.test_id);
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
