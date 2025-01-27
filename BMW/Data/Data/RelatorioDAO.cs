using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BMW.Data.Models;
using MySql.Data.MySqlClient;

namespace BMW.Data.Data
{
    public class RelatorioDAO
    {
        private static RelatorioDAO? _instance;

        // Construtor privado para implementar Singleton
        private RelatorioDAO() { }

        // Singleton para garantir uma única instância
        public static RelatorioDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RelatorioDAO();
            }
            return _instance;
        }

        // Verifica se um relatório existe na base de dados pelo ID
        public bool ContainsKey(int id)
        {
            string query = "SELECT COUNT(*) FROM Relatorio WHERE idRelatorio = @Id";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        connection.Open();
                        long count = (long)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao verificar existência do relatório com ID {id}: {ex.Message}");
            }
        }

        // Obtém um relatório pelo ID
        public Relatorio? Get(int id)
        {
            string query = "SELECT * FROM Relatorio WHERE idRelatorio = @Id";
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
                                return new Relatorio(
                                    reader.GetInt32(reader.GetOrdinal("idRelatorio")),
                                    reader.GetInt32(reader.GetOrdinal("Tipo")),
                                    reader.GetDateTime(reader.GetOrdinal("DataGeracao")),
                                    reader.GetString(reader.GetOrdinal("Conteudo")),
                                    reader.GetInt32(reader.GetOrdinal("idFuncionario"))
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter relatório com ID {id}: {ex.Message}");
            }
            return null;
        }


        // Insere ou atualiza um relatório na base de dados
        public void Put(Relatorio relatorio)
        {
            string query = ContainsKey(relatorio.Id)
                ? "UPDATE Relatorio SET Tipo = @Tipo, DataGeracao = @DataGeracao, Conteudo = @Conteudo, idFuncionario = @IdFuncionario WHERE idRelatorio = @Id"
                : "INSERT INTO Relatorio (Tipo, DataGeracao, Conteudo, idFuncionario) VALUES (@Tipo, @DataGeracao, @Conteudo, @IdFuncionario)";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        if (ContainsKey(relatorio.Id)) command.Parameters.AddWithValue("@Id", relatorio.Id);
                        command.Parameters.AddWithValue("@Tipo", relatorio.Tipo);
                        command.Parameters.AddWithValue("@DataGeracao", relatorio.DataGeracao);
                        command.Parameters.AddWithValue("@Conteudo", relatorio.Conteudo);
                        command.Parameters.AddWithValue("@IdFuncionario", relatorio.IdFuncionario);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao salvar relatório: {ex.Message}");
            }
        }
        


        // Remove um relatório pelo ID
        public void Remove(int id)
        {
            string query = "DELETE FROM Relatorio WHERE idRelatorio = @Id";
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
                throw new DAOException($"Erro ao remover relatório com ID {id}: {ex.Message}");
            }
        }

        // Obtém todos os relatórios
        public ICollection<Relatorio> GetAll()
        {
            string query = "SELECT * FROM Relatorio";
            var relatorios = new List<Relatorio>();
        
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
                                var relatorio = new Relatorio
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("idRelatorio")),
                                    Tipo = reader.GetInt32(reader.GetOrdinal("Tipo")), // Supondo que "Tipo" é um inteiro no banco
                                    DataGeracao = reader.GetDateTime(reader.GetOrdinal("DataGeracao")),
                                    Conteudo = reader.GetString(reader.GetOrdinal("Conteudo")), // Certifique-se que "Conteudo" é VARCHAR no banco
                                    IdFuncionario = reader.GetInt32(reader.GetOrdinal("idFuncionario"))
                                };
                                relatorios.Add(relatorio);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter todos os relatórios: {ex.Message}");
            }
        
            return relatorios;
        }
    }
}
