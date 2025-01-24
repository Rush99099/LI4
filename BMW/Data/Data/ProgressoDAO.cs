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
        public bool ContainsKey(int idEncomenda, int idFase)
        {
            string query = "SELECT COUNT(*) FROM Progresso WHERE idEncomenda = @IdEncomenda AND idFase = @IdFase";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdEncomenda", idEncomenda);
                        command.Parameters.AddWithValue("@IdFase", idFase);
                        connection.Open();
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao verificar a existência do progresso com ID {idEncomenda} {idFase}: {ex.Message}");
            }
        }

        // Obtém um progresso pelo ID
        public Progresso? Get(int idEncomenda, int idFase)
        {
            string query = "SELECT * FROM Progresso WHERE idEncomenda = @IdEncomenda AND idFase = @IdFase";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdEncomenda", idEncomenda);
                        command.Parameters.AddWithValue("@IdFase", idFase);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Progresso(
                                    reader.GetInt32(reader.GetOrdinal("idEncomenda")),
                                    reader.GetInt32(reader.GetOrdinal("idFase")),
                                    reader.GetDateTime(reader.GetOrdinal("StartFase")),
                                    reader.GetDateTime(reader.GetOrdinal("EndFase")),
                                    reader.GetString(reader.GetOrdinal("Observacoes")),
                                    reader.GetInt32(reader.GetOrdinal("idFuncionario"))
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter o progresso com ID {idEncomenda} {idFase}: {ex.Message}");
            }
            return null;
        }

        // Insere ou atualiza um progresso na base de dados
        public void Put(Progresso progresso)
        {
            string query = ContainsKey(progresso.IdEncomenda, progresso.IdFase)
                ? "UPDATE Progresso SET StartFase = @StartFase, EndFase = @EndFase, Observacoes = @Observacoes, idFuncionario = @IdFuncionario WHERE idEncomenda = @IdEncomenda AND idFase = @IdFase"
                : "INSERT INTO Progresso (idEncomenda, StartFase, EndFase, Observacoes, idFuncionario) VALUES (@Id, @StartFase, @EndFase, @Observacoes, @IdFuncionario)";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdEncomenda", progresso.IdEncomenda);
                        command.Parameters.AddWithValue("@IdFase", progresso.IdFase);
                        command.Parameters.AddWithValue("@StartFase", progresso.StartFase);
                        command.Parameters.AddWithValue("@EndFase", progresso.EndFase);
                        command.Parameters.AddWithValue("@Observacoes", progresso.Observacoes);
                        command.Parameters.AddWithValue("@IdFuncionario", progresso.IdFuncionario);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao salvar o progresso com ID {progresso.IdEncomenda} {progresso.IdFase}: {ex.Message}");
            }
        }

        // Remove um progresso pelo ID
        public void Remove(int idEncomenda, int idFase)
        {
            string query = "DELETE FROM Progresso WHERE idEncomenda = @IdEncomenda AND idFase = @IdFase";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdEncomenda", idEncomenda);
                        command.Parameters.AddWithValue("@IdFase", idFase);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao remover o progresso com ID {idEncomenda} {idFase}: {ex.Message}");
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
                                    reader.GetInt32(reader.GetOrdinal("idEncomenda")),
                                    reader.GetInt32(reader.GetOrdinal("idFase")),
                                    reader.GetDateTime(reader.GetOrdinal("StartFase")),
                                    reader.GetDateTime(reader.GetOrdinal("EndFase")),
                                    reader.GetString(reader.GetOrdinal("Observacoes")),
                                    reader.GetInt32(reader.GetOrdinal("idFuncionario"))
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
