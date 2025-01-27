using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BMW.Data.Models;
using MySql.Data.MySqlClient;

namespace BMW.Data.Data
{
    public class EncomendaDAO
    {
        private static EncomendaDAO? _instance;

        // Construtor privado para implementar Singleton
        private EncomendaDAO() { }

        // Singleton para garantir uma única instância
        public static EncomendaDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EncomendaDAO();
            }
            return _instance;
        }

        // Verifica se uma encomenda existe na base de dados pelo ID
        public bool ContainsKey(int id)
        {
            string query = "SELECT COUNT(*) FROM Encomenda WHERE idEncomenda = @Id";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        connection.Open();
                        long count = (long)command.ExecuteScalar(); // `COUNT(*)` retorna um `long`
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao verificar existência da encomenda com ID {id}: {ex.Message}");
            }
        }


        // Obtém uma encomenda pelo ID
        public Encomenda? Get(int id)
        {
            string query = "SELECT * FROM Encomenda WHERE idEncomenda = @Id";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Encomenda(
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetDateTime(reader.GetOrdinal("DataRegisto")),
                                    reader.IsDBNull(reader.GetOrdinal("Observacoes")) ? null : reader.GetString(reader.GetOrdinal("Observacoes")),
                                    reader.GetInt32(reader.GetOrdinal("IdCliente")),
                                    reader.GetInt32(reader.GetOrdinal("IdVeiculo")),
                                    reader.GetInt32(reader.GetOrdinal("Estado"))
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter encomenda com ID {id}: {ex.Message}");
            }
            return null;
        }

        // Insere ou atualiza uma encomenda na base de dados
        public void Put(Encomenda encomenda)
        {
            string query = encomenda.Id > 0 && ContainsKey(encomenda.Id)
                ? "UPDATE Encomenda SET DataRegisto = @DataRegisto, Observacoes = @Observacoes, idCliente = @IdCliente, idVeiculo = @IdVeiculo, Estado = @Estado WHERE idEncomenda = @Id"
                : "INSERT INTO Encomenda (DataRegisto, Observacoes, idCliente, idVeiculo, Estado) VALUES (@DataRegisto, @Observacoes, @IdCliente, @IdVeiculo, @Estado)";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        if (encomenda.Id > 0 && ContainsKey(encomenda.Id))
                        {
                            command.Parameters.AddWithValue("@Id", encomenda.Id);
                        }

                        command.Parameters.AddWithValue("@DataRegisto", encomenda.DataRegisto);
                        command.Parameters.AddWithValue("@Observacoes", (object?)encomenda.Observacoes ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IdCliente", encomenda.IdCliente);
                        command.Parameters.AddWithValue("@IdVeiculo", encomenda.IdVeiculo);
                        command.Parameters.AddWithValue("@Estado", encomenda.Estado);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao salvar encomenda com ID {encomenda.Id}: {ex.Message}");
            }
        }


        // Remove uma encomenda pelo ID
        public void Remove(int id)
        {
            string query = "DELETE FROM Encomenda WHERE idEncomenda = @Id";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao remover encomenda com ID {id}: {ex.Message}");
            }
        }

        // Obtém todas as encomendas
        public ICollection<Encomenda> GetAll()
        {
            string query = "SELECT * FROM Encomenda";
            var encomendas = new List<Encomenda>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var encomenda = new Encomenda(
                                    reader.GetInt32(reader.GetOrdinal("idEncomenda")),
                                    reader.GetDateTime(reader.GetOrdinal("DataRegisto")),
                                    reader.IsDBNull(reader.GetOrdinal("Observacoes")) ? null : reader.GetString(reader.GetOrdinal("Observacoes")),
                                    reader.GetInt32(reader.GetOrdinal("idCliente")),
                                    reader.GetInt32(reader.GetOrdinal("idVeiculo")),
                                    reader.GetInt32(reader.GetOrdinal("Estado"))
                                );
                                encomendas.Add(encomenda);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter todas as encomendas: {ex.Message}");
            }
            return encomendas;
        }

        public ICollection<Encomenda> GetByClienteID(int idCliente)
        {
            string query = "SELECT * FROM Encomenda WHERE idCliente = @IdCliente";
            var encomendas = new List<Encomenda>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdCliente", idCliente);
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var encomenda = new Encomenda(
                                    reader.GetInt32(reader.GetOrdinal("idEncomenda")),
                                    reader.GetDateTime(reader.GetOrdinal("DataRegisto")),
                                    reader.IsDBNull(reader.GetOrdinal("Observacoes")) ? null : reader.GetString(reader.GetOrdinal("Observacoes")),
                                    reader.GetInt32(reader.GetOrdinal("idCliente")),
                                    reader.GetInt32(reader.GetOrdinal("idVeiculo")),
                                    reader.GetInt32(reader.GetOrdinal("Estado"))
                                );
                                encomendas.Add(encomenda);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter encomendas para o cliente com ID {idCliente}: {ex.Message}");
            }
            return encomendas;
        }
    }
}
