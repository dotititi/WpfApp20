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
    /// Логика взаимодействия для ShowTreatmentPlanInfoWindow.xaml
    /// </summary>
    public partial class ShowTreatmentPlanInfoWindow : Window
    {
        public ShowTreatmentPlanInfoWindow(TreatmentPlan treatmentPlan)
        {
            InitializeComponent();
            PlanDetailTextBlock.Text = treatmentPlan.plan_detailt;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
