using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для PassTestWindow.xaml
    /// </summary>
    public partial class ResponseTestPacientWindow : Window
    {
        private Test SelectedTest { get; set; }

        private byte[] fileData;

        test1entities db = new test1entities();
        public ResponseTestPacientWindow(Test selectedTest)
        {
            InitializeComponent();
            SelectedTest = selectedTest;
            LoadTestDetails();
        }
        private void LoadTestDetails()
        {
            TitleTextBlock.Text = SelectedTest.title;
            DescriptionTextBlock.Text = SelectedTest.description;
            InstructionTextBlock.Text = SelectedTest.instruction;
        }

        private string SelectedFilePath;
        private void AttachFileButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Изображения и видео (*.jpg;*.jpeg;*.png;*.mp4;*.avi)|*.jpg;*.jpeg;*.png;*.mp4;*.avi"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedFilePath = openFileDialog.FileName;

                try
                {
                    fileData = File.ReadAllBytes(SelectedFilePath);

                    FileNameTextBlock.Text = $"Выбран файл: {System.IO.Path.GetFileName(SelectedFilePath)}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    fileData = null; 
                }
            }
        }

        private void SaveResultButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ResultTextBox.Text))
                {
                    MessageBox.Show("Пожалуйста, введите результат теста.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (fileData == null || fileData.Length == 0)
                {
                    MessageBox.Show("Пожалуйста, пожалуйста прикрепите результат теста.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var result = new PatientResult
                {
                    test_id = SelectedTest.id,
                    patient_id = SelectedTest.patient_id,
                    result_text = ResultTextBox.Text.Trim(),
                    media = fileData
                };

                db.PatientResult.Add(result);
                db.SaveChanges();

                MessageBox.Show("Результат успешно сохранён.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);


                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении результата: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
