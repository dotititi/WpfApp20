using Microsoft.Win32;
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
    /// Логика взаимодействия для ShowTestContextWindow.xaml
    /// </summary>
    public partial class ShowTestContextWindow : Window
    {
        private Test SelectedTest { get; set; }
        private PatientResult SelectedResult { get; set; }
        public ShowTestContextWindow(Test selectedTest, PatientResult selectedResult)
        {
            InitializeComponent();
            SelectedTest = selectedTest;
            SelectedResult = selectedResult;
            LoadTestAndResultDetails();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void LoadTestAndResultDetails()
        {
            TitleTextBlock.Text = SelectedTest.title;
            DescriptionTextBlock.Text = SelectedTest.description;
            InstructionTextBlock.Text = SelectedTest.instruction;

            if (SelectedResult != null)
            {
                ResultTextBlock.Text = SelectedResult.result_text;

            }
            else
            {
                ResultTextBlock.Text = "Результат не найден.";
            }
        }

        private void DownloadResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedResult != null && SelectedResult.media != null && SelectedResult.media.Length > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = "result_file";
                saveFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|Video Files (*.mp4;*.avi)|*.mp4;*.avi|All files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        File.WriteAllBytes(saveFileDialog.FileName, SelectedResult.media);
                        MessageBox.Show("Файл успешно скачан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при скачивании файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Результат не содержит данных для скачивания.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
