using Microsoft.Data.SqlClient;
using BMW.Data.Models;

namespace BMW.Data.Data
{
    public class UtilizadorDAO
    {
        private static UtilizadorDAO? singleton = null;

        private UtilizadorDAO() { }

        public static UtilizadorDAO GetInstance()
        {
            if (singleton == null)
            {
                singleton = new UtilizadorDAO();
            }
            return singleton;
        }

        public bool ContainsKey(int key)
        {
            bool result = false;
            string query = "SELECT * FROM Utilizador WHERE IdUtilizador = @Key";
            try
            {
                using (SqlConnection con = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Key", key);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                result = true;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new DAOException("Erro no ContainsKey do UtilizadorDAO");
            }
            return result;
        }

        public Utilizador Get(int key)
        {
            Utilizador? utilizador = null;
            string query = "SELECT * FROM Utilizador WHERE IdUtilizador = @Key";
            try
            {
                using (SqlConnection con = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Key", key);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("IdUtilizador"));
                                string email = reader.GetString(reader.GetOrdinal("Email"));
                                string nome = reader.GetString(reader.GetOrdinal("Nome"));
                                string password = reader.GetString(reader.GetOrdinal("Password"));
                                bool isClient = reader.GetBoolean(reader.GetOrdinal("IsClient"));

                                utilizador = new Utilizador(id, email, nome, password, isClient);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new DAOException("Erro no Get do UtilizadorDAO");
            }
            return utilizador;
        }

        public void put(int key, Utilizador value)
        {
            string query;
            if (ContainsKey(key))
            {
                query = "UPDATE Utilizador SET Email = @Email, Nome = @Nome, Password = @Password, IsClient = @IsClient WHERE IdUtilizador = @Key";
            }
            else
            {
                query = "INSERT INTO Utilizador (IdUtilizador, Email, Nome, Password, IsClient) VALUES (@Key, @Email, @Nome, @Password, @IsClient)";
            }
            try
            {
                using (SqlConnection con = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Key", key);
                        cmd.Parameters.AddWithValue("@Email", value.Email);
                        cmd.Parameters.AddWithValue("@Nome", value.Nome);
                        cmd.Parameters.AddWithValue("@Password", value.Password);
                        cmd.Parameters.AddWithValue("@IsClient", value.IsClient);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw new DAOException("Erro no Put do UtilizadorDAO");
            }
        }

        public Utilizador Remove(int key)
        {
            Utilizador? utilizador = Get(key);
            if (utilizador != null)
            {
                string query = "DELETE FROM Utilizador WHERE IdUtilizador = @Key";
                try
                {
                    using (SqlConnection con = new SqlConnection(DAOconfig.GetConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@Key", key);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception)
                {
                    throw new DAOException("Erro no Remove do UtilizadorDAO");
                }
            }
            return utilizador;
        }

        public ICollection<Utilizador> Values()
        {
            ICollection<Utilizador> utilizadores = new List<Utilizador>();
            string query = "SELECT * FROM Utilizador";
            try
            {
                using (SqlConnection con = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("IdUtilizador"));
                                string email = reader.GetString(reader.GetOrdinal("Email"));
                                string nome = reader.GetString(reader.GetOrdinal("Nome"));
                                string password = reader.GetString(reader.GetOrdinal("Password"));
                                bool isClient = reader.GetBoolean(reader.GetOrdinal("IsClient"));

                                utilizadores.Add(new Utilizador(id, email, nome, password, isClient));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new DAOException("Erro no Values do UtilizadorDAO");
            }
            return utilizadores;
        }
    }
}
