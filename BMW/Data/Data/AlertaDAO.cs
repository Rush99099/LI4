using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BMW.Data.Models;

namespace BMW.Data.Data
{
    public class AlertaDAO
    {
        private static AlertaDAO? _instance;

        // Construtor privado para implementar Singleton
        private AlertaDAO() { }

        // Singleton para garantir uma única instância
        public static AlertaDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AlertaDAO();
            }
            return _instance;
        }

        // Verifica se um alerta existe na base de dados pelo ID
        public bool ContainsKey(int idAlerta)
        {
            string query = "SELECT COUNT(*) FROM Alerta WHERE idAlerta = @IdAlerta";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdAlerta", idAlerta);
                        connection.Open();
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao verificar existência do alerta com ID {idAlerta}: {ex.Message}");
            }
        }

        // Obtém um alerta pelo ID
        public Alerta? Get(int idAlerta)
        {
            string query = "SELECT * FROM Alerta WHERE idAlerta = @IdAlerta";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdAlerta", idAlerta);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Alerta(
                                    reader.GetInt32(reader.GetOrdinal("idAlerta")),
                                    reader.GetInt32(reader.GetOrdinal("IdProgresso")),
                                    reader.GetString(reader.GetOrdinal("Mensagem")),
                                    reader.GetDateTime(reader.GetOrdinal("Data"))
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter alerta com ID {idAlerta}: {ex.Message}");
            }
            return null;
        }

        // Insere ou atualiza um alerta na base de dados
        public void Put(Alerta alerta)
        {
            string query = ContainsKey(alerta.IdAlerta)
                ? "UPDATE Alerta SET idProgresso = @IdProgresso, Mensagem = @Mensagem, Data = @Data WHERE idAlerta = @IdAlerta"
                : "INSERT INTO Alerta (idAlerta, idProgresso, Mensagem, Data) VALUES (@IdAlerta, @IdProgresso, @Mensagem, @Data)";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdAlerta", alerta.IdAlerta);
                        command.Parameters.AddWithValue("@IdProgresso", alerta.IdProgresso);
                        command.Parameters.AddWithValue("@Mensagem", alerta.Mensagem);
                        command.Parameters.AddWithValue("@Data", alerta.Data);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao salvar alerta com ID {alerta.IdAlerta}: {ex.Message}");
            }
        }

        // Remove um alerta pelo ID
        public void Remove(int idAlerta)
        {
            string query = "DELETE FROM Alerta WHERE idAlerta = @IdAlerta";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdAlerta", idAlerta);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao remover alerta com ID {idAlerta}: {ex.Message}");
            }
        }

        // Obtém todos os alertas
        public ICollection<Alerta> GetAll()
        {
            string query = "SELECT * FROM Alerta";
            var alertas = new List<Alerta>();
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
                                var alerta = new Alerta(
                                    reader.GetInt32(reader.GetOrdinal("idAlerta")),
                                    reader.GetInt32(reader.GetOrdinal("idProgresso")),
                                    reader.GetString(reader.GetOrdinal("Mensagem")),
                                    reader.GetDateTime(reader.GetOrdinal("Data"))
                                );
                                alertas.Add(alerta);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter todos os alertas: {ex.Message}");
            }
            return alertas;
        }
    }
}
