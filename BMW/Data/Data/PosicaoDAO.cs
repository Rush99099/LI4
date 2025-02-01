using System;
using System.Collections.Generic;
using System.Data;
using BMW.Data.Models;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace BMW.Data.Data
{
    public class PosicaoDAO
    {
        private static PosicaoDAO? _instance;

        // Singleton para garantir que apenas uma instância do PosicaoDAO seja criada
        public static PosicaoDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PosicaoDAO();
            }
            return _instance;
        }

        // Método para buscar uma posição pelo ID
        public Posicao? Get(int idPosicao)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    string query = "SELECT * FROM Posicao WHERE idPosicao = @idPosicao";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idPosicao", idPosicao);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Posicao
                                {
                                    IdPosicao = reader.GetInt32("idPosicao"),
                                    Tipo = reader.GetString("Tipo")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter a posição com ID {idPosicao}: {ex.Message}");
            }

            return null;
        }

        // Método para buscar todas as posições
        public List<Posicao> GetAll()
        {
            var posicoes = new List<Posicao>();

            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    string query = "SELECT * FROM Posicao";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                posicoes.Add(new Posicao
                                {
                                    IdPosicao = reader.GetInt32("idPosicao"),
                                    Tipo = reader.GetString("Tipo")
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter todas as posições: {ex.Message}");
            }

            return posicoes;
        }
    }

}
