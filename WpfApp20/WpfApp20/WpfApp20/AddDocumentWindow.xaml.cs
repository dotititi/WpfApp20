using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace WpfApp20
{
    public partial class AddDocumentWindow : Window
    {
        private byte[] fileData; 
        private int PatientId { get; set; }
        private int DoctorId { get; set; }

        public AddDocumentWindow(int patientId, int doctorId)
        {
            InitializeComponent();
            PatientId = patientId;
            DoctorId = doctorId;
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "PDF файлы|*.pdf"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                if (Path.GetExtension(filePath).ToLower() != ".pdf")
                {
                    MessageBox.Show("Пожалуйста, выберите файл формата .pdf.");
                    return;
                }

                SelectedFileTextBlock.Text = filePath;
                fileData = File.ReadAllBytes(filePath);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, укажите название документа.");
                return;
            }

            if (fileData == null)
            {
                MessageBox.Show("Пожалуйста, выберите PDF файл.");
                return;
            }

            try
            {
                using (var db = new test1entities())
                {
                    var document = new Documents
                    {
                        type = NameTextBox.Text,
                        file = fileData,
                        doctor_id = DoctorId,
                        patient_id = PatientId
                    };

                    db.Documents.Add(document);
                    db.SaveChanges();
                }

                MessageBox.Show("Файл успешно добавлен.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении файла: {ex.Message}");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
