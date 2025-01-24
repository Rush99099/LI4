using Microsoft.Data.SqlClient;


namespace BMW.Data.Data
{
    internal class DAOconfig
    {
        public const string USER = "";
        public const string PASSWORD = "";
        public const string MACHINE = "";
        public const string DATABASE = "";
        
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