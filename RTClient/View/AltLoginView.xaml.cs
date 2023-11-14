using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RTClient.View
{
    /// <summary>
    /// Логика взаимодействия для AltLoginView.xaml
    /// </summary>
    public partial class AltLoginView : Window
    {
        private ServerCommunication serverCommunication;
        public AltLoginView(ServerCommunication serverCommunication)
        {
            InitializeComponent();
            this.serverCommunication = serverCommunication;
            fillOrgCombobox();
        }

        async private void fillOrgCombobox()
        {
            comboOrg.Items.Clear();

            string message = $"getAllOrg";
            string response = await serverCommunication.SendMessageAndGetResponse(message);

            string[] responseArr = response.Split('+');
            foreach (string org in responseArr)
            {
                comboOrg.Items.Add(org);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        async private void comboOrg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboUser.Items.Clear();
            string message = $"getUsersFromOrg+{comboOrg.SelectedItem}";
            string response = await serverCommunication.SendMessageAndGetResponse(message);
            string[] responseArr = response.Split('+');
            foreach (string users in responseArr)
            {
                comboUser.Items.Add(users);
            }
        }
        async private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboUser.SelectedItem == null)
                {
                    throw new Exception("Проверьте логин");
                }
                var userName = comboUser.SelectedItem.ToString();
                var password = txtPass.Password;

                string message = $"login+{userName}+{password}";
                string response = await serverCommunication.SendMessageAndGetResponse(message);

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
                MessageBox.Show($"Ошибка, проверьте данные и попробуйте еще раз.\n{ex.Message} ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAltLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView(serverCommunication);
            loginView.Show();
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
