using BMW.Data.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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
                using (MySqlConnection connection = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdEncomenda", idEncomenda);
                        command.Parameters.AddWithValue("@IdFase", idFase);
                        connection.Open();
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao verificar a existência do progresso com ID {idEncomenda} e fase {idFase}: {ex.Message}");
            }
        }

        // Obtém um progresso pelo ID
        public Progresso? Get(int idEncomenda, int idFase)
        {
            string query = "SELECT * FROM Progresso WHERE idEncomenda = @IdEncomenda AND idFase = @IdFase";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdEncomenda", idEncomenda);
                        command.Parameters.AddWithValue("@IdFase", idFase);
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Progresso(
                                    reader.GetInt32(reader.GetOrdinal("idEncomenda")),
                                    reader.GetInt32(reader.GetOrdinal("idFase")),
                                    reader.GetDateTime(reader.GetOrdinal("StartFase")),
                                    reader.IsDBNull(reader.GetOrdinal("EndFase")) ? null : reader.GetDateTime(reader.GetOrdinal("EndFase")),
                                    reader.GetInt32(reader.GetOrdinal("idFuncionario"))
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter o progresso com ID {idEncomenda} e fase {idFase}: {ex.Message}");
            }
            return null;
        }

        // Insere ou atualiza um progresso na base de dados
        public void Put(Progresso progresso)
        {
            string query = ContainsKey(progresso.IdEncomenda, progresso.IdFase)
                ? "UPDATE Progresso SET StartFase = @StartFase, EndFase = @EndFase, idFuncionario = @IdFuncionario WHERE idEncomenda = @IdEncomenda AND idFase = @IdFase"
                : "INSERT INTO Progresso (idEncomenda, idFase, StartFase, EndFase, idFuncionario) VALUES (@IdEncomenda, @IdFase, @StartFase, @EndFase, @IdFuncionario)";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdEncomenda", progresso.IdEncomenda);
                        command.Parameters.AddWithValue("@IdFase", progresso.IdFase);
                        command.Parameters.AddWithValue("@StartFase", progresso.StartFase);
                        command.Parameters.AddWithValue("@EndFase", progresso.EndFase);
                        command.Parameters.AddWithValue("@IdFuncionario", progresso.IdFuncionario);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao salvar o progresso com ID {progresso.IdEncomenda} e fase {progresso.IdFase}: {ex.Message}");
            }
        }

        // Obtém todos os progressos
        public ICollection<Progresso> GetAll()
        {
            string query = "SELECT * FROM Progresso";
            var progressos = new List<Progresso>();
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
                                progressos.Add(new Progresso(
                                    reader.GetInt32(reader.GetOrdinal("idEncomenda")),
                                    reader.GetInt32(reader.GetOrdinal("idFase")),
                                    reader.GetDateTime(reader.GetOrdinal("StartFase")),
                                    reader.IsDBNull(reader.GetOrdinal("EndFase")) ? null : reader.GetDateTime(reader.GetOrdinal("EndFase")),
                                    reader.GetInt32(reader.GetOrdinal("idFuncionario"))
                                ));
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

        // Obtém progressos por ID da encomenda
        public List<Progresso> GetByEncomendaId(int encomendaId)
        {
            string query = "SELECT * FROM Progresso WHERE idEncomenda = @IdEncomenda";
            var progressoList = new List<Progresso>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdEncomenda", encomendaId);
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                progressoList.Add(new Progresso(
                                    reader.GetInt32(reader.GetOrdinal("idEncomenda")),
                                    reader.GetInt32(reader.GetOrdinal("idFase")),
                                    reader.GetDateTime(reader.GetOrdinal("StartFase")),
                                    reader.IsDBNull(reader.GetOrdinal("EndFase")) ? null : reader.GetDateTime(reader.GetOrdinal("EndFase")),
                                    reader.GetInt32(reader.GetOrdinal("idFuncionario"))
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao buscar progresso da encomenda {encomendaId}: {ex.Message}");
            }
            return progressoList;
        }
    }
}
