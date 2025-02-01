using BMW.Data.Models;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BMW.Data.Data
{
    public class EstadoDAO
    {
        private static EstadoDAO? _instance;

        private EstadoDAO() { }

        public static EstadoDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EstadoDAO();
            }
            return _instance;
        }

        // Obter todos os estados da tabela Estado
        public List<Estado> GetAll()
        {
            var estados = new List<Estado>();

            string query = "SELECT idEstado, Estado FROM Estado";

            try
            {
                using (var connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var estado = new Estado
                                {
                                    IdEstado = reader.GetInt32(reader.GetOrdinal("idEstado")),
                                    NomeEstado = reader.GetString(reader.GetOrdinal("Estado"))
                                };
                                estados.Add(estado);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter estados: {ex.Message}", ex);
            }

            return estados;
        }

        // Obter estado por ID
        public Estado? GetById(int idEstado)
        {
            string query = "SELECT idEstado, Estado FROM Estado WHERE idEstado = @idEstado";

            try
            {
                using (var connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idEstado", idEstado);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Estado
                                {
                                    IdEstado = reader.GetInt32(reader.GetOrdinal("idEstado")),
                                    NomeEstado = reader.GetString(reader.GetOrdinal("Estado"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter estado com ID {idEstado}: {ex.Message}", ex);
            }

            return null;
        }

        // Adicionar ou atualizar um estado
        public void Save(Estado estado)
        {
            string query = ContainsKey(estado.IdEstado)
                ? "UPDATE Estado SET Estado = @Descricao WHERE idEstado = @IdEstado"
                : "INSERT INTO Estado (idEstado, Estado) VALUES (@IdEstado, @Descricao)";

            try
            {
                using (var connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdEstado", estado.IdEstado);
                        command.Parameters.AddWithValue("@Descricao", estado.NomeEstado);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao salvar estado com ID {estado.IdEstado}: {ex.Message}", ex);
            }
        }

        // Verificar se um estado existe
        public bool ContainsKey(int idEstado)
        {
            string query = "SELECT COUNT(*) FROM Estado WHERE idEstado = @idEstado";

            try
            {
                using (var connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idEstado", idEstado);

                        var count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao verificar existência do estado com ID {idEstado}: {ex.Message}", ex);
            }
        }

        // Remover um estado por ID
        public void Remove(int idEstado)
        {
            string query = "DELETE FROM Estado WHERE idEstado = @idEstado";

            try
            {
                using (var connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idEstado", idEstado);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao remover estado com ID {idEstado}: {ex.Message}", ex);
            }
        }
    }
}
