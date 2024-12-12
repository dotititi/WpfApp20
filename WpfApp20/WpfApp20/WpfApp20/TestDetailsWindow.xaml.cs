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
    /// Логика взаимодействия для TestDetailsWindow.xaml
    /// </summary>
    public partial class TestDetailsWindow : Window
    {
        private int currentTestId;
        public TestDetailsWindow(int testId)
        {
            InitializeComponent();
            currentTestId = testId;
            LoadTestDetails();
        }
        private void LoadTestDetails()
        {
            using (var db = new test1entities())
            {
                var test = db.Test.FirstOrDefault(t => t.id == currentTestId);
                if (test != null)
                {
                    TitleTextBlock.Text = test.title;
                    DescriptionTextBlock.Text = test.description;
                    InstructionTextBlock.Text = test.instruction;
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
