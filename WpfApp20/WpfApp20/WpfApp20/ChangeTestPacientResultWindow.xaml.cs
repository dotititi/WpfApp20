using Microsoft.Win32;
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
    /// Логика взаимодействия для ChangeTestPacientResultWindow.xaml
    /// </summary>
    public partial class ChangeTestPacientResultWindow : Window
    {
        private Test SelectedTest { get; set; }
        private PatientResult SelectedResult { get; set; }
        private string filePath; 
        private byte[] fileData; 

        test1entities db = new test1entities();
        public ChangeTestPacientResultWindow(Test selectedTest, PatientResult selectedResult)
        {
            InitializeComponent();
            SelectedTest = selectedTest;
            SelectedResult = selectedResult;
            LoadTestAndResultDetails();
        }
        private void LoadTestAndResultDetails()
        {
            TitleTextBlock.Text = SelectedTest.title;
            DescriptionTextBlock.Text = SelectedTest.description;
            InstructionTextBlock.Text = SelectedTest.instruction;

            if (SelectedResult != null)
            {
                ResultTextBox.Text = SelectedResult.result_text;
            }
            else
            {
                ResultTextBox.Text = "Результат не найден.";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SaveResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ResultTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
            }

            if (SelectedResult != null)
            {
                try
                {
                    using (var db = new test1entities())
                    {
                        var testResponse = db.PatientResult.SingleOrDefault(d => d.test_id == SelectedResult.test_id);
                        {
                            if (testResponse != null)
                            {
                                testResponse.result_text = ResultTextBox.Text;
                                if (fileData != null)
                                {
                                    testResponse.media = fileData;
                                }
                                db.SaveChanges();
                                MessageBox.Show("Результат успешно сохранен.");
                                this.Close();
                            }
                        };
                        this.Close();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
                }
            }
        }

        private void AddFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Все файлы (*.*)|*.*|Изображения (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|Видео (*.mp4;*.avi)|*.mp4;*.avi";

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                filePath = openFileDialog.FileName; 
                FilePathTextBlock.Text = filePath;

                try
                {
                    if (string.IsNullOrEmpty(filePath))
                    {
                        MessageBox.Show("Пожалуйста, выберите файл.");
                        return;
                    }
                    fileData = System.IO.File.ReadAllBytes(filePath); 
                    MessageBox.Show("Файл успешно загружен.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}");
                }
            }
        }

    }
}
