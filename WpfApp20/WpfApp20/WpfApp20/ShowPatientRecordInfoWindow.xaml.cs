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
    /// Логика взаимодействия для ShowPatientRecordInfoWindow.xaml
    /// </summary>
    public partial class ShowPatientRecordInfoWindow : Window
    {
        public ShowPatientRecordInfoWindow(PatientRecord record)
        {
            InitializeComponent();
            ReasonTextBlock.Text = record.reason;
            DiagnosisTextBlock.Text = record.diagnosis;
            DateOfTextBlock.Text = record.date_of_visit.ToString("d");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
