using System.Windows;
using System.Windows.Input;

namespace RTClient.View
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private ServerCommunication serverCommunication;
        public MainView(ServerCommunication serverCommunication, string userName)
        {
            InitializeComponent();
            this.serverCommunication = serverCommunication;
            fillUserData(userName);
        }

        async private void fillUserData(string userName)
        {
            string message = $"getUserData+{userName}";
            string response = await serverCommunication.SendMessageAndGetResponse(message);
            string[] responseArr = response.Split('+');
            loginLabel.Text = responseArr[0];
            namesLabel.Text = $"{responseArr[1]} {responseArr[2]} {responseArr[3]}";
            orgLabel.Text = responseArr[4];
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

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView(serverCommunication);
            loginView.Show();
            Close();
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            searchResult.Clear();

            string message = $"bookSearch+{txtSearch.Text}";
            string response = await serverCommunication.SendMessageAndGetResponse(message);
            if (response == "false")
                searchResult.Text = "Ничего не найдено";
            else
                searchResult.Text = response;
        }
    }
}
