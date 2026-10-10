using Lab3.Models;
using System.Linq;
using System.Windows;

namespace Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AuthClick(object sender, RoutedEventArgs e)
        {
            // обращение к базе: поиск пользователя
            // если найден — открываем окно списка и передаем пользователя
            // иначе — ошибка
            var login = loginInput.Text;
            var password = passwordInput.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                this.Information("Заполни все поля!");
                return;
            }

            using var context = new Lab3Context();
            var user = context.Users.FirstOrDefault(u =>
                u.Login == login && u.Password == password);

            if (user != null)
            {
                GoToList(user);
            }
            else
            {
                this.Error("Неверный логин или пароль!");
            }
        }

        private void GuestEnterClick(object sender, RoutedEventArgs e)
        {
            // просто открываем список как гость
            GoToList(null);
        }

        private void GoToList(User? user)
        {
            var win = new ProductListWindow(user);
            win.Show();
            this.Close();
        }
    }
}