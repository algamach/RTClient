using RTClient.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RTClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServerCommunication serverCommunication;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            serverCommunication = new ServerCommunication();

            LoginView loginView = new LoginView(serverCommunication);
            loginView.Show();
        }
        private void OnExit(object sender, EventArgs e)
        {
            serverCommunication.CloseConnection();
        }
    }
}
