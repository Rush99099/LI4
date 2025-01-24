using Microsoft.Data.SqlClient;
using BMW.Data.Models;

namespace BMW.Data.Data
{
    public class VeiculoDAO
    {
        private static VeiculoDAO? _instance;

        // Construtor privado para implementar Singleton
        private VeiculoDAO() { }

        // Singleton para garantir uma única instância
        public static VeiculoDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new VeiculoDAO();
            }
            return _instance;
        }

        // Verifica se um veículo existe na base de dados pelo ID
        public bool ContainsKey(int id)
        {
            string query = "SELECT COUNT(*) FROM Veiculo WHERE idVeiculo = @Id";
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
                throw new DAOException($"Erro ao verificar existência do veículo com ID {id}: {ex.Message}");
            }
        }

        // Obtém um veículo pelo ID
        public Veiculo? Get(int id)
        {
            string query = "SELECT * FROM Veiculo WHERE idVeiculo = @Id";
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
                                return new Veiculo(
                                    reader.GetInt32(reader.GetOrdinal("idVeiculo")),
                                    reader.GetString(reader.GetOrdinal("Modelo")),
                                    reader.GetInt32(reader.GetOrdinal("PrecoBase")),
                                    reader.GetDateTime(reader.GetOrdinal("DataAdicao"))
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter veículo com ID {id}: {ex.Message}");
            }
            return null;
        }

        // Insere ou atualiza um veículo na base de dados
        public void Put(Veiculo veiculo)
        {
            string query = ContainsKey(veiculo.Id)
                ? "UPDATE Veiculo SET Modelo = @Modelo, PrecoBase = @PrecoBase, DataAdicao = @DataAdicao WHERE idVeiculo = @Id"
                : "INSERT INTO Veiculo (idVeiculo, Modelo, PrecoBase, DataAdicao) VALUES (@Id, @Modelo, @PrecoBase, @DataAdicao)";
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", veiculo.Id);
                        command.Parameters.AddWithValue("@Modelo", veiculo.Modelo);
                        command.Parameters.AddWithValue("@PrecoBase", veiculo.PrecoBase);
                        command.Parameters.AddWithValue("@DataAdicao", veiculo.DataAdicao);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao salvar veículo com ID {veiculo.Id}: {ex.Message}");
            }
        }

        // Remove um veículo pelo ID
        public void Remove(int id)
        {
            string query = "DELETE FROM Veiculo WHERE idVeiculo = @Id";
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
                throw new DAOException($"Erro ao remover veículo com ID {id}: {ex.Message}");
            }
        }

        // Obtém todos os veículos
        public ICollection<Veiculo> GetAll()
        {
            string query = "SELECT * FROM Veiculo";
            var veiculos = new List<Veiculo>();
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
                                var veiculo = new Veiculo(
                                    reader.GetInt32(reader.GetOrdinal("idVeiculo")),
                                    reader.GetString(reader.GetOrdinal("Modelo")),
                                    reader.GetInt32(reader.GetOrdinal("PrecoBase")),
                                    reader.GetDateTime(reader.GetOrdinal("DataAdicao"))
                                );
                                veiculos.Add(veiculo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException($"Erro ao obter todos os veículos: {ex.Message}");
            }
            return veiculos;
        }
    }
}
