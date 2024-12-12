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
    /// Логика взаимодействия для TestPacientWindow.xaml
    /// </summary>
    public partial class TestPacientWindow : Window
    {
        private int PatientId;
        public TestPacientWindow(int patientId)
        {
            InitializeComponent();
            PatientId = patientId;
            LoadTests();
        }
        private void LoadTests()
        {
            using (var db = new test1entities())
            {
                var tests = db.Test.Where(t => t.patient_id == PatientId).ToList();
                TestsDataGrid.ItemsSource = tests;
            }
        }
        private void OpenTestDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedTest = TestsDataGrid.SelectedItem as Test;
            if (selectedTest == null)
            {
                MessageBox.Show("Пожалуйста, выберите тест.");
                return;
            }

            TestDetailsWindow testDetailsWindow = new TestDetailsWindow(selectedTest.id);
            testDetailsWindow.ShowDialog();
        }
        private void PassTest_Click(object sender, RoutedEventArgs e)
        {
            var selectedTest = TestsDataGrid.SelectedItem as Test;
            if (selectedTest == null)
            {
                MessageBox.Show("Пожалуйста, выберите тест.");
                return;
            }

            ResponseTestPacientWindow responseTestPacientWindow = new ResponseTestPacientWindow(selectedTest);
            responseTestPacientWindow.ShowDialog();
        }

        private void ResultTest_Click(object sender, RoutedEventArgs e)
        {
            ViewPatientTestWindow viewPatientTestWindow = new ViewPatientTestWindow(PatientId);
            viewPatientTestWindow.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
