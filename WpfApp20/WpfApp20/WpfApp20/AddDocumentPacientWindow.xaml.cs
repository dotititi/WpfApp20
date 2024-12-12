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
    /// Логика взаимодействия для AddDocumentPacientWindow.xaml
    /// </summary>
    public partial class AddDocumentPacientWindow : Window
    {
        private byte[] CurrentfileData;
        private string CurrentfileType;
        private int CurrentPatientId;
        private int CurrentDocumentId;
        private byte[] NewFileData;

        public AddDocumentPacientWindow(Documents documents)
        {
            InitializeComponent();
            CurrentfileType = documents.type;
            CurrentfileData = documents.file;
            CurrentDocumentId = documents.id;
            CurrentPatientId = documents.patient_id;

            TypeTextBlock.Text = CurrentfileType;
        }
        private void AddPdfButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                Title = "Выберите PDF файл"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    NewFileData = File.ReadAllBytes(openFileDialog.FileName);
                    CurrentfileType = "PDF";

                    string filePath = openFileDialog.FileName;
                    FilePathTextBlock.Text = filePath;

                    MessageBox.Show("PDF файл успешно загружен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "document",
                Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    File.WriteAllBytes(saveFileDialog.FileName, CurrentfileData);
                    MessageBox.Show("Файл успешно сохранён.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewFileData == null)
            {
                MessageBox.Show("Пожалуйста, выберите PDF файл.");
                return;
            }

            try
            {
                using (var db = new test1entities())
                {
                    var documentSignature = new DocumentSignature
                    {
                        
                        media = NewFileData,
                        document_id = CurrentDocumentId,
                        patient_id = CurrentPatientId
                    };

                    db.DocumentSignature.Add(documentSignature);
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
