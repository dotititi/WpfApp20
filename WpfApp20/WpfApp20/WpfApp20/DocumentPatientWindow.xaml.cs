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
    /// Логика взаимодействия для DocumentPatientWindow.xaml
    /// </summary>
    public partial class DocumentPatientWindow : Window
    {
        private int PatientId { get; set; }

        public DocumentPatientWindow(int patientId)
        {
            InitializeComponent();
            PatientId = patientId;
            LoadDocuments();
        }

        private void LoadDocuments()
        {
            using (var db = new test1entities())
            {
                var documents = db.Documents.Where(d => d.patient_id == PatientId).ToList();

                DocumentsDataGrid.ItemsSource = documents;
            }
        }
        private void AddDocument_Click(object sender, RoutedEventArgs e)
        {
            var selectedDocument = DocumentsDataGrid.SelectedItem as Documents;
            if (selectedDocument != null)
            {
                AddDocumentPacientWindow addDocumentPacientWindow = new AddDocumentPacientWindow(selectedDocument);
                addDocumentPacientWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите документ для добавления.");
            }
        }

        private void ViewDocument_Click(object sender, RoutedEventArgs e)
        {
            ShowDocumentPacientWindow showDocumentPacientWindow = new ShowDocumentPacientWindow(PatientId);
            showDocumentPacientWindow.ShowDialog();

        }

        private void DownloadsDocument_Click(object sender, RoutedEventArgs e)
        {
            var selectedDocument = DocumentsDataGrid.SelectedItem as Documents;
            if (selectedDocument != null)
            {
                byte[] fileContent = selectedDocument.file;

                if (fileContent != null && fileContent.Length > 0)
                {
                    try
                    {
                        string fileName = $"Document_{Guid.NewGuid()}.pdf"; 

                        Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
                        {
                            FileName = fileName,
                            Filter = "PDF файлы|*.pdf",
                        };

                        if (saveFileDialog.ShowDialog() == true)
                        {
                            System.IO.File.WriteAllBytes(saveFileDialog.FileName, fileContent);
                            MessageBox.Show("Документ успешно загружен.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке документа: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Документ не найден.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите документ для скачивания.");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

