using System;
using System.Windows;
using System.Windows.Input;

namespace RTClient.View
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private ServerCommunication serverCommunication;
        public LoginView(ServerCommunication serverCommunication)
        {
            InitializeComponent();
            this.serverCommunication = serverCommunication;
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
            string password = txtPass.Password;

            string message = $"login+{userName}+{password}";
            string response = await serverCommunication.SendMessageAndGetResponse(message);

            try
            {
                string[] responseArr = response.Split('+');
                if (responseArr[1] == "true")
                {
                    MainView mainView = new MainView(serverCommunication, userName);
                    mainView.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль!", "Такого аккаунта не существует!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка, проверте данные и попробуйте еще раз\n {ex.Message} ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnAltLogin_Click(object sender, RoutedEventArgs e)
        {
            AltLoginView altLoginView = new AltLoginView(serverCommunication);
            altLoginView.Show();
            Close();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            SignUpView signUpView = new SignUpView(serverCommunication);
            signUpView.Show();
            Close();
        }
    }
}
