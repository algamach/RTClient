using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace RTClient.Model
{
    public class Database
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-TR2CJF1\\SQLEXPRESS;Initial Catalog=RTdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public SqlConnection getConnection()
        {
            return connection;
        }

        public void loadElementToComboBox(string stringQuery, ComboBox myBox, string column)
        {
            List<string> columnValues = GetColumnValues(stringQuery, column);
            foreach (string value in columnValues)
            {
                myBox.Items.Add(value);
            }
        }
        public List<string> GetColumnValues(string query, string column)
        {
            List<string> columnValues = new List<string>();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            SqlCommand command = new SqlCommand(query, getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                // получаем все ячейки строки
                columnValues.Add((string)row[column]);
            }
            return columnValues;
        }

    }
}
