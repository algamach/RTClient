using Microsoft.Data.SqlClient;
using RTClient.Model;
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

namespace RTClient.View
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        Database database = new Database();
        public MainView(string userName)
        {
            InitializeComponent();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"SELECT * FROM Users WHERE Username = '{userName}'";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            loginLabel.Text = table.Rows[0]["UserName"].ToString();
            namesLabel.Text = $"{table.Rows[0]["LastName"].ToString()} {table.Rows[0]["FirstName"].ToString()} {table.Rows[0]["FatherName"].ToString()}";

            //SqlDataAdapter adapter2 = new SqlDataAdapter();
            //DataTable table2 = new DataTable();
            table.Clear();
            queryString = $"SELECT Organizations.Name, UserName FROM Organizations JOIN Users ON Organizations.Id = Users.OrgId WHERE UserName = '{userName}'";
            SqlCommand command2 = new SqlCommand(queryString, database.getConnection());
            adapter.SelectCommand = command2;
            adapter.Fill(table);
            orgLabel.Text = table.Rows[0]["Name"].ToString();
            Console.Write("sdf");
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            this.Close();
            loginView.Show();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            searchResult.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"SELECT * FROM Books";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            for (int i =0;i<table.Rows.Count;i++)
            {
                searchResult.Text += "* ";
                searchResult.Text += table.Rows[i]["Name"];
                searchResult.Text += "\n";

            }
        }
    }
}
