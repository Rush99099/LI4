using BMW.Data.Models;
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
            string query = "SELECT `idEstado de montagem`, Ordem, Descricao, TempoExecucaoExpectavel FROM `Fase de montagem` WHERE `idEstado de montagem` = @idEstado";
            try
            {
                using (var connection = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idEstado", idEstado);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new FaseDeMontagem(
                                    reader.GetInt32("idEstado de montagem"),
                                    reader.GetInt32("Ordem"),
                                    reader.GetString("Descricao"),
                                    reader.GetTimeSpan("TempoExecucaoExpectavel")
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
            const string query = "SELECT `idEstado de montagem`, Ordem, Descricao, TempoExecucaoExpectavel FROM `Fase de montagem`";

            try
            {
                using (var connection = new MySqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (var command = new MySqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                fases.Add(new FaseDeMontagem(
                                    reader.GetInt32("idEstado de montagem"),
                                    reader.GetInt32("Ordem"),
                                    reader.GetString("Descricao"),
                                    reader.GetTimeSpan("TempoExecucaoExpectavel")
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
            string query = "SELECT * FROM `Fase de montagem` WHERE `idEstado de montagem` = @Id";
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
                                return new FaseDeMontagem
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("idEstado de montagem")),
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
                throw new DAOException($"Erro ao obter fase de montagem com ID {id}: {ex.Message}", ex);
            }
            return null;
        }
        
    }
}
