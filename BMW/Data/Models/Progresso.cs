namespace BMW.Data.Models
{
    public class Progresso
    {
// Propriedades
        public int IdEncomenda { get; set; } // Identificador único do progresso

        public int IdFase { get; set; } // Identificador único do progresso
        public DateTime StartFase { get; set; } // Data de registo do progresso
        public DateTime? EndFase { get; set; } // Data do final do progresso (em minutos)
        public string Observacoes { get; set; } // Observações relacionadas ao progresso
        public int IdFuncionario { get; set; }
        // Construtor completo
        public Progresso(int idEncomenda, int idFase, DateTime startFase, DateTime? endFase, string observacoes, int idFuncionario)
        {
            IdEncomenda = idEncomenda;
            IdFase = idFase;
            StartFase = startFase;
            EndFase = endFase;
            Observacoes = observacoes;
            IdFuncionario = idFuncionario;
        }
    }
}
