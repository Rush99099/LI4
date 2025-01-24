namespace BMW.Data.Models
{
    public class Progresso
    {
// Propriedades
        public int Id { get; set; } // Identificador único do progresso
        public DateTime DataRegisto { get; set; } // Data de registo do progresso
        public int TempoExecucao { get; set; } // Tempo de execução (em minutos)
        public string Observacoes { get; set; } // Observações relacionadas ao progresso
        public int IdFuncionario { get; set; }
        // Construtor completo
        public Progresso(int id, DateTime dataRegisto, int tempoExecucao, string observacoes, int idFuncionario)
        {
            Id = id;
            DataRegisto = dataRegisto;
            TempoExecucao = tempoExecucao;
            Observacoes = observacoes;
            IdFuncionario = idFuncionario;
        }
    }
}
