using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using RTClient.Model;
using RTClient.View;

namespace RTClient.View
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        Database database = new Database();
        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUser.Text;
            string password = txtPass.Password;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"SELECT UserName, [Password] FROM Users WHERE UserName = '{userName}' and [Password] = '{password}'";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            adapter.SelectCommand= command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль!", "Такого аккаунта не существует!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAltLogin_Click(object sender, RoutedEventArgs e)
        {
            AltLoginView altLoginView = new AltLoginView();
            this.Close();
            altLoginView.Show();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            SignUpView signUpView = new SignUpView();
            this.Close();
            signUpView.Show();
        }
    }
}
