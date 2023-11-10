using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RTClient.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            //_connectionString = "Server=(localdb)\\mssqllocaldb;Database=RTdb;Trusted_Connection=True;Integrated Security=true;";
            _connectionString = "Server=DESKTOP-TR2CJF1\\SQLEXPRESS; Database=RTdb; Integrated Security=true;Trusted_Connection=True";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}