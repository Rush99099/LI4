using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BMW.Data.Models;

namespace BMW.Data.Data
{
    public class ProgressoDAO
    {
        private static ProgressoDAO? _instance;

        // Construtor privado para implementar Singleton
        private ProgressoDAO() { }

        // Singleton para garantir uma única instância
        public static ProgressoDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ProgressoDAO();
            }
            return _instance;
        }

        // Verifica se um progresso existe na base de dados pelo ID
        public bool ContainsKey(int id)
        {
            string query = "SELECT COUNT(*) FROM Progresso WHERE Id = @Id";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        connection.Open();
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao verificar a existência do progresso com ID {id}: {ex.Message}");
            }
        }

        // Obtém um progresso pelo ID
        public Progresso? Get(int id)
        {
            string query = "SELECT * FROM Progresso WHERE Id = @Id";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Progresso(
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetDateTime(reader.GetOrdinal("DataRegisto")),
                                    reader.GetInt32(reader.GetOrdinal("TempoExecucao")),
                                    reader.GetString(reader.GetOrdinal("Observacoes")),
                                    reader.GetInt32(reader.GetOrdinal("IdFuncionario"))
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter o progresso com ID {id}: {ex.Message}");
            }
            return null;
        }

        // Insere ou atualiza um progresso na base de dados
        public void Put(Progresso progresso)
        {
            string query = ContainsKey(progresso.Id)
                ? "UPDATE Progresso SET DataRegisto = @DataRegisto, TempoExecucao = @TempoExecucao, Observacoes = @Observacoes, IdFuncionario = @IdFuncionario WHERE Id = @Id"
                : "INSERT INTO Progresso (Id, DataRegisto, TempoExecucao, Observacoes, IdFuncionario) VALUES (@Id, @DataRegisto, @TempoExecucao, @Observacoes, @IdFuncionario)";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", progresso.Id);
                        command.Parameters.AddWithValue("@DataRegisto", progresso.DataRegisto);
                        command.Parameters.AddWithValue("@TempoExecucao", progresso.TempoExecucao);
                        command.Parameters.AddWithValue("@Observacoes", progresso.Observacoes);
                        command.Parameters.AddWithValue("@IdFuncionario", progresso.IdFuncionario);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao salvar o progresso com ID {progresso.Id}: {ex.Message}");
            }
        }

        // Remove um progresso pelo ID
        public void Remove(int id)
        {
            string query = "DELETE FROM Progresso WHERE Id = @Id";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao remover o progresso com ID {id}: {ex.Message}");
            }
        }

        // Obtém todos os progressos
        public ICollection<Progresso> GetAll()
        {
            string query = "SELECT * FROM Progresso";
            var progressos = new List<Progresso>();
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var progresso = new Progresso(
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetDateTime(reader.GetOrdinal("DataRegisto")),
                                    reader.GetInt32(reader.GetOrdinal("TempoExecucao")),
                                    reader.GetString(reader.GetOrdinal("Observacoes")),
                                    reader.GetInt32(reader.GetOrdinal("IdFuncionario"))
                                );
                                progressos.Add(progresso);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter todos os progressos: {ex.Message}");
            }
            return progressos;
        }
    }
}
