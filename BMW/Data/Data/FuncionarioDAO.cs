using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BMW.Data.Models;
using MySql.Data.MySqlClient;

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
        public static bool PosicaoExiste(int posicaoId)
        {
            string query = "SELECT COUNT(*) FROM Posicao WHERE idPosicao = @Id";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", posicaoId);
                        connection.Open();
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao verificar existência da posição com ID {posicaoId}: {ex.Message}");
            }
        }

        // Verifica se um funcionário existe na base de dados pelo ID
        public bool ContainsKey(int id)
        {
            string query = "SELECT COUNT(*) FROM Funcionario WHERE idFuncionario = @Id";
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
                throw new DAOException($"Erro ao verificar existência do funcionário com ID {id}: {ex.Message}");
            }
        }


        // Obtém um funcionário pelo ID
        public Funcionario? Get(int idFuncionario)
        {
            string query = "SELECT * FROM Funcionario WHERE idFuncionario = @IdFuncionario";
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
                                return new Funcionario(
                                    idFuncionario: reader.GetInt32(reader.GetOrdinal("idFuncionario")),
                                    contractDate: reader.GetDateTime(reader.GetOrdinal("contractDate")),
                                    posicao: reader.GetInt32(reader.GetOrdinal("Posicao")),
                                    supervisor: reader.IsDBNull(reader.GetOrdinal("supervisor")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("supervisor"))
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
                ? "UPDATE Funcionario SET contractDate = @ContractDate, Posicao = @Posicao, supervisor = @Supervisor WHERE idFuncionario = @IdFuncionario"
                : "INSERT INTO Funcionario (idFuncionario, ContractDate, Posicao, supervisor) VALUES (@IdFuncionario, @ContractDate, @Posicao, @Supervisor)";
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
            string query = "DELETE FROM Funcionario WHERE idFuncionario = @IdFuncionario";
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
                                    reader.GetInt32(reader.GetOrdinal("idFuncionario")),
                                    reader.GetDateTime(reader.GetOrdinal("ContractDate")),
                                    reader.GetInt32(reader.GetOrdinal("Posicao")),
                                    reader.IsDBNull(reader.GetOrdinal("supervisor")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("supervisor"))
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
