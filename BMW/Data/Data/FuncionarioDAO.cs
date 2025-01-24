using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BMW.Data.Models;

namespace BMW.Data.Data
{
    public class FuncionarioDAO
    {
        private static FuncionarioDAO? _instance;

        // Construtor privado para Singleton
        private FuncionarioDAO() { }

        // Singleton para garantir uma única instância
        public static FuncionarioDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new FuncionarioDAO();
            }
            return _instance;
        }

        // Verifica se um funcionário existe na base de dados pelo ID
        public bool ContainsKey(int idFuncionario)
        {
            string query = "SELECT COUNT(*) FROM Funcionario WHERE IdFuncionario = @IdFuncionario";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdFuncionario", idFuncionario);
                        connection.Open();
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao verificar existência do funcionário com ID {idFuncionario}: {ex.Message}");
            }
        }

        // Obtém um funcionário pelo ID
        public Funcionario? Get(int idFuncionario)
        {
            string query = "SELECT * FROM Funcionario WHERE IdFuncionario = @IdFuncionario";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdFuncionario", idFuncionario);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int? supervisor = reader.IsDBNull(reader.GetOrdinal("Supervisor")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Supervisor"));

                                return new Funcionario(
                                    reader.GetInt32(reader.GetOrdinal("IdFuncionario")),
                                    reader.GetDateTime(reader.GetOrdinal("ContractDate")),
                                    reader.GetInt32(reader.GetOrdinal("Posicao")),
                                    reader.GetInt32(reader.GetOrdinal("Supervisor")) 
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter funcionário com ID {idFuncionario}: {ex.Message}");
            }
            return null;
        }


        // Insere ou atualiza um funcionário na base de dados
        public void Put(Funcionario funcionario)
        {
            string query = ContainsKey(funcionario.IdFuncionario)
                ? "UPDATE Funcionario SET ContractDate = @ContractDate, Posicao = @Posicao, Supervisor = @Supervisor WHERE IdFuncionario = @IdFuncionario"
                : "INSERT INTO Funcionario (IdFuncionario, ContractDate, Posicao, Supervisor) VALUES (@IdFuncionario, @ContractDate, @Posicao, @Supervisor)";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdFuncionario", funcionario.IdFuncionario);
                        command.Parameters.AddWithValue("@ContractDate", funcionario.ContractDate);
                        command.Parameters.AddWithValue("@Posicao", funcionario.Posicao);
                        command.Parameters.AddWithValue("@Supervisor", (object?)funcionario.Supervisor ?? DBNull.Value);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao salvar funcionário com ID {funcionario.IdFuncionario}: {ex.Message}");
            }
        }

        // Remove um funcionário pelo ID
        public void Remove(int idFuncionario)
        {
            string query = "DELETE FROM Funcionario WHERE IdFuncionario = @IdFuncionario";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdFuncionario", idFuncionario);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao remover funcionário com ID {idFuncionario}: {ex.Message}");
            }
        }

        // Obtém todos os funcionários
        public ICollection<Funcionario> GetAll()
        {
            string query = "SELECT * FROM Funcionario";
            var funcionarios = new List<Funcionario>();
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
                                var funcionario = new Funcionario(
                                    reader.GetInt32(reader.GetOrdinal("IdFuncionario")),
                                    reader.GetDateTime(reader.GetOrdinal("ContractDate")),
                                    reader.GetInt32(reader.GetOrdinal("Posicao")),
                                    reader.GetInt32(reader.GetOrdinal("Supervisor")) 
                                );
                                funcionarios.Add(funcionario);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter todos os funcionários: {ex.Message}");
            }
            return funcionarios;
        }
    }
}
