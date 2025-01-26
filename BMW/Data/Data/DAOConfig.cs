using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace BMW.Data.Data
{
    internal class DAOconfig
    {
        public const string USER = "root";
        public const string PASSWORD = "root";
        public const string MACHINE = "localhost";
        public const string DATABASE = "ASSEMBLYMNGR";
        
        public static string GetConnectionString()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = MACHINE;
            builder.UserID = USER;
            builder.Password = PASSWORD;
            builder.Database = DATABASE;
            return builder.ConnectionString;
        }
    }
}