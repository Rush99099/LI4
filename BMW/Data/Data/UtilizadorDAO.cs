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

        public bool ContainsEmail(string email)
        {
            bool result = false;
            string query = "SELECT * FROM Utilizador WHERE email = @email";
            try
            {
                using (SqlConnection con = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
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

        public bool ContainsKey(int key)
        {
            bool result = false;
            string query = "SELECT * FROM Utilizador WHERE idUtilizador = @Key";
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

        public Utilizador? Get(int key)
        {
            Utilizador? utilizador = null;
            string query = "SELECT * FROM Utilizador WHERE idUtilizador = @Key";
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
                                int id = reader.GetInt32(reader.GetOrdinal("idUtilizador"));
                                string email = reader.GetString(reader.GetOrdinal("email"));
                                string nome = reader.GetString(reader.GetOrdinal("nome"));
                                string password = reader.GetString(reader.GetOrdinal("Password"));
                                bool isClient = reader.GetBoolean(reader.GetOrdinal("isClient"));

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
        public Utilizador? GetByEmail(string email)
        {
            Utilizador? utilizador = null;
            string query = "SELECT * FROM Utilizador WHERE idUtilizador = @Email";
            try
            {
                using (SqlConnection con = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("idUtilizador"));
                                //string email = reader.GetString(reader.GetOrdinal("Email"));
                                string nome = reader.GetString(reader.GetOrdinal("nome"));
                                string password = reader.GetString(reader.GetOrdinal("Password"));
                                bool isClient = reader.GetBoolean(reader.GetOrdinal("isClient"));

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

        public void Put(Utilizador value) //put sem key, id de novos utilizadores sao auto increment
        {
            string query;
            if (ContainsEmail(value.Email))
            {
                query = "UPDATE Utilizador SET email = @Email, nome = @Nome, Password = @Password, isClient = @IsClient WHERE email = @Email";
            }
            else
            {
                query = "INSERT INTO Utilizador (email, nome, Password, isClient) VALUES (@Email, @Nome, @Password, @IsClient)";
            }
            try
            {
                using (SqlConnection con = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        //cmd.Parameters.AddWithValue("@Key", key);
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

        public void Remove(int key)
        {
            Utilizador? utilizador = Get(key);
            if (utilizador != null)
            {
                string query = "DELETE FROM Utilizador WHERE idUtilizador = @Key";
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
                                int id = reader.GetInt32(reader.GetOrdinal("idUtilizador"));
                                string email = reader.GetString(reader.GetOrdinal("email"));
                                string nome = reader.GetString(reader.GetOrdinal("nome"));
                                string password = reader.GetString(reader.GetOrdinal("Password"));
                                bool isClient = reader.GetBoolean(reader.GetOrdinal("isClient"));

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

        public List<Utilizador> ValuesClientes()
        {
            List<Utilizador> utilizadores = new List<Utilizador>();
            string query = "SELECT * FROM Utilizador WHERE isCliente = 1";
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
                                int id = reader.GetInt32(reader.GetOrdinal("idUtilizador"));
                                string email = reader.GetString(reader.GetOrdinal("email"));
                                string nome = reader.GetString(reader.GetOrdinal("nome"));
                                string password = reader.GetString(reader.GetOrdinal("Password"));
                                bool isClient = reader.GetBoolean(reader.GetOrdinal("isClient"));

                                utilizadores.Add(new Utilizador(id, email, nome, password, isClient));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Erro no ValuesClientes do UtilizadorDAO", ex);
            }
            return utilizadores;
        }

        public void ChangePassword(int id, string password)
        {
            string query;
            query = "UPDATE Utilizador SET Password = @password WHERE idUtilizador = @IdUtilizador";
            try
            {
                using (SqlConnection con = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IdUtilizador", id);
                        cmd.Parameters.AddWithValue("@password", password);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw new DAOException("Erro no ChangePassword do UtilizadorDAO");
            }
        }
    }
}
