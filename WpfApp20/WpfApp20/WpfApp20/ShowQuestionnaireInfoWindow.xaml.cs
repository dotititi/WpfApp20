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
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp20
{
    /// <summary>
    /// Логика взаимодействия для ShowQuestionnaireInfoWindow.xaml
    /// </summary>
    public partial class ShowQuestionnaireInfoWindow : Window
    {
        public ShowQuestionnaireInfoWindow(Questionnaire questionnaire)
        {
            InitializeComponent();
            TitleTextBlock.Text = questionnaire.title;
            DescriptionTextBlock.Text = questionnaire.description;

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
