using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfApp20
{
    public partial class ShowDocumentInfoWindow : Window
    {
        private byte[] CurrentfileData;
        private string CurrentfileType;

        public ShowDocumentInfoWindow(Documents documents)
        {
            InitializeComponent();
            CurrentfileType = documents.type;
            CurrentfileData = documents.file;

            TypeTextBlock.Text = CurrentfileType;

            if (CurrentfileData != null && CurrentfileData.Length > 0)
            {
            }
            else
            {
                MessageBox.Show("Файл не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
