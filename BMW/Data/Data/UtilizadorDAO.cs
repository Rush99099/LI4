using Microsoft.Data.SqlClient;
using BMW.Data.Models;
using MySql.Data.MySqlClient;

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
                using (MySqlConnection con = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        con.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
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
                using (MySqlConnection con = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Key", key);
                        con.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
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
                using (MySqlConnection con = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Key", key);
                        con.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("idUtilizador"));
                                string email = reader.GetString(reader.GetOrdinal("email"));
                                string nome = reader.GetString(reader.GetOrdinal("nome"));
                                string password = reader.GetString(reader.GetOrdinal("Password"));
                                bool isClient = reader.GetBoolean(reader.GetOrdinal("isCliente"));

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
            string query = "SELECT * FROM utilizador WHERE email = @Email";
            try
            {
                using (MySqlConnection con = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        con.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("idUtilizador"));
                                //string email = reader.GetString(reader.GetOrdinal("Email"));
                                string nome = reader.GetString(reader.GetOrdinal("nome"));
                                string password = reader.GetString(reader.GetOrdinal("Password"));
                                bool isClient = reader.GetBoolean(reader.GetOrdinal("isCliente"));

                                utilizador = new Utilizador(id, email, nome, password, isClient);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new DAOException("Erro no GetByEmail do UtilizadorDAO", ex);
            }
            return utilizador;
        }

        public void Put(Utilizador value) //put sem key, id de novos utilizadores sao auto increment
        {
            string query;
            if (ContainsEmail(value.Email))
            {
                Console.WriteLine("Test");
                query = "UPDATE Utilizador SET email = @Email, nome = @Nome, Password = @Password, isCliente = @IsClient WHERE email = @Email";
            }
            else
            {
                query = "INSERT INTO Utilizador (email, nome, Password, isCliente) VALUES (@Email, @Nome, @Password, @IsClient)";
            }
            try
            {
                using (MySqlConnection con = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        //cmd.Parameters.AddWithValue("@Key", key);
                        cmd.Parameters.AddWithValue("@Email", value.Email);
                        cmd.Parameters.AddWithValue("@Nome", value.Nome);
                        cmd.Parameters.AddWithValue("@Password", value.Password);
                        cmd.Parameters.AddWithValue("@IsClient", value.IsCliente);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Erro no Put do UtilizadorDAO", ex);
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
                    using (MySqlConnection con = new MySqlConnection(DAOconfig.GetConnectionString()))
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, con))
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
                using (MySqlConnection con = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        con.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("idUtilizador"));
                                string email = reader.GetString(reader.GetOrdinal("email"));
                                string nome = reader.GetString(reader.GetOrdinal("nome"));
                                string password = reader.GetString(reader.GetOrdinal("Password"));
                                bool isClient = reader.GetBoolean(reader.GetOrdinal("isCliente"));

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
                using (MySqlConnection con = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        con.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("idUtilizador"));
                                string email = reader.GetString(reader.GetOrdinal("email"));
                                string nome = reader.GetString(reader.GetOrdinal("nome"));
                                string password = reader.GetString(reader.GetOrdinal("Password"));
                                bool isClient = reader.GetBoolean(reader.GetOrdinal("isCliente"));

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
                using (MySqlConnection con = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
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
