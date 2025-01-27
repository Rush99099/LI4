public class Encomenda
{
    public int Id { get; set; }
    public DateTime DataRegisto { get; set; }
    public string? Observacoes { get; set; }
    public int IdCliente { get; set; }
    public int IdVeiculo { get; set; }
    public int Estado { get; set; }

    public Encomenda(int id, DateTime dataRegisto, string? observacoes, int idCliente, int idVeiculo, int estado)
    {
        Id = id;
        DataRegisto = dataRegisto;
        Observacoes = observacoes;
        IdCliente = idCliente;
        IdVeiculo = idVeiculo;
        Estado = estado;
    }
}
