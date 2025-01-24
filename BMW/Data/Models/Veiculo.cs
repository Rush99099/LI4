namespace BMW.Data.Models
{
    public class Veiculo
    {
        // Propriedades do Veículo
        public int Id { get; set; } // Identificador único do veículo
        public string Modelo { get; set; } // Modelo do veículo (ex.: "BMW Série 3")
        public int PrecoBase { get; set; } // Preço base do veículo
        public DateTime DataAdicao { get; set; } // Data de adição do veículo ao sistema

        // Construtor padrão
        public Veiculo(int id, string modelo, int precoBase, DateTime dataAdicao)
        {
            Id = id;
            Modelo = modelo;
            PrecoBase = precoBase;
            DataAdicao = dataAdicao;
        }
    }
}
