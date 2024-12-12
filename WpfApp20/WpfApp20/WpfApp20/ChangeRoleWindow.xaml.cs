using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для ChangeRoleWindow.xaml
    /// </summary>
    public partial class ChangeRoleWindow : Window
    {
        test1entities db = new test1entities();
        public ChangeRoleWindow(string login, test1entities dbContext)
        {
            InitializeComponent();
            txbLogin.Text = login;
            this.db = dbContext;
            LoadRoles();
        }
        private void LoadRoles()
        {
            var roles = db.User
                          .Select(u => u.role)
                          .Distinct()
                          .ToList();

            cmbRole.ItemsSource = roles;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbRole.Text) || String.IsNullOrWhiteSpace(txbLogin.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }
            if (String.IsNullOrWhiteSpace(txbLogin.Text))
            {

            }

            string selectedRole = cmbRole.SelectedItem.ToString();

            var user = db.User.FirstOrDefault(u => u.login == txbLogin.Text);
            if (user == null)
            {
                MessageBox.Show("Пользователь не найден.");
                return;
            }

            try
            {
                user.role = selectedRole;
                db.SaveChanges();
                MessageBox.Show("Роль успешно изменена.");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Не удалось изменить роль: ");
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
