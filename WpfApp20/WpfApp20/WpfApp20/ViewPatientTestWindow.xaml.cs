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
    /// Логика взаимодействия для ViewPatientTestWindow.xaml
    /// </summary>
    public partial class ViewPatientTestWindow : Window
    {
        private int PatientId { get; set; }
        test1entities db = new test1entities();
        public ViewPatientTestWindow(int patientId)
        {
            InitializeComponent();
            PatientId = patientId;
            var patientResultTests = db.PatientResult.Where(p => p.patient_id == PatientId).Select(pr => new { pr.id, pr.result_text }).ToList();
            TestsDataGrid.ItemsSource = patientResultTests;
        }

        private void OpenTestDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedResult = TestsDataGrid.SelectedItem;

            if (selectedResult != null)
            {
                var selectedId = (int)selectedResult.GetType().GetProperty("id").GetValue(selectedResult);

                // Перезагружаем данные из базы
                var fullResult = db.PatientResult.FirstOrDefault(pr => pr.id == selectedId);
                if (fullResult != null)
                {
                    db.Entry(fullResult).Reload(); // Гарантируем обновление данных

                    var selectedTest = db.Test.FirstOrDefault(t => t.id == fullResult.test_id);

                    if (selectedTest != null)
                    {
                        ShowTestPacientResultWindow showTestPacientResultWindow = new ShowTestPacientResultWindow(selectedTest, fullResult);
                        showTestPacientResultWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось найти связанный тест.");
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось найти результат.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись из списка.");
            }
        }


        private void ChangeTestDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedResult = TestsDataGrid.SelectedItem;

            if (selectedResult != null)
            {
                var selectedId = (int)selectedResult.GetType().GetProperty("id").GetValue(selectedResult);

                // Перезагружаем данные из базы
                var fullResult = db.PatientResult.FirstOrDefault(pr => pr.id == selectedId);
                if (fullResult != null)
                {
                    db.Entry(fullResult).Reload(); // Гарантируем, что данные актуальны

                    var selectedTest = db.Test.FirstOrDefault(t => t.id == fullResult.test_id);

                    if (selectedTest != null)
                    {
                        ChangeTestPacientResultWindow changeTestPacientResult = new ChangeTestPacientResultWindow(selectedTest, fullResult);
                        changeTestPacientResult.ShowDialog();

                        // Обновляем DataGrid после изменений
                        TestsDataGrid.ItemsSource = null;
                        var patientResultTests = db.PatientResult
                            .Where(p => p.patient_id == PatientId)
                            .Select(pr => new { pr.id, pr.result_text })
                            .ToList();
                        TestsDataGrid.ItemsSource = patientResultTests;
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


        private void DeleteTest_Click(object sender, RoutedEventArgs e)
        {
            var selectedResult = TestsDataGrid.SelectedItem;
            if (selectedResult != null)
            {
                var selectedId = (int)selectedResult.GetType().GetProperty("id").GetValue(selectedResult);

                var patientResult = db.PatientResult.FirstOrDefault(pr => pr.id == selectedId);

                if (patientResult != null)
                {
                    try
                    {
                        db.PatientResult.Remove(patientResult);
                        db.SaveChanges();
                        TestsDataGrid = null;
                        TestsDataGrid.ItemsSource = db.PatientResult.Where(p => p.patient_id == PatientId).Select(pr => new { pr.id, pr.result_text }).ToList();
                        MessageBox.Show("Запись удалена успешно");
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно удалить запись");
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось найти запись для удаления.");
                }
            }
            else
            {
                MessageBox.Show("Выберите запись.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
