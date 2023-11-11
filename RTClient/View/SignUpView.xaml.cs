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
    /// Логика взаимодействия для SignUpView.xaml
    /// </summary>
    public partial class SignUpView : Window
    {
        Database database = new Database();
        public SignUpView()
        {
            InitializeComponent();
            var queryListCodeRequest = "SELECT [Name] FROM Organizations";
            string column = "Name";
            database.loadElementToComboBox(queryListCodeRequest, comboOrg, column);
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            this.Close();
            loginView.Show();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            var orgName = comboOrg.Text;
            var lastName = txtLastName.Text;
            var firstName = txtFirstName.Text;
            var fatherName = txtFatherName.Text;
            var userName = txtUserName.Text;
            var password = txtPass.Password;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"SELECT Id FROM Organizations WHERE [Name] = '{orgName}'";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            try
            {
                var orgId = table.Rows[0]["Id"];
                if ((userName == "")||(lastName == "")||(firstName == "") || (fatherName == "") || (userName == "") || (password == ""))
                {
                    throw new Exception();
                }
                if (isUserExist(userName))
                {
                    MessageBox.Show("Такой пользователь уже существует. Придумайте другой логин.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new Exception();
                }
                queryString = $"INSERT INTO Users (LastName, FirstName, FatherName, UserName, [Password], OrgID) VALUES ('{lastName}', '{firstName}', '{fatherName}', '{userName}', '{password}', {orgId});";

                SqlCommand insert_command = new SqlCommand(queryString, database.getConnection());

                database.openConnection();
                if (insert_command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Аккаунт успешно создан!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoginView loginView = new LoginView();
                    this.Close();
                    loginView.Show();
                }
                else
                {
                    MessageBox.Show("Аккаунт не создан!", "Ошибка!",MessageBoxButton.OK, MessageBoxImage.Error);
                }
                database.closeConnection();

            }
            catch
            {
                MessageBox.Show("Произошла ошибка! Проверте введенные данные и попробуйте еще раз", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }

        private Boolean isUserExist(string userName)
        {

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"SELECT UserName FROM Users WHERE UserName = '{userName}'";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
                return true;
            else
                return false;
        }

    }
}
