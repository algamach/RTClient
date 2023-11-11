using Microsoft.Data.SqlClient;
using RTClient.Model;
using RTClient.View;
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
    /// Логика взаимодействия для AltLoginView.xaml
    /// </summary>
    public partial class AltLoginView : Window
    {
        Database database = new Database();
        public AltLoginView()
        {
            InitializeComponent();
            var queryListCodeRequest = "SELECT [Name] FROM Organizations";
            string column = "Name";
            database.loadElementToComboBox(queryListCodeRequest, comboOrg, column);
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
        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            SignUpView signUpView = new SignUpView();
            this.Close();
            signUpView.Show();
        }
        
        private void btnAltLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            this.Close();
            loginView.Show();
        }
       
        


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var userName = comboUser.SelectedItem.ToString();
            var password = txtPass.Password;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"SELECT UserName, [Password] FROM Users WHERE UserName = '{userName}' and [Password] = '{password}'";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                //MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                MainView mainView = new MainView(userName);
                this.Close();
                mainView.Show();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль!", "Такого аккаунта не существует!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        //public void loadElementToComboBox(string stringQuery, ComboBox myBox, string column)
        //{
        //    List<string> columnValues = GetColumnValues(stringQuery, column);
        //    foreach (string value in columnValues)
        //    {
        //        myBox.Items.Add(value);
        //    }
        //}
        //public List<string> GetColumnValues(string query, string column)
        //{
        //    List<string> columnValues = new List<string>();

        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    DataTable table = new DataTable();

        //    SqlCommand command = new SqlCommand(query, database.getConnection());

        //    adapter.SelectCommand = command;
        //    adapter.Fill(table);

        //    foreach (DataRow row in table.Rows)
        //    {
        //        // получаем все ячейки строки
        //        columnValues.Add((string)row[column]);
        //    }
        //        return columnValues;
        //}

        private void comboOrg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var queryListCodeRequest = $"SELECT Username FROM Users JOIN Organizations ON Users.OrgID=Organizations.Id WHERE Organizations.[Name] = '{comboOrg.SelectedItem.ToString()}'";
            string column = "Username";
            comboUser.Items.Clear();
            database.loadElementToComboBox(queryListCodeRequest, comboUser, column);
        }
    }
}
