using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace BMW.Data.Data
{
    internal class DAOconfig
    {
        public const string USER = "root";
        public const string PASSWORD = "a100746";
        public const string MACHINE = "localhost";
        public const string DATABASE = "ASSEMBLYMNGR";
        
        public static string GetConnectionString()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = MACHINE;
            builder.UserID = USER;
            builder.Password = PASSWORD;
            builder.Database = DATABASE;
            builder.Port = 3306;
            return builder.ConnectionString;
        }
    }
}