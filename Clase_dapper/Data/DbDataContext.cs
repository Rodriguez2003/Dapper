using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace Clase_dapper.Data
{
    public class DbDataContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _databaseName;

        public DbDataContext(IConfiguration configuration)
        {
            _configuration=configuration;
            _databaseName=_configuration.GetConnectionString("conexion");
        }
        public IDbConnection crearconexion()
        {
            return new MySqlConnection(_databaseName);
        }
    }

}
