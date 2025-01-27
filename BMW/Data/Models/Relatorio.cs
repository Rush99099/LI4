namespace BMW.Data.Models
{
    public class Relatorio
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public DateTime DataGeracao { get; set; }
        public string? Conteudo { get; set; }
        public int IdFuncionario { get; set; }
        
        public Relatorio() {}

        public Relatorio(int id, int tipo, DateTime dataGeracao, string conteudo, int idFuncionario)
        {
            Id = id;
            Tipo = tipo;
            DataGeracao = dataGeracao;
            Conteudo = conteudo;
            IdFuncionario = idFuncionario;
        }
    }
}
