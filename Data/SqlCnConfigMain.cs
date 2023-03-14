using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BLUEWAY.Data
{
    public class SqlCnConfigMain
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public SqlCnConfigMain(IConfiguration configuration)
        {
            _configuration = configuration;
            //_connectionString = _configuration.GetConnectionString("mysqlconection");
            _connectionString = _configuration.GetConnectionString("sqlconection");
        }

        public IDbConnection CreateConnection()
            //=> new MySqlConnection(_connectionString);
            => new SqlConnection(_connectionString);
    }
}
