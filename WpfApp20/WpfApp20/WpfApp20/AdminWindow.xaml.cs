using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        test1entities db = new test1entities();
        public AdminWindow()
        {
            InitializeComponent();
            GridUser.ItemsSource = db.User.ToList();
        }



        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (GridUser.SelectedItem is User selectedUser)
            {
                string login = selectedUser.login;
                ChangeRoleWindow changeRoleWindow = new ChangeRoleWindow(login,db);
                changeRoleWindow.ShowDialog(); ;
                GridUser.ItemsSource = db.User.ToList();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            User user = GridUser.SelectedValue as User;
            if (user == null)
            {
                MessageBox.Show("Укажите команду");
                return;
            }
            try
            {
                db.User.Remove(user);
                db.SaveChanges();
                GridUser.ItemsSource = null;
                GridUser.ItemsSource = db.User.ToList();

            }
            catch
            {
                MessageBox.Show("Невозможно удалить команду");
            }
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
