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
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private ServerCommunication serverCommunication;
        public MainView(ServerCommunication serverCommunication)
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

        }
    }
}
