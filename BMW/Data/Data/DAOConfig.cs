using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace BMW.Data.Data
{
    internal class DAOconfig
    {
        public const string DATABASE = "ASSEMBLYMNGR";
        
        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost,1433";
            builder.InitialCatalog = DATABASE;
            builder.TrustServerCertificate = true;
            builder.IntegratedSecurity = true;
            return builder.ConnectionString;
        }
    }
}