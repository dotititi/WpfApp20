using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace WpfApp20
{
    public partial class ChangeDocumentWindow : Window
    {
        private byte[] fileData;
        private int DocumentId { get; set; }
        private int PatientId { get; set; }
        private int DoctorId { get; set; }

        public ChangeDocumentWindow(int documentId, int patientId, int doctorId)
        {
            InitializeComponent();

            DocumentId = documentId;
            PatientId = patientId;
            DoctorId = doctorId;

            LoadDocumentData();
        }

        private void LoadDocumentData()
        {
            using (var db = new test1entities())
            {
                var document = db.Documents.SingleOrDefault(d => d.id == DocumentId);
                if (document != null)
                {
                    NameTextBox.Text = document.type;  
                    fileData = document.file;  
                }
            }
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
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
                    var document = db.Documents.SingleOrDefault(d => d.id == DocumentId);

                    if (document != null)
                    {
                        document.type = NameTextBox.Text;  
                        document.file = fileData;  

                        db.SaveChanges();
                    }
                }

                MessageBox.Show("Документ успешно обновлен.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при изменении файла: {ex.Message}");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
