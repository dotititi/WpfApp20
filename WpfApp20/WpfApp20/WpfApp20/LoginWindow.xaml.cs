using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WpfApp20
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Register_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow(); 
            registerWindow.Show();
            this.Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string login = txbLogin.Text;
            string password = txbPassword.Password;

            if (string.IsNullOrWhiteSpace(login) && string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            using (var context = new test1entities()) 
            {
                var user = context.User.FirstOrDefault(u => u.login == login && u.password == password);

                if (user != null)
                {
                    int userId = user.id;
                    switch (user.role)
                    {
                        case "user":
                            MessageBox.Show("Ваш доступ ограничен. Обратитесь к администратору для изменения роли.","Ограниченный доступ", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        case "doctor":
                            DoctorWindow doctorWindow = new DoctorWindow(userId);
                            doctorWindow.Show();
                            break;
                        case "patient":
                            PatientWindow patientWindow = new PatientWindow(userId);
                            patientWindow.Show();
                            break;
                        case "admin":
                            AdminWindow adminWindow = new AdminWindow();
                            adminWindow.Show();
                            break;
                        default:
                            MessageBox.Show("Роль пользователя неизвестна.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}