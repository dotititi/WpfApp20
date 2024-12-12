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
    /// Логика взаимодействия для ChangeDocumentDetailsPacientWindow.xaml
    /// </summary>
    public partial class ChangeDocumentDetailsPacientWindow : Window
    {
        private DocumentSignature CurrentDocument { get; set; }
        private int CurrentPatientId;
        private int CurrentDocumentId;
        public ChangeDocumentDetailsPacientWindow(DocumentSignature document)
        {
            InitializeComponent();
            CurrentDocument = document;
            CurrentPatientId = document.patient_id;
            CurrentDocumentId = document.document_id;
        }
        
        private string SelectedFilePath;
        private void AddPdfButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF файлы (*.pdf)|*.pdf";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    byte[] fileData = File.ReadAllBytes(openFileDialog.FileName);
                    FileNameTextBlock.Text = $"Выбран файл: {System.IO.Path.GetFileName(openFileDialog.FileName)}";
                    CurrentDocument.media = fileData;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}");
                }
            }
        }
        private void SaveDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentDocument != null && CurrentDocument.media != null)
            {
                try
                {
                    using (var db = new test1entities())
                    {
                        var documentSignature = db.DocumentSignature.SingleOrDefault(d => d.document_id == CurrentDocumentId);
                        {
                            if(documentSignature != null)
                            {
                                documentSignature.media = CurrentDocument.media;
                                documentSignature.document_id = CurrentDocumentId;
                                documentSignature.patient_id = CurrentPatientId;
                                db.SaveChanges();
                            }
                        };

                        MessageBox.Show("Документ добавлен успешно");
                        this.Close();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении документа: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, добавьте PDF файл.");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
