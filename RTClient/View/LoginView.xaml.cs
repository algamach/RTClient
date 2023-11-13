using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net.Sockets;
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

namespace RTClient.View
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private ServerCommunication serverCommunication;
        public LoginView()
        {
            InitializeComponent();
            serverCommunication = new ServerCommunication();
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
            serverCommunication.CloseConnection();
            Application.Current.Shutdown();
        }

        async private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUser.Text;
            string password = txtPass.Password
                ;
            string message = $"login+{userName}+{password}";
            string response = await serverCommunication.SendMessageAndGetResponse(message);
            
            string[] responseArr = response.Split('+');
            if (responseArr[1]=="true")
            {
                MainView mainView = new MainView();
                this.Close();
                mainView.Show();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль!", "Такого аккаунта не существует!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            txtUser.Text = response;


        }
        

        private void btnAltLogin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
