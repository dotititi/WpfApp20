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
using System.Xml.Linq;

namespace WpfApp20
{
    /// <summary>
    /// Логика взаимодействия для ShowDocumentResponseInfoWindow.xaml
    /// </summary>
    public partial class ShowDocumentResponseInfoWindow : Window
    {
        private byte[] CurrentfileData;
        private string CurrentfileType;
        public ShowDocumentResponseInfoWindow(DocumentSignature documentSignature)
        {
            InitializeComponent();
            CurrentfileData = documentSignature.media;

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
                Filter = "PDF Files (*.pdf)|*.pdf |All Files (*.*)|*.*"
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
