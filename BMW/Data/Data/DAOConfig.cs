using Microsoft.Data.SqlClient;


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
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = MACHINE;
            builder.UserID = USER;
            builder.Password = PASSWORD;
            builder.InitialCatalog = DATABASE;
            builder.TrustServerCertificate = true;
            return builder.ConnectionString;
        }
    }
}