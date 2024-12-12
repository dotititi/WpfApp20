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
    /// Логика взаимодействия для ShowDocumentPacientWindow.xaml
    /// </summary>
    public partial class ShowDocumentPacientWindow : Window
    {
        private int PatientId { get; set; }
        test1entities db = new test1entities();
        public ShowDocumentPacientWindow(int patientId)
        {
            InitializeComponent();
            PatientId = patientId;
            DocumentsDataGrid.ItemsSource = db.DocumentSignature.Where(t => t.patient_id == PatientId).ToList();
        }

        private void ChangeDocumentDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecord = DocumentsDataGrid.SelectedItem as DocumentSignature;

            if (selectedRecord != null)
            {
            ChangeDocumentDetailsPacientWindow changeDocumentDetailsPacientWindow = new ChangeDocumentDetailsPacientWindow(selectedRecord);
            changeDocumentDetailsPacientWindow.ShowDialog();
            DocumentsDataGrid.ItemsSource = null;
            DocumentsDataGrid.ItemsSource = db.DocumentSignature.Where(t => t.patient_id == PatientId).ToList();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите документ из списка.");
            }
        }

        private void DeleteDocument_Click(object sender, RoutedEventArgs e)
        {
            DocumentSignature documentSignature = DocumentsDataGrid.SelectedValue as DocumentSignature;
            if (documentSignature == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }
            try
            {
                db.DocumentSignature.Remove(documentSignature);
                db.SaveChanges();
                DocumentsDataGrid.ItemsSource = null;
                DocumentsDataGrid.ItemsSource = db.DocumentSignature.Where(t => t.patient_id == PatientId).ToList();
                MessageBox.Show("Документ успешно удалён");

            }
            catch
            {
                MessageBox.Show("Невозможно удалить запись");
            }
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDocument = DocumentsDataGrid.SelectedItem as DocumentSignature;
            if (selectedDocument != null)
            {
                byte[] fileContent = selectedDocument.media;

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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
