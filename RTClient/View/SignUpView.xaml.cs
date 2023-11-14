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

namespace RTClient.View
{
    /// <summary>
    /// Логика взаимодействия для SignUpView.xaml
    /// </summary>
    public partial class SignUpView : Window
    {
        private ServerCommunication serverCommunication;
        public SignUpView(ServerCommunication serverCommunication)
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
            foreach (string orgNames in responseArr)
            {
                comboOrg.Items.Add(orgNames);
            }
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        async private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            var orgName = comboOrg.SelectedItem;
            var lastName = txtLastName.Text;
            var firstName = txtFirstName.Text;
            var fatherName = txtFatherName.Text;
            var userName = txtUserName.Text;
            var password = txtPass.Password;
            
            string message = $"signUp+{orgName}+{lastName}+{firstName}+{fatherName}+{userName}+{password}";
            string response = await serverCommunication.SendMessageAndGetResponse(message);
            string[] responseArr = response.Split('+');

            if (responseArr[0] == "success")
            {
                MessageBox.Show("Аккаунт успешно создан!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                LoginView loginView = new LoginView(serverCommunication);
                loginView.Show();
                Close();
            }
            else if (responseArr[0] == "usernameError")
            {
                MessageBox.Show("Такой пользователь уже существует. Придумайте другой логин.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (responseArr[0] == "emptyError")
            {
                MessageBox.Show("Произошла ошибка! Проверте введенные данные и попробуйте еще раз", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Аккаунт не создан!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView(serverCommunication);
            loginView.Show();
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
