namespace BMW.Data.Models
{
    public class Encomenda  
    {
        // Propriedades da Encomenda
        public int Id { get; set; } // Identificador único da encomenda
        public DateTime DataRegisto { get; set; } // Data de criação da encomenda
        public string? Observacoes { get; set; } // Estado atual da encomenda
        public int IdCliente { get; set; }
        public int IdVeiculo { get; set; }
        public int Estado { get; set; }
        // Construtor padrão
        public Encomenda(int id, DateTime data, string? observacoes, int idCliente, int idVeiculo, int estado)
        {
            Id = id;
            DataRegisto = data;
            Observacoes = observacoes;
            IdCliente = idCliente;
            IdVeiculo = idVeiculo;
            Estado = estado;
        }
    }
}