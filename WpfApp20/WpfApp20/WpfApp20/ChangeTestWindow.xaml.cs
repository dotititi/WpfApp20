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
    /// Логика взаимодействия для ChangeTestWindow.xaml
    /// </summary>
    public partial class ChangeTestWindow : Window
    {
        private Test CurrentTest;
        public ChangeTestWindow(Test test)
        {
            InitializeComponent();
            CurrentTest = test;
            TitleTextBox.Text = test.title;
            InstructionTextBox.Text = test.instruction;
            DescriptionTextBox.Text = test.description;
        }
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentTest.title = TitleTextBox.Text;
            CurrentTest.instruction = InstructionTextBox.Text;
            CurrentTest.description = DescriptionTextBox.Text;
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) || string.IsNullOrWhiteSpace(DescriptionTextBox.Text) || string.IsNullOrWhiteSpace(InstructionTextBox.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                using (var db = new test1entities())
                {
                    var recordToUpdate = db.Test.SingleOrDefault(r => r.id == CurrentTest.id);
                    if (recordToUpdate != null)
                    {
                        recordToUpdate.title = CurrentTest.title;
                        recordToUpdate.instruction= CurrentTest.instruction;
                        recordToUpdate.description = CurrentTest.description;
                        db.SaveChanges();
                        MessageBox.Show("Запись успешно обновлена.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Запись не найдена в базе данных.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось обновить запись: " + ex.Message);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
