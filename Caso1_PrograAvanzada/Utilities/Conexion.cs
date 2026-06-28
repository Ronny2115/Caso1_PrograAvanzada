using Microsoft.Data.SqlClient;

namespace Caso1_PrograAvanzada.Utilities
{
    public class Conexion
    {
        private readonly IConfiguration _configuration;

        public Conexion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_configuration.GetConnectionString("CadenaSQL"));
        }
    }
}
