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
    /// Логика взаимодействия для ShowDocumentResponseWindow.xaml
    /// </summary>
    public partial class ShowDocumentResponseWindow : Window
    {
        private int PatientId { get; set; }
        test1entities db = new test1entities();
        public ShowDocumentResponseWindow(int patientId)
        {
            InitializeComponent();
            PatientId = patientId;
            var patient = db.Patient.FirstOrDefault(p => p.id == PatientId);
            if (patient != null)
            {
                FioTextBlock.Text = $"{patient.fullname} {patient.name} {patient.middlename}";
            }
            var patientResponseDocument = db.DocumentSignature.Where(d => d.patient_id == PatientId).ToList();
            GridDocument.ItemsSource = patientResponseDocument;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedDocument = GridDocument.SelectedItem as DocumentSignature;
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
                            Filter = "PDF файлы|*.pdf"
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
