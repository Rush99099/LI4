using BMW.Data.Models;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BMW.Data.Data
{
    public class FaseMontagemDAO
    {
        private static FaseMontagemDAO? _instance;

        private FaseMontagemDAO() { }

        public static FaseMontagemDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new FaseMontagemDAO();
            }
            return _instance;
        }

        public FaseDeMontagem? Get(int idEstado)
        {
            string query = "SELECT idEstado_de_montagem, Ordem, Descricao, TempoExecucaoExpectavel FROM [Fase de montagem] WHERE idEstado_de_montagem = @idEstado";
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
                                return new FaseDeMontagem(
                                    reader.GetInt32(reader.GetOrdinal("idEstado_de_montagem")),
                                    reader.GetInt32(reader.GetOrdinal("Ordem")),
                                    reader.GetString(reader.GetOrdinal("Descricao")),
                                    reader.GetTimeSpan(reader.GetOrdinal("TempoExecucaoExpectavel"))
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter a Fase de Montagem com ID {idEstado}: {ex.Message}");
            }

            return null;
        }

        public List<FaseDeMontagem> GetAll()
        {
            var fases = new List<FaseDeMontagem>();
            const string query = "SELECT idEstado_de_montagem, Ordem, Descricao, TempoExecucaoExpectavel FROM [Fase de montagem]";

            try
            {
                using (var connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                fases.Add(new FaseDeMontagem(
                                    reader.GetInt32(reader.GetOrdinal("idEstado_de_montagem")),
                                    reader.GetInt32(reader.GetOrdinal("Ordem")),
                                    reader.GetString(reader.GetOrdinal("Descricao")),
                                    reader.GetTimeSpan(reader.GetOrdinal("TempoExecucaoExpectavel"))
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter fases de montagem: {ex.Message}");
            }

            return fases;
        }

        public FaseDeMontagem? GetById(int id)
        {
            string query = "SELECT * FROM [Fase de montagem] WHERE idEstado_de_montagem = @Id";
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
                                return new FaseDeMontagem
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("idEstado_de_montagem")),
                                    Ordem = reader.GetInt32(reader.GetOrdinal("Ordem")),
                                    Descricao = reader.GetString(reader.GetOrdinal("Descricao")),
                                    TempoExecucaoExpectavel = reader.GetTimeSpan(reader.GetOrdinal("TempoExecucaoExpectavel"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new DAOException($"Erro ao obter fase de montagem com ID {id}: {ex.Message}", ex);
            }
            return null;
        }
        
    }
}
